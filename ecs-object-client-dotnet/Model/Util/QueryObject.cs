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
using System.Collections.Generic;

namespace ECSSDK.S3.Model.Util
{
    public class QueryObject
    {
        private string objectName;
        private string objectId;
        private string versionId;
        private List<QueryMetadata> queryMds = new List<QueryMetadata>();

        /// <summary>
        /// The name/key of the matching object.
        /// </summary>
        public string Name
        {
            get { return this.objectName; }
            set { this.objectName = value; }
        }

        /// <summary>
        /// The object id of the matching object.
        /// </summary>
        public string ObjectId
        {
            get { return this.objectId; }
            set { this.objectId = value; }
        }

        /// <summary>
        /// The version id of the matching object.
        /// </summary>
        public string VersionID
        {
            get { return this.versionId; }
            set { this.versionId = value; }
        }

        /// <summary>
        /// The system and user metadata values for the object.
        /// </summary>
        public List<QueryMetadata> QueryMds
        {
            get { return this.queryMds; }
            set { this.queryMds = value; }
        }
    }
}
