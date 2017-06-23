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

namespace ECSSDK.S3.Model
{
    public class QueryObjectsRequest : AmazonWebServiceRequest
    {
        private string bucketName;
        private string query;
        private List<string> attributes = new List<string>();
        private string sorted;
        private int? maxKeys;
        private string marker;
        private bool includeOlderVersions = false;

        /// <summary>
        /// If set to true the query result will include both current and non-current versions of the objects.
        /// Default: false.
        /// </summary>
        public bool IncludeOlderVersions
        {
            get { return this.includeOlderVersions; }
            set
            {
                this.includeOlderVersions = value;
            }
        }

        /// <summary>
        ///  The bucket upon which the operation is taking place.
        /// </summary>
        public string BucketName
        {
            get { return this.bucketName; }
            set { this.bucketName = value; }
        }

        /// <summary>
        /// The query to be used to search for matching objects in the bucket.
        /// </summary>
        public string Query
        {
            get { return this.query; }
            set { this.query = value; }
        }

        /// <summary>
        /// Indicates if a metadata search query has been set.
        /// </summary>
        internal bool IsSetQuery()
        {
            return this.query != null;
        }

        /// <summary>
        /// A list of system or user defined metadata values to be included in the query results.
        /// </summary>
        public List<string> Attributes
        {
            get { return this.attributes; }
            set { this.attributes = value; }
        }

        /// <summary>
        /// Indicates if a list of attributes has been set.
        /// </summary>
        internal bool IsSetAttributes()
        {
            return this.attributes.Count > 0;
        }

        /// <summary>
        /// Sets the index by which to sort the keys returned in the response.  This index must be part of the query.
        /// </summary>
        public string Sorted
        {
            get { return this.sorted; }
            set { this.sorted = value; }
        }

        /// <summary>
        /// Indicates if a sort value has been set.
        /// </summary>
        internal bool IsSetSorted()
        {
            return this.sorted != null;
        }

        /// <summary>
        /// Sets the maximum number of keys returned in the response. The response might contain fewer keys but will never contain more.
        /// </summary>
        public int MaxKeys
        {
            get { return this.maxKeys ?? default(int); }
            set { this.maxKeys = value; }
        }

        /// <summary>
        /// Indicates if a max keys value has been set.
        /// </summary>
        internal bool IsSetMaxKeys()
        {
            return this.maxKeys.HasValue;
        }

        /// <summary>
        /// Specifies the key to start with when listing results from a metadata search.
        /// </summary>
        public string Marker
        {
            get { return this.marker; }
            set { this.marker = value; }
        }

        /// <summary>
        /// Indicates if a marker has been set.
        /// </summary>
        internal bool IsSetMarker()
        {
            return this.marker != null;
        }
    }
}
