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
    class MetaSearchKeyUnmarshaller : IUnmarshaller<MetaSearchKey, XmlUnmarshallerContext>, IUnmarshaller<MetaSearchKey, JsonUnmarshallerContext>
    {
        public MetaSearchKey Unmarshall(XmlUnmarshallerContext context)
        {
            MetaSearchKey metaKeyItem = new MetaSearchKey();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Name", targetDepth))
                    {
                        metaKeyItem.Name = StringUnmarshaller.GetInstance().Unmarshall(context);

                        continue;
                    }
                    if (context.TestExpression("DataType", targetDepth))
                    {
                        switch (StringUnmarshaller.GetInstance().Unmarshall(context))
                        {
                            case "string":
                                metaKeyItem.Type = MetaSearchDatatype.@string;
                                break;
                            case "datetime":
                                metaKeyItem.Type = MetaSearchDatatype.datetime;
                                break;
                            case "decimal":
                                metaKeyItem.Type = MetaSearchDatatype.@decimal;
                                break;
                            case "integer":
                                metaKeyItem.Type = MetaSearchDatatype.integer;
                                break;
                        }

                        continue;
                    }

                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return metaKeyItem;
                }
            }

            return metaKeyItem;
        }

        public MetaSearchKey Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }

        private static MetaSearchKeyUnmarshaller _instance;

        public static MetaSearchKeyUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MetaSearchKeyUnmarshaller();
                }
                return _instance;
            }
        }


    }
}
