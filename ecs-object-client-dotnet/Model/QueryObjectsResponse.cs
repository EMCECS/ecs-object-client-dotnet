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
using System.Collections.Generic;
using ECSSDK.S3.Model.Util;

namespace ECSSDK.S3.Model
{
    public class QueryObjectsResponse : AmazonWebServiceResponse
    {
        private string bucketName;
        private int maxKeys;
        private string nextMarker;
        private List<QueryObject> objects = new List<QueryObject>();

        /// <summary>
        ///  The bucket upon which the query was executed.
        /// </summary>
        public string BucketName
        {
            get { return this.bucketName; }
            set { this.bucketName = value; }
        }

        /// <summary>
        ///  The list of objects that matched the query expression.
        /// </summary>
        public List<QueryObject> ObjectMatches
        {
            get { return this.objects; }
            set { this.objects = value; }
        }

        /// <summary>
        ///  The maximum number of key that was specified to be returned.
        /// </summary>
        public int MaxKeys
        {
            get { return this.maxKeys; }
            set { this.maxKeys = value; }
        }

        /// <summary>
        ///  If applicable, the next marker to be used in subsequent queries.
        /// </summary>
        public string NextMarker
        {
            get { return this.nextMarker; }
            set { this.nextMarker = value; }
        }

        /// <summary>
        ///  Indicates if the next marker has been set.
        /// </summary>
        public bool IsSetNextMarker()
        {
            return (CheckNextMarker());
        }

        private bool CheckNextMarker()
        {
            bool result = false;
            if (this.NextMarker != null && this.NextMarker != "NO MORE PAGES")
            {
                result = true;
            }
            return result;
        }
    }
}
