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

namespace ECSSDK.S3.Model.Internal.MarshallTransformations
{
    class ListBucketMetaSearchKeysResponseUnMarshaller : S3ReponseUnmarshaller
    {
        public override AmazonWebServiceResponse Unmarshall(XmlUnmarshallerContext context)
        {
            ListBucketMetaSearchKeysResponse response = new ListBucketMetaSearchKeysResponse();

            while (context.Read())
            {
                if (context.IsStartElement)
                {
                    UnmarshallResult(context, response);
                    continue;
                }
            }


            return response;
        }

        private static void UnmarshallResult(XmlUnmarshallerContext context, ListBucketMetaSearchKeysResponse response)
        {

            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("IndexableKeys/Key", targetDepth))
                    {
                        response.MetaDataSearchList.IndexableKeys.Add(MetaSearchKeyUnmarshaller.Instance.Unmarshall(context));

                        continue;
                    }
                    if (context.TestExpression("OptionalAttributes/Attribute", targetDepth))
                    {
                        response.MetaDataSearchList.OptionalAttributes.Add(MetaSearchKeyUnmarshaller.Instance.Unmarshall(context));

                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return;
                }
            }



            return;
        }

        private static ListBucketMetaSearchKeysResponseUnMarshaller _instance;

        public static ListBucketMetaSearchKeysResponseUnMarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ListBucketMetaSearchKeysResponseUnMarshaller();
                }
                return _instance;
            }
        }
    }
}
