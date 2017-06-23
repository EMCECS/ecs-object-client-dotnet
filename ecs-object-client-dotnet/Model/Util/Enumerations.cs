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
    /// <summary>
    /// The valid search metadata data types supported by ECS.
    /// </summary>
    public enum MetaSearchDatatype {@string, integer, datetime, @decimal};

    /// <summary>
    /// The valid metadata types supported by ECS.
    /// </summary>
    public enum QueryMetadataType { SYSMD, USERMD };
}
