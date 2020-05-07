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
using Amazon.S3;
using Amazon.Runtime;
using ECSSDK.S3.Model.Internal.MarshallTransformations;
using ECSSDK.S3.Model;
using Amazon.Runtime.Internal.Auth;
using ECSSDK.S3.Model.Util;
using Amazon.Runtime.Internal;

namespace ECSSDK.S3
{
    public class ECSS3Client : Amazon.S3.AmazonS3Client
    {
        /// <summary>
        /// Constructs ECSS3Client with AWS Credentials and an
        /// AmazonS3Client Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonS3Client Configuration Object</param>
        public ECSS3Client(AWSCredentials credentials, AmazonS3Config clientConfig)
            : base(credentials, clientConfig)
        {
        }

        /// <summary>
        /// Creates a new bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutBucket service method.</param>
        /// <returns>The response from the PutBucket service method, as returned by ECS.</returns>
        public PutBucketResponseECS PutBucket(PutBucketRequestECS request)
        {
            var marshaller = new PutBucketRequestMarshallerECS();
            var unmarshaller = PutBucketResponseUnmarshallerECS.Instance;

            InvokeOptions invokeOptions = new InvokeOptions();
            invokeOptions.RequestMarshaller = marshaller;
            invokeOptions.ResponseUnmarshaller = unmarshaller;

            return Invoke<PutBucketResponseECS>(request, invokeOptions);
        }

        /// <summary>
        /// Gets the list of system and user metadata keys that are currently being indexed for the bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListBucketMetaSearchKeys service method.</param>
        /// <returns>The response from the ListBucketMetaSearchKeys service method, as returned by ECS.</returns>
        public ListBucketMetaSearchKeysResponse ListBucketMetaSearchKeys(ListBucketMetaSearchKeysRequest request)
        {
            var marshaller = new ListBucketMetaSearchKeysRequestMarshaller();
            var unmarshaller = ListBucketMetaSearchKeysResponseUnMarshaller.Instance;

            InvokeOptions invokeOptions = new InvokeOptions();
            invokeOptions.RequestMarshaller = marshaller;
            invokeOptions.ResponseUnmarshaller = unmarshaller;

            return Invoke<ListBucketMetaSearchKeysResponse>(request, invokeOptions);
        }

        /// <summary>
        /// Gets the list of system metadata keys that are available for assigning to a bucket.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the ListSystemMetaSearchKeys service method.</param>
        /// <returns>The response from the ListSystemMetaSearchKeys service method, as returned by ECS.</returns>
        public ListSystemMetaSearchKeysResponse ListSystemMetaSearchKeys(ListSystemMetaSearchKeysRequest request)
        {
            var marshaller = new ListSystemMetaSearchKeysRequestMarshaller();
            var unmarshaller = ListSystemMetaSearchKeysResponseUnMarshaller.Instance;

            InvokeOptions invokeOptions = new InvokeOptions();
            invokeOptions.RequestMarshaller = marshaller;
            invokeOptions.ResponseUnmarshaller = unmarshaller;

            return Invoke<ListSystemMetaSearchKeysResponse>(request, invokeOptions);
        }

        /// <summary>
        /// Creates or updates an object using provided parameters.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the PutObject service method.</param>
        /// <returns>The response from the PutObject service method, as returned by ECS.</returns>
        public PutObjectResponseECS PutObject(PutObjectRequestECS request)
        {
            var marshaller = new PutObjectRequestMarshallerECS();
            var unmarshaller = PutObjectResponseUnmarshallerECS.Instance;

            InvokeOptions invokeOptions = new InvokeOptions();
            invokeOptions.RequestMarshaller = marshaller;
            invokeOptions.ResponseUnmarshaller = unmarshaller;

            return Invoke<PutObjectResponseECS>(request, invokeOptions);
        }

        /// <summary>
        /// Appends the provided data to the end of the object.
        /// </summary>
        /// <param name="bucketName">Container for the necessary parameters to execute the PutObject service method.</param>
        /// <returns>The response from the PutObject service method, as returned by ECS.</returns>
        public long AppendObject(string bucketName, string keyName, string contentBody)
        {
            var marshaller = new PutObjectRequestMarshallerECS();
            var unmarshaller = PutObjectResponseUnmarshallerECS.Instance;

            InvokeOptions invokeOptions = new InvokeOptions();
            invokeOptions.RequestMarshaller = marshaller;
            invokeOptions.ResponseUnmarshaller = unmarshaller;

            PutObjectRequestECS request = new PutObjectRequestECS()
            {
                BucketName = bucketName,
                Key = keyName,
                ContentBody = contentBody,
                Range = Range.fromOffset(-1),
                UseChunkEncoding = false
            };

            PutObjectResponseECS response = Invoke<PutObjectResponseECS>(request, invokeOptions);
            return response.AppendOffset;
        }

        /// <summary>
        /// Executes a bucket search and returns a list of objects including system and user metadata values based on supplied query.
        /// </summary>
        /// <param name="request">Container for the necessary parameters to execute the QueryObjects service method.</param>
        /// <returns>The response from the QueryObjects service method, as returned by ECS.</returns>
        public QueryObjectsResponse QueryObjects(QueryObjectsRequest request)
        {
            var marshaller = new QueryObjectsRequestMarshaller();
            var unmarshaller = QueryObjectsResponseUnmarshaller.Instance;

            InvokeOptions invokeOptions = new InvokeOptions();
            invokeOptions.RequestMarshaller = marshaller;
            invokeOptions.ResponseUnmarshaller = unmarshaller;

            return Invoke<QueryObjectsResponse>(request, invokeOptions); 
        }

        protected override AbstractAWSSigner CreateSigner()
        {
            return new Internal.S3SignerECS();
        }
    }
}
