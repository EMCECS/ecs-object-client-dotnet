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
    /// Put Bucket Request Marshaller
    /// </summary>       
    public class PutBucketRequestMarshallerECS : IMarshaller<IRequest, PutBucketRequestECS> ,IMarshaller<IRequest, Amazon.Runtime.AmazonWebServiceRequest>
    {
        public IRequest Marshall(Amazon.Runtime.AmazonWebServiceRequest input)
        {
            return this.Marshall((PutBucketRequestECS)input);
        }

        public IRequest Marshall(PutBucketRequestECS putBucketRequest)
        {
            var marshaller = new PutBucketRequestMarshaller();
            var request = marshaller.Marshall(putBucketRequest);

            if (putBucketRequest.EnableFileSystem)
                request.Headers.Add(COMMON.EMC_FS_ENABLED, "true");
            if (putBucketRequest.StaleReadAllowed)
                request.Headers.Add(COMMON.EMC_STALE_READ_ALLOWED, "true");
            if (putBucketRequest.EnableCompliance)
                request.Headers.Add(COMMON.EMC_COMPLIANCE_ENABLED, "true");
            if (putBucketRequest.EnableServerSideEncryption)
                request.Headers.Add(COMMON.EMC_DARE_ENABLED, "true");
            if (putBucketRequest.IsSetNamespace())
                request.Headers.Add(COMMON.EMC_NAMESPACE, ECSTransforms.ToStringValue(putBucketRequest.Namespace));
            if (putBucketRequest.IsSetVirtualPoolId())
                request.Headers.Add(COMMON.EMC_VPOOL, ECSTransforms.ToStringValue(putBucketRequest.VirtualPoolId));
            if (putBucketRequest.IsSetRetentionPeriod())
                request.Headers.Add(COMMON.EMC_RETENTION_PERIOD, ECSTransforms.ToStringValue(putBucketRequest.RetentionPeriod));
            if (putBucketRequest.IsSetMetadataSearchKeys())
                request.Headers.Add(COMMON.EMC_METADATA_SEARCH, ECSTransforms.ToStringValue(putBucketRequest.GetMetadataSearchKeys()));

            return request;
        }

    }
}
