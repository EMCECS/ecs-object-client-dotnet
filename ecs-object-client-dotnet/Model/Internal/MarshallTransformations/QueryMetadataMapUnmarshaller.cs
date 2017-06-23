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
using Amazon.Runtime.Internal.Transform;
using ECSSDK.S3.Model.Util;

namespace ECSSDK.S3.Model.Internal.MarshallTransformations
{
    class QueryMetadataMapUnmarshaller : IUnmarshaller<QueryMetadataMap, XmlUnmarshallerContext>, IUnmarshaller<QueryMetadataMap, JsonUnmarshallerContext>
    {
        public QueryMetadataMap Unmarshall(XmlUnmarshallerContext context)
        {
            QueryMetadataMap queryMetadataMap = new QueryMetadataMap();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("key", targetDepth))
                    {
                        queryMetadataMap.Key = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }

                    if (context.TestExpression("value", targetDepth))
                    {
                        queryMetadataMap.Value = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return queryMetadataMap;
                }
            }

            return queryMetadataMap;
        }

        public QueryMetadataMap Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }

        private static QueryMetadataMapUnmarshaller _instance;

        public static QueryMetadataMapUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QueryMetadataMapUnmarshaller();
                }
                return _instance;
            }
        }
    }
}