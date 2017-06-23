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
using Amazon.S3.Model;
using ECSSDK.S3.Model.Util;
using System;

namespace ECSSDK.S3.Model
{
    public class PutObjectRequestECS : PutObjectRequest
    {
        private Range range;
        DateTime? modifiedSinceDate;
        DateTime? unmodifiedSinceDate;
        string etagToMatch;
        string etagToNotMatch;
        long retentionPeriod;
        string retentionPolicy;

        /// <summary>
        /// The length of retention (in seconds) to set on the object.  Note that -1 denotes infinity.
        /// </summary>
        public long RetentionPeriod
        {
            get { return this.retentionPeriod; }
            set { this.retentionPeriod = value; }
        }

        /// <summary>
        /// Indicates if a retention period has been set.
        /// </summary>
        internal bool IsSetRetentionPeriod()
        {
            return this.retentionPeriod != default(long);
        }

        /// <summary>
        /// The name of the retention policy to be set on the object.
        /// </summary>
        public string RetentionPolicy
        {
            get { return this.retentionPolicy; }
            set { this.retentionPolicy = value; }
        }

        /// <summary>
        /// Indicates if a retention policy has been set.
        /// </summary>
        internal bool IsSetRetentionPolicy()
        {
            return this.retentionPolicy != null;
        }

        /// <summary>
        /// Sets the byte range for which the object is to be updated.
        /// </summary>
        public Range Range
        {
            get { return this.range; }
            set { this.range = value; }
        }

        /// <summary>
        /// Indicates if a range has been set.
        /// </summary>
        internal bool IsSetRange()
        {
            return this.range != null;
        }

        /// <summary>
        /// ETag to be matched as a pre-condition for updating the object,
        /// otherwise a PreconditionFailed signal is returned.
        /// </summary>
        public string EtagToMatch
        {
            get { return this.etagToMatch; }
            set { this.etagToMatch = value; }
        }

        /// <summary>
        /// Indicates if a etag to match has been set.
        /// </summary>
        internal bool IsSetEtagToMatch()
        {
            return this.etagToMatch != null;
        }

        /// <summary>
        /// Updates the object only if it has been modified since the specified time, 
        /// otherwise returns a PreconditionFailed.
        /// </summary>
        public DateTime ModifiedSinceDate
        {
            get { return this.modifiedSinceDate ?? default(DateTime); }
            set { this.modifiedSinceDate = value; }
        }

        /// <summary>
        /// Indicates if a modified since date has been set.
        /// </summary>
        internal bool IsSetModifiedSinceDate()
        {
            return this.modifiedSinceDate.HasValue;
        }

        /// <summary>
        /// ETag that should not be matched as a pre-condition for updating the object,
        /// otherwise a PreconditionFailed signal is returned.
        /// </summary>
        public string EtagToNotMatch
        {
            get { return this.etagToNotMatch; }
            set { this.etagToNotMatch = value; }
        }

        /// <summary>
        /// Indicates if a etag to not match has been set.
        /// </summary>
        internal bool IsSetEtagToNotMatch()
        {
            return this.etagToNotMatch != null;
        }

        /// <summary>
        /// Updates the object only if it has not been modified since the specified time, 
        /// otherwise returns a PreconditionFailed.
        /// </summary>
        public DateTime UnmodifiedSinceDate
        {
            get { return this.unmodifiedSinceDate ?? default(DateTime); }
            set { this.unmodifiedSinceDate = value; }
        }

        /// <summary>
        /// Indicates if a unmodified since date has been set.
        /// </summary>
        internal bool IsSetUnmodifiedSinceDate()
        {
            return this.unmodifiedSinceDate.HasValue;
        }
    }
}