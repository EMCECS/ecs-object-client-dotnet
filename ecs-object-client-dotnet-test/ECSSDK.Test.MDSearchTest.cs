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
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using ECSSDK.S3;
using ECSSDK.S3.Model;
using ECSSDK.S3.Model.Util;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;


namespace ECSSDK.Test
{
    [TestClass]
    public class MDSearchTest
    {
        /// <summary>
        /// The S3 client -handles object api interactions.
        ///</summary>
        static ECSS3Client client;

        /// <summary>
        /// Generic bucket name used for bucket API testing.
        ///</summary>
        static string temp_bucket = Guid.NewGuid().ToString();

        /// <summary>
        /// List of indexable keys to be created with the bucket.
        /// </summary>
        static List<MetaSearchKey> bucketMetadataSearchKeys = new List<MetaSearchKey>()
        {
            new MetaSearchKey() { Name = "x-amz-meta-datetimevalue", Type = MetaSearchDatatype.datetime },
            new MetaSearchKey() { Name = "x-amz-meta-decimalvalue", Type = MetaSearchDatatype.@decimal },
            new MetaSearchKey() { Name = "x-amz-meta-integervalue", Type = MetaSearchDatatype.integer },
            new MetaSearchKey() { Name = "x-amz-meta-stringvalue", Type = MetaSearchDatatype.@string }
        };

        /// <summary>
        /// List of system indexable that can be created with the bucket.
        /// </summary>
        static List<MetaSearchKey> indexableSystemKeys = new List<MetaSearchKey>()
        {
                new MetaSearchKey() { Name = "CreateTime", Type = MetaSearchDatatype.@datetime},
                new MetaSearchKey() { Name = "LastModified", Type = MetaSearchDatatype.@datetime},
                new MetaSearchKey() { Name = "ObjectName", Type = MetaSearchDatatype.@string },
                new MetaSearchKey() { Name = "Owner", Type = MetaSearchDatatype.@string },
                new MetaSearchKey() { Name = "Size", Type = MetaSearchDatatype.integer}
        };

        /// <summary>
        /// List of system optional attributes that can be created with the bucket.
        /// </summary>
        static List<MetaSearchKey> optionalSystemKeys = new List<MetaSearchKey>()
        {
                new MetaSearchKey() { Name = "ContentEncoding", Type = MetaSearchDatatype.@string},
                new MetaSearchKey() { Name = "ContentType", Type = MetaSearchDatatype.@string},
                new MetaSearchKey() { Name = "CreateTime", Type = MetaSearchDatatype.@datetime },
                new MetaSearchKey() { Name = "Etag", Type = MetaSearchDatatype.@string },
                new MetaSearchKey() { Name = "Expiration", Type = MetaSearchDatatype.@datetime},
                new MetaSearchKey() { Name = "Expires", Type = MetaSearchDatatype.@datetime },
                new MetaSearchKey() { Name = "LastModified", Type = MetaSearchDatatype.@datetime },
                new MetaSearchKey() { Name = "Namespace", Type = MetaSearchDatatype.@string },
                new MetaSearchKey() { Name = "ObjectName", Type = MetaSearchDatatype.@string },
                new MetaSearchKey() { Name = "Owner", Type = MetaSearchDatatype.@string },
                new MetaSearchKey() { Name = "Retention", Type = MetaSearchDatatype.integer },
                new MetaSearchKey() { Name = "Size", Type = MetaSearchDatatype.integer },
        };

        [ClassInitialize()]
        public static void Initialize(TestContext testContext)
        {
            BasicAWSCredentials creds = new BasicAWSCredentials(ConfigurationManager.AppSettings["S3_ACCESS_KEY_ID"], ConfigurationManager.AppSettings["S3_SECRET_KEY"]);

            AmazonS3Config cc = new AmazonS3Config()
            {
                ForcePathStyle = true,
                ServiceURL = ConfigurationManager.AppSettings["S3_ENDPOINT"],
                SignatureVersion = "2",
                SignatureMethod = SigningAlgorithm.HmacSHA1,
                UseHttp = false,
            };
            client = new ECSS3Client(creds, cc);

            PutBucketRequestECS request = new PutBucketRequestECS()
            {
                BucketName = temp_bucket,
            };

            // Set the indexable search keys on the bucket.
            request.SetMetadataSearchKeys(bucketMetadataSearchKeys);
            string vpool_id = ConfigurationManager.AppSettings["VPOOL_ID"];
            if (!string.IsNullOrWhiteSpace(vpool_id))
            {
                request.VirtualPoolId = vpool_id;
            }

            client.PutBucket(request);

            for (int i = 0; i < 5; i++)
            {
                PutObjectRequest object_request = new PutObjectRequest()
                {
                    BucketName = temp_bucket,
                    Key = string.Format("obj-{0}", i),
                    ContentBody = string.Format("This is sample content for object {0}", i)
                };

                object_request.Metadata.Add("x-amz-meta-decimalvalue", Convert.ToString(i * 2));
                object_request.Metadata.Add("x-amz-meta-stringvalue", string.Format("sample-{0}", Convert.ToString(i)));

                client.PutObject(object_request);
            }
        }

        /// <summary>
        /// Clean up instances used in this group of tests.
        ///</summary>
        [ClassCleanup()]
        public static void Cleanup()
        {
            CleanBucket(temp_bucket);

            DeleteBucketRequest request = new DeleteBucketRequest()
            {
                BucketName = temp_bucket
            };
            client.DeleteBucket(request);
        }

        [TestMethod]
        public void TestListBucketMetaSearchKeys()
        {
            ListBucketMetaSearchKeysRequest request = new ListBucketMetaSearchKeysRequest()
            {
                BucketName = temp_bucket
            };

            ListBucketMetaSearchKeysResponse response = client.ListBucketMetaSearchKeys(request);
            Assert.IsNotNull(response.MetaDataSearchList.IndexableKeys);
            CompareKeyResults(response.MetaDataSearchList.IndexableKeys, bucketMetadataSearchKeys);
        }

        [TestMethod]
        public void TestListSystemMetaSearchKeys()
        {
            ListSystemMetaSearchKeysRequest request = new ListSystemMetaSearchKeysRequest();

            ListSystemMetaSearchKeysResponse response = client.ListSystemMetaSearchKeys(request);
            Assert.IsNotNull(response);
            CompareKeyResults(response.MetaDataSearchList.IndexableKeys, indexableSystemKeys);
            CompareKeyResults(response.MetaDataSearchList.OptionalAttributes, optionalSystemKeys);
        }

        private void CompareKeyResults(List<MetaSearchKey> actual, List<MetaSearchKey> expected)
        {
            Assert.AreEqual(actual.Count, expected.Count);

            actual.Sort();

            for (int i = 0; i < actual.Count; i++)
            {
                var key = actual[i];
                Assert.AreEqual(key.Name, expected[i].Name);
                Assert.AreEqual(key.Type, expected[i].Type);
            }
        }

        [TestMethod]
        public void TestingAQueryDecimal()
        {
            QueryObjectsRequest query_request = new QueryObjectsRequest()
            {
                BucketName = temp_bucket,
                Query = "x-amz-meta-decimalvalue>=6",
            };

            var response = client.QueryObjects(query_request);
            Assert.AreEqual(temp_bucket, response.BucketName);
            Assert.IsNotNull(response.ObjectMatches);
            Assert.AreEqual(2, response.ObjectMatches.Count);
            Assert.AreEqual(false, response.IsSetNextMarker());
        }

        [TestMethod]
        public void TestingAQueryString()
        {
            QueryObjectsRequest query_request = new QueryObjectsRequest()
            {
                BucketName = temp_bucket,
                Query = "x-amz-meta-stringvalue==\"sample-4\"",
            };

            var response = client.QueryObjects(query_request);
            Assert.AreEqual(temp_bucket, response.BucketName);
            Assert.IsNotNull(response.ObjectMatches);
            Assert.AreEqual(1, response.ObjectMatches.Count);
            Assert.AreEqual(false, response.IsSetNextMarker());
        }

        [TestMethod]
        public void TestingAQueryWithMaxKeysPaging()
        {
            QueryObjectsRequest query_request = new QueryObjectsRequest()
            {
                BucketName = temp_bucket,
                Query = "x-amz-meta-decimalvalue>4",
                MaxKeys = 1
            };

            bool more = true;
            while (more)
            {
                var response = client.QueryObjects(query_request);
                Assert.AreEqual(temp_bucket, response.BucketName);
                Assert.AreEqual(1, response.ObjectMatches.Count);

                if (response.IsSetNextMarker())
                {
                    query_request.Marker = response.NextMarker;
                }
                else
                {
                    more = false;
                }
            }
        }

        [TestMethod]
        public void TestingAQueryWithVersioning()
        {
            string bucket = Guid.NewGuid().ToString();

            PutBucketRequestECS pbr = new PutBucketRequestECS()
            {
                BucketName = bucket,
            };

            pbr.SetMetadataSearchKeys(bucketMetadataSearchKeys);

            client.PutBucket(pbr);

            PutBucketVersioningRequest pbv = new PutBucketVersioningRequest()
            {
                BucketName = bucket,
                VersioningConfig = new S3BucketVersioningConfig()
                {
                    Status = VersionStatus.Enabled
                }
            };

            client.PutBucketVersioning(pbv);

            for (int i = 0; i < 5; i++)
            {
                PutObjectRequest object_request = new PutObjectRequest()
                {
                    BucketName = bucket,
                    Key = string.Format("obj-{0}", i),
                    ContentBody = string.Format("This is sample content for object {0}.", i)
                };

                object_request.Metadata.Add("x-amz-meta-decimalvalue", Convert.ToString(i * 2));
                object_request.Metadata.Add("x-amz-meta-stringvalue", string.Format("sample-{0}", Convert.ToString(i)));

                client.PutObject(object_request);

                object_request.ContentBody = string.Format("This is sample content for object {0} after versioning has been enabled.", i);

                client.PutObject(object_request);
            }

            QueryObjectsRequest qor = new QueryObjectsRequest()
            {
                BucketName = bucket,
                IncludeOlderVersions = true,
                Query = "x-amz-meta-decimalvalue>4"
            };

            var qor_respose = client.QueryObjects(qor);

            Assert.IsNotNull(qor_respose.ObjectMatches);
            Assert.AreEqual(4, qor_respose.ObjectMatches.Count);
            Assert.AreEqual(bucket, qor_respose.BucketName);

            CleanBucket(bucket);

            client.DeleteBucket(bucket);
        }



        private static bool CleanBucket(string bucket)
        {
            bool moreRecords = true;
            string nextMarker = string.Empty;

            while (moreRecords)
            {
                ListVersionsRequest request = new ListVersionsRequest()
                {
                    BucketName = bucket,
                };

                if (nextMarker.Length > 0)
                    request.KeyMarker = nextMarker;

                ListVersionsResponse response = new ListVersionsResponse();
                response = client.ListVersions(request);


                foreach (S3ObjectVersion theObject in response.Versions)
                {
                    client.DeleteObject(new DeleteObjectRequest()
                    {
                        BucketName = bucket,
                        Key = theObject.Key,
                        VersionId = theObject.VersionId
                    });
                }

                if (response.IsTruncated)
                    nextMarker = response.NextKeyMarker;
                else
                    moreRecords = false;
            }

            return true;
        }



        [TestMethod]
        public void TestingPutBucketWithExtensions()
        {
            string bucket = Guid.NewGuid().ToString();

            PutBucketRequestECS request = new PutBucketRequestECS()
            {
                BucketName = bucket,
                RetentionPeriod = 180,
                StaleReadAllowed = true,
                EnableCompliance = true,
                EnableServerSideEncryption = true,
                Namespace = ConfigurationManager.AppSettings["ECS_NAMESPACE"]
            };

            Assert.IsNotNull(client.PutBucket(request));

            DeleteBucketRequest dbr = new DeleteBucketRequest()
            {
                BucketName = bucket
            };

            Assert.IsNotNull(client.DeleteBucket(dbr));

        }
    }
}