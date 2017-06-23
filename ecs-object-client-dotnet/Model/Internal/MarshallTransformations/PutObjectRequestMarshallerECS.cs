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
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.S3.Model.Internal.MarshallTransformations;
using ECSSDK.S3.Model.Util;

namespace ECSSDK.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Put Object Request Marshaller
    /// </summary> 
    public class PutObjectRequestMarshallerECS : IMarshaller<IRequest, PutObjectRequestECS>, IMarshaller<IRequest, Amazon.Runtime.AmazonWebServiceRequest>
    {
        public IRequest Marshall(Amazon.Runtime.AmazonWebServiceRequest input)
        {
            return this.Marshall((PutObjectRequestECS)input);
        }

        public IRequest Marshall(PutObjectRequestECS putObjectRequest)
        {
            var marshaller = new PutObjectRequestMarshaller();
            var request = marshaller.Marshall(putObjectRequest);

            if (putObjectRequest.IsSetRange())
                request.Headers.Add(COMMON.HEADER_RANGE, "bytes=" + putObjectRequest.Range.ToString());
            if (putObjectRequest.IsSetEtagToMatch())
                request.Headers.Add(COMMON.HEADER_IF_MATCH, ECSTransforms.ToStringValue(putObjectRequest.EtagToMatch));
            if (putObjectRequest.IsSetEtagToNotMatch())
                request.Headers.Add(COMMON.HEADER_IF_NONE_MATCH, ECSTransforms.ToStringValue(putObjectRequest.EtagToNotMatch));
            if (putObjectRequest.IsSetModifiedSinceDate())
                request.Headers.Add(COMMON.HEADER_IF_MODIFIED_SINCE, ECSTransforms.ToStringValue(putObjectRequest.ModifiedSinceDate));
            if (putObjectRequest.IsSetUnmodifiedSinceDate())
                request.Headers.Add(COMMON.HEADER_IF_UNMODIFIED_SINCE, ECSTransforms.ToStringValue(putObjectRequest.UnmodifiedSinceDate));
            if (putObjectRequest.IsSetRetentionPeriod())
                request.Headers.Add(COMMON.EMC_RETENTION_PERIOD, ECSTransforms.ToStringValue(putObjectRequest.RetentionPeriod));
            if (putObjectRequest.IsSetRetentionPolicy())
                request.Headers.Add(COMMON.EMC_RETENTION_POLICY, ECSTransforms.ToStringValue(putObjectRequest.RetentionPolicy));

            return request;
        }
    }
}
