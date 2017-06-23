/**
 * Copyright 2017 EMC Corporation. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/
using Amazon.Runtime;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Amazon.Runtime.Internal.Transform;
using ECSSDK.S3.Model.Util;
using Amazon.S3.Model;
using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ECSSDK.S3.Model.Internal.MarshallTransformations
{
    public class PutObjectResponseUnmarshallerECS : S3ReponseUnmarshaller
    {
        public override AmazonWebServiceResponse Unmarshall(XmlUnmarshallerContext context)
        {
            PutObjectResponseECS response = new PutObjectResponseECS();

            UnmarshallResult(context, response);


            return response;
        }

        private static void UnmarshallResult(XmlUnmarshallerContext context, PutObjectResponseECS response)
        {
            IWebResponseData responseData = context.ResponseData;
            if (responseData.IsHeaderPresent("x-amz-expiration"))
                response.Expiration = ParseExpirationHeader(responseData.GetHeaderValue("x-amz-expiration"));
            if (responseData.IsHeaderPresent("x-amz-server-side-encryption"))
                response.ServerSideEncryptionMethod = ECSTransforms.ToString(responseData.GetHeaderValue("x-amz-server-side-encryption"));
            if (responseData.IsHeaderPresent("ETag"))
                response.ETag = ECSTransforms.ToString(responseData.GetHeaderValue("ETag"));
            if (responseData.IsHeaderPresent("x-amz-version-id"))
                response.VersionId = ECSTransforms.ToString(responseData.GetHeaderValue("x-amz-version-id"));
            if (responseData.IsHeaderPresent(COMMON.XAmzServerSideEncryptionAwsKmsKeyIdHeader))
                response.ServerSideEncryptionKeyManagementServiceKeyId = ECSTransforms.ToString(responseData.GetHeaderValue(COMMON.XAmzServerSideEncryptionAwsKmsKeyIdHeader));
            if (responseData.IsHeaderPresent(COMMON.EMC_APPEND_OFFSET))
                response.AppendOffset = Convert.ToInt64(ECSTransforms.ToString(responseData.GetHeaderValue(COMMON.EMC_APPEND_OFFSET)));

            return;
        }

        private static PutObjectResponseUnmarshallerECS _instance;

        public static PutObjectResponseUnmarshallerECS Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PutObjectResponseUnmarshallerECS();
                }
                return _instance;
            }
        }

        // Check STORAGE-17982 as the PUT wasn't returning this header.  Only HEAD.
        // x-amz-expiration: expiry-date="Mon, 19 Jun 2017 00:00:00 GMT", rule-id="log-file-removal"
        private static Expiration ParseExpirationHeader(string headerData)
        {            
            DateTime expiryDate;
            string ruleId;

            Regex expiryDateRegex = new Regex("expiry-date=\"(.+?)\"");
            Regex ruleIdRegex = new Regex("rule-id=\"(.+?)\"");

            if (string.IsNullOrEmpty(headerData))
                throw new ArgumentNullException("headerData");

            var expiryMatches = expiryDateRegex.Match(headerData);
            if (!expiryMatches.Success || !expiryMatches.Groups[1].Success)
                throw new InvalidOperationException("No Expiry Date match");
            string expiryDateValue = expiryMatches.Groups[1].Value;
            expiryDate = DateTime.ParseExact(expiryDateValue, COMMON.GMTDateFormat, CultureInfo.InvariantCulture);

            var ruleMatches = ruleIdRegex.Match(headerData);
            if (!ruleMatches.Success || !ruleMatches.Groups[1].Success)
                throw new InvalidOperationException("No Rule Id match");
            string ruleIdValue = ruleMatches.Groups[1].Value;
            ruleId = UrlDecode(ruleIdValue);

            Expiration result = new Expiration() {
                ExpiryDate = expiryDate,
                RuleId = ruleId
            };

            return result;
        }

        private static string UrlDecode(string url)
        {
            string decoded = Uri.UnescapeDataString(url).Replace("+", " ");
            return decoded;
        }
    }   

}
