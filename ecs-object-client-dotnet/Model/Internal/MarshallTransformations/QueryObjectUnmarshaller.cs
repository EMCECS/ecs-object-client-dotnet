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
    public class QueryObjectUnmarshaller : IUnmarshaller<QueryObject, XmlUnmarshallerContext>, IUnmarshaller<QueryObject, JsonUnmarshallerContext>
    {
        public QueryObject Unmarshall(XmlUnmarshallerContext context)
        {
            QueryObject queryObject = new QueryObject();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("objectName", targetDepth))
                    {
                        queryObject.Name = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }

                    if (context.TestExpression("objectId", targetDepth))
                    {
                        queryObject.ObjectId = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }

                    if (context.TestExpression("versionId", targetDepth))
                    {
                        queryObject.VersionID = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }

                    if (context.TestExpression("queryMds", targetDepth))
                    {
                        queryObject.QueryMds.Add(QueryMetadataUnmarshaller.Instance.Unmarshall(context));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return queryObject;
                }
            }

            return queryObject;
        }

        public QueryObject Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }

        private static QueryObjectUnmarshaller _instance;

        public static QueryObjectUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QueryObjectUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
