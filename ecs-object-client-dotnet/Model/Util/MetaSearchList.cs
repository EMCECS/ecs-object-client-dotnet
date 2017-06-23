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
    public class MetaSearchList
    {
        private List<MetaSearchKey> indexableSearchKeys = new List<MetaSearchKey>();
        private List<MetaSearchKey> optionalAttributes = new List<MetaSearchKey>();

        /// <summary>
        /// Gets and sets the IndexableKeys property. This is a list of 
        /// indexable keys on the bucket that can later be used for metadata search.
        /// </summary>
        public List<MetaSearchKey> IndexableKeys
        {
            get { return this.indexableSearchKeys; }
            set { this.indexableSearchKeys = value; }
        }

        /// <summary>
        /// Indicates if any indexable metadata search keys have been set.
        /// </summary>
        internal bool IsSetIndexableSearchKeys()
        {
            return this.indexableSearchKeys.Count > 0;
        }

        /// <summary>
        /// Gets and sets the OptionalAttributes property. This is a list of 
        /// attributes that can be used to better describe characteristics of the bucket.
        /// </summary>
        public List<MetaSearchKey> OptionalAttributes
        {
            get { return this.optionalAttributes; }
            set { this.optionalAttributes = value; }
        }

        /// <summary>
        /// Indicates if any optional metadata attributes have been set.
        /// </summary>
        internal bool IsSetOptionalAttributes()
        {
            return this.optionalAttributes.Count > 0;
        }
    }
}
    