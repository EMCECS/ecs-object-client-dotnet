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

namespace ECSSDK.S3.Model.Internal.MarshallTransformations
{
    public class ListBucketMetaSearchKeysRequestMarshaller : IMarshaller<IRequest, ListBucketMetaSearchKeysRequest>, IMarshaller<IRequest, Amazon.Runtime.AmazonWebServiceRequest>
    {
        public IRequest Marshall(Amazon.Runtime.AmazonWebServiceRequest input)
        {
            return this.Marshall((ListBucketMetaSearchKeysRequest)input);
        }

        public IRequest Marshall(ListBucketMetaSearchKeysRequest listMetaSearcKeysRequest)
        {
            IRequest request = new DefaultRequest(listMetaSearcKeysRequest, "AmazonS3");

            request.HttpMethod = "GET";
            request.ResourcePath = string.Concat("/", ECSTransforms.ToStringValue(listMetaSearcKeysRequest.BucketName));

            request.AddSubResource("searchmetadata");

            request.UseQueryString = true;
            return request;
        }
    }
}
