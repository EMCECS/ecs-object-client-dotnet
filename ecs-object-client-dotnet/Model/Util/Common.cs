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
namespace ECSSDK.S3.Model.Util
{
    public class COMMON
    {
        public static string EMC_PREFIX = "x-emc-";

        public static string EMC_METADATA_SEARCH = EMC_PREFIX + "metadata-search";
        public static string EMC_FS_ENABLED = EMC_PREFIX + "file-system-access-enabled";
        public static string EMC_RETENTION_PERIOD = EMC_PREFIX + "retention-period";
        public static string EMC_RETENTION_POLICY = EMC_PREFIX + "retention-policy";
        public static string EMC_STALE_READ_ALLOWED = EMC_PREFIX + "is-stale-allowed";
        public static string EMC_VPOOL = EMC_PREFIX + "vpool";
        public static string EMC_COMPLIANCE_ENABLED = EMC_PREFIX + "compliance-enabled";
        public static string EMC_DARE_ENABLED = EMC_PREFIX + "server-side-encryption-enabled";
        public static string EMC_NAMESPACE = EMC_PREFIX + "namespace";
        public static string EMC_APPEND_OFFSET = EMC_PREFIX + "append-offset";

        public static string PARAM_QUERY = "query";
        public static string PARAM_ATTRIBUTES = "attributes";
        public static string PARAM_SORTED = "sorted";
        public static string PARAM_MAX_KEYS = "max-keys";
        public static string PARAM_MARKER = "marker";
        public static string PARAM_INCLUDE_OLDER_VERSIONS = "include-older-versions";

        public static string HEADER_RANGE = "Range";
        public static string HEADER_IF_MATCH = "If-Match";
        public static string HEADER_IF_NONE_MATCH = "If-None-Match";
        public static string HEADER_IF_MODIFIED_SINCE = "If-Modified-Since";
        public static string HEADER_IF_UNMODIFIED_SINCE = "If-Unmodified-Since";

        public static string XAmzServerSideEncryptionAwsKmsKeyIdHeader = "x-amz-server-side-encryption-aws-kms-key-id";

        public static string GMTDateFormat = "ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T";
    }
}
