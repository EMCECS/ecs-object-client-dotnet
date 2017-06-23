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
using System.Linq;
using ECSSDK.S3.Model.Util;

namespace ECSSDK.S3.Model.Internal.MarshallTransformations
{
    public class QueryObjectsRequestMarshaller : IMarshaller<IRequest, QueryObjectsRequest>, IMarshaller<IRequest, Amazon.Runtime.AmazonWebServiceRequest>
    {
        public IRequest Marshall(Amazon.Runtime.AmazonWebServiceRequest input)
        {
            return this.Marshall((QueryObjectsRequest)input);
        }

        public IRequest Marshall(QueryObjectsRequest queryObjectsRequest)
        {
            IRequest request = new DefaultRequest(queryObjectsRequest, "AmazonS3");

            request.HttpMethod = "GET";
            request.ResourcePath = string.Concat("/", ECSTransforms.ToStringValue(queryObjectsRequest.BucketName));

            if (queryObjectsRequest.IsSetQuery())
                request.SubResources.Add(COMMON.PARAM_QUERY, ECSTransforms.ToStringValue(queryObjectsRequest.Query));
            else
                throw new System.ArgumentNullException("Query", "Unable to submit query objecsts request with null or empty Query parameter.");

            if (queryObjectsRequest.IsSetAttributes())
                request.Parameters.Add(COMMON.PARAM_ATTRIBUTES, string.Join(",", queryObjectsRequest.Attributes.Select(s => ECSTransforms.ToStringValue(s))));
            if (queryObjectsRequest.IsSetSorted())
                request.Parameters.Add(COMMON.PARAM_SORTED, ECSTransforms.ToStringValue(queryObjectsRequest.Sorted));
            if (queryObjectsRequest.IsSetMaxKeys())
                request.Parameters.Add(COMMON.PARAM_MAX_KEYS, ECSTransforms.ToStringValue(queryObjectsRequest.MaxKeys));
            if (queryObjectsRequest.IsSetMarker())
                request.Parameters.Add(COMMON.PARAM_MARKER, ECSTransforms.ToStringValue(queryObjectsRequest.Marker));
            if (queryObjectsRequest.IncludeOlderVersions)
                request.Parameters.Add(COMMON.PARAM_INCLUDE_OLDER_VERSIONS, "true");

            request.UseQueryString = true;
            return request;
        }
    }
}
