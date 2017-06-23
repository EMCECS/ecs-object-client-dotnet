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
    public class Range
    {
        private long first;
        private long? last;

        public static Range fromOffsetLength(long offset, long length) { return new Range(offset, offset + length - 1); }

        public static Range fromOffset(long offset) { return new Range(offset, null); }

        public Range(long first, long? last)
        {
            this.first = first;
            this.last = last;
        }

        public long First
        {
            get { return this.first; }
        }

        public long? Last
        {
            get { return this.last; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Range range = obj as Range;
            if (GetType() != range.GetType())
                return false;

            return (first == range.first) && (last == range.last);
        }

        public override int GetHashCode()
        {
            int result = first.GetHashCode();
            result = 31 * result + last.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Convert.ToString(first), last == null ? string.Empty : Convert.ToString(last));
        }
    }

}