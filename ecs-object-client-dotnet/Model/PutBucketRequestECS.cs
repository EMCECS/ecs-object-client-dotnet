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
using System.Collections.Generic;
using System.Text;

namespace ECSSDK.S3.Model
{
    public class PutBucketRequestECS : PutBucketRequest
    {
        private bool enableFileSystem = false;
        private bool enableCompliance = false;
        private bool enableServerSideEncryption = false;
        private string metadataSearchKeys;
        private bool staleReadAllowed = false;
        private long? retentionPeriod;
        private string vPoolId;
        private string nameSpace;

        /// <summary>
        /// Indicates if a namespace value has been set.
        /// </summary>
        internal bool IsSetNamespace()
        {
            return this.nameSpace !=null;
        }

        /// <summary>
        /// Sets what namespace will own the bucket -will be passed in request header.
        /// </summary>
        public string Namespace
        {
            get { return this.nameSpace; }
            set
            {
                this.nameSpace = value;
            }
        }

        /// <summary>
        /// Sets a retention period in seconds for all objects in bucket, -1 denotes infinity.
        /// </summary>
        public long RetentionPeriod
        {
            get { return this.retentionPeriod ?? default(long); }
            set
            {
                this.retentionPeriod = value;
            }
        }

        /// <summary>
        /// Indicates if a retention period value has been set.
        /// </summary>
        internal bool IsSetRetentionPeriod()
        {
            return this.retentionPeriod.HasValue;
        }

        /// <summary>
        /// The replication group to associate the new bucket with.
        /// </summary>
        public string VirtualPoolId
        {
            get { return this.vPoolId; }
            set
            {
                this.vPoolId = value;
            }
        }

        // Check to see if VirtualPoolId property is set.
        internal bool IsSetVirtualPoolId()
        {
            return this.vPoolId != null;
        }

        /// <summary>
        /// If set to true, D@RE (data at rest encryption) will be enabled on the bucket.
        /// </summary>
        public bool EnableServerSideEncryption
        {
            get { return this.enableServerSideEncryption; }
            set
            {
                this.enableServerSideEncryption = value;
            }
        }

        /// <summary>
        /// If set to true, compliance will be enabled on the bucket.
        /// </summary>
        public bool EnableCompliance
        {
            get { return this.enableCompliance; }
            set
            {
                this.enableCompliance = value;
            }
        }

        /// <summary>
        /// If set to true, during temporary VDC outage, the bucket can still be accessed.
        /// </summary>
        public bool StaleReadAllowed
        {
            get { return this.staleReadAllowed; }
            set
            {
                this.staleReadAllowed = value;
            }
        }

        /// <summary>
        /// If set to true the bucket will be created as a file system enabled bucket
        /// allowing access via NFS through export configuration.
        /// Default: true.
        /// </summary>
        public bool EnableFileSystem
        {
            get { return this.enableFileSystem; }
            set
            {
                this.enableFileSystem = value;
            }
        }

        // Check to see if MetadataSearchKeys property is set
        internal bool IsSetMetadataSearchKeys()
        {
            return this.metadataSearchKeys != null;
        }

        /// <summary>
        /// Sets the metadata search keys to be used when the bucket is created.
        /// </summary>
        /// <param name="metadataSearchKeys">A list of metadata search keys to act as indexes on the bucket as objects are created/updated.</param>
        public void SetMetadataSearchKeys(List<MetaSearchKey> metadataSearchKeys)
        {
            StringBuilder sb = new StringBuilder();
            foreach (MetaSearchKey key in metadataSearchKeys)
            {
                if (sb.Length > 0) { sb.Append(","); }
                sb.Append(string.Format("{0};{1}", key.Name, key.Type));
            }
            this.metadataSearchKeys = sb.ToString();
        }

        /// <summary>
        /// Gets the metadata search keys to be used when the bucket is created.
        /// </summary>
        public string GetMetadataSearchKeys()
        {
            return this.metadataSearchKeys;
        }
    }
}
