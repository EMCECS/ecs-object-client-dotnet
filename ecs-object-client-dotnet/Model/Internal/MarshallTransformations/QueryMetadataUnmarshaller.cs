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
    public class QueryMetadataUnmarshaller : IUnmarshaller<QueryMetadata, XmlUnmarshallerContext>, IUnmarshaller<QueryMetadata, JsonUnmarshallerContext>
    {
        public QueryMetadata Unmarshall(XmlUnmarshallerContext context)
        {
            QueryMetadata queryMetadata = new QueryMetadata();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("type", targetDepth))
                    {
                        switch (StringUnmarshaller.GetInstance().Unmarshall(context))
                        {
                            case "SYSMD":
                                queryMetadata.Type = QueryMetadataType.SYSMD;
                                break;
                            case "USERMD":
                                queryMetadata.Type = QueryMetadataType.USERMD;
                                break;
                        }

                        continue;
                    }

                    if (context.TestExpression("mdMap/entry", targetDepth))
                    {
                        queryMetadata.MdMap.Add(QueryMetadataMapUnmarshaller.Instance.Unmarshall(context));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return queryMetadata;
                }
            }

            return queryMetadata;
        }

        public QueryMetadata Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }

        private static QueryMetadataUnmarshaller _instance;

        public static QueryMetadataUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QueryMetadataUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
