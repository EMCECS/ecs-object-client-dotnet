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
using System.Globalization;

namespace ECSSDK.S3.Model.Internal.MarshallTransformations
{
    public static class ECSTransforms
    {
        public static string ToStringValue(string value)
        {
            return value ?? "";
        }

        public static string ToStringValue(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringValue(long value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        internal static string ToString(string value)
        {
            return value;
        }

        /*internal static string ToStringNullSafe<T>(this T value) {
            if (value == null)
            {
                return null;
            } else
            {
                return value.ToString();
            }
        }

        internal static string ToStringValue(DateTime? value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                var temp = (DateTime)value;
                return temp.ToString("ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T", CultureInfo.InvariantCulture);
            }
        }*/

        internal static string ToStringValue(DateTime value)
        {
            return value.ToString("ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T", CultureInfo.InvariantCulture);
        }
    }
}
