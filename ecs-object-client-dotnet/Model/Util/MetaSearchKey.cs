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
using System;

namespace ECSSDK.S3.Model.Util
{
    public class MetaSearchKey : IComparable<MetaSearchKey>
    {
        private string name;
        private MetaSearchDatatype dataType;

        public MetaSearchKey() { }

        /// <summary>
        /// The name of the user-defined metadata key for indexing the bucket.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// The data type of the user-defined metadata key for indexing the bucket.
        /// </summary>
        public MetaSearchDatatype Type
        {
            get { return this.dataType; }
            set
            {
                this.dataType = value;
            }
        }

        int IComparable<MetaSearchKey>.CompareTo(MetaSearchKey other)
        {
            return name.CompareTo(other.name);
        }
    }
}
