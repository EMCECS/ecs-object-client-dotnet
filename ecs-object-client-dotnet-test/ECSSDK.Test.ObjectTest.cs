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
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ECSSDK.Test
{
    [TestClass]
    public class ObjectTest
    {
        /// <summary>
        /// The S3 client -handles object api interactions.
        ///</summary>
        static ECSS3Client client;

        /// <summary>
        /// Generic bucket name used for bucket API testing.
        ///</summary>
        static string temp_bucket = Guid.NewGuid().ToString();

        [ClassInitialize()]
        public static void Initialize(TestContext testContext)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

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

            client.PutBucket(request);
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
        public void TestUpdateObjectWithRange()
        {
            string key = "key-1";
            string content = "The cat crossed the road.";
            int offset = content.IndexOf("cat");

            PutObjectRequestECS por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                ContentBody = content
            };

            // create the object
            client.PutObject(por);

            string updatePart = "dog";

            por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                Range = Range.fromOffsetLength(offset, updatePart.Length),
                ContentBody = updatePart
            };

            // update the object
            client.PutObject(por);

            // verify update
            GetObjectResponse response = client.GetObject(temp_bucket, key);
            Stream responseStream = response.ResponseStream;
            StreamReader reader = new StreamReader(responseStream);
            string readContent = reader.ReadToEnd();
            Assert.AreEqual("The dog crossed the road.", readContent);

            updatePart = "very lucky animal crossed the road.";

            por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                Range = Range.fromOffset(offset),
                ContentBody = updatePart
            };

            client.PutObject(por);

            // verify update
            response = client.GetObject(temp_bucket, key);
            responseStream = response.ResponseStream;
            reader = new StreamReader(responseStream);
            readContent = reader.ReadToEnd();
            Assert.AreEqual(content.Substring(0, offset) + updatePart, readContent);

        }

        [TestMethod]
        public void TestAppendObject()
        {
            string key = "key-1";
            string content = "What goes up";

            PutObjectRequestECS por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                ContentBody = content
            };

            // create the object
            client.PutObject(por);

            // append to the object
            long result = client.AppendObject(temp_bucket, key, " must come down.");

            // verify the append worked
            GetObjectResponse response = client.GetObject(temp_bucket, key);
            Stream responseStream = response.ResponseStream;
            StreamReader reader = new StreamReader(responseStream);
            string readContent = reader.ReadToEnd();
            Assert.AreEqual("What goes up must come down.", readContent);
            Assert.AreEqual(content.Length, result);
        }

        [TestMethod]
        public void TestConditionalObject()
        {
            string key = "key-1";
            string content = "testing a conditional PUT";

            DateTime in_the_past = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddMinutes(-5));
            DateTime in_the_future = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddMinutes(10));

            PutObjectRequestECS por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                ContentBody = content,
                ContentType = "text/plain"
            };

            PutObjectResponse response = client.PutObject(por);

            string eTag = response.ETag;

            por.UnmodifiedSinceDate = in_the_past;
            try
            {
                client.PutObject(por);
                Assert.Fail("Expected 412 response code");
            } catch (AmazonS3Exception e)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.PreconditionFailed, e.StatusCode);
            }

            // clear out unmodified
            por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                ContentBody = content,
                ContentType = "text/plain"
            };

            // set same stamp as modified - test
            por.ModifiedSinceDate = in_the_past;
            client.PutObject(por);

            por.ModifiedSinceDate = in_the_future;
            try {
                client.PutObject(por);
                Assert.Fail("Expected 412 response code");
            } catch (AmazonS3Exception e) {
                Assert.AreEqual(System.Net.HttpStatusCode.PreconditionFailed, e.StatusCode);
            }

            // clear out modified
            por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                ContentBody = content,
                ContentType = "text/plain"
            };

            // if etag match - pass
            por.EtagToMatch = eTag;
            client.PutObject(por);

            por.EtagToMatch = null;

            // if etag doesn't match - fail
            por.EtagToNotMatch = eTag;
            try
            {
                client.PutObject(por);
                Assert.Fail("Expected 412 response code");
            }
            catch (AmazonS3Exception e)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.PreconditionFailed, e.StatusCode);
            }

            eTag = "0f7373bfe4bda6531b15229e9b9e8f75";

            // if etag doesn't match - pass
            por.EtagToNotMatch = eTag;
            client.PutObject(por);
            por.EtagToNotMatch = null;

            // if etag to match - fail
            por.EtagToMatch = eTag;
            try
            {
                client.PutObject(por);
                Assert.Fail("Expected 412 response code");
            }
            catch (AmazonS3Exception e)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.PreconditionFailed, e.StatusCode);
            }

            por.EtagToMatch = null;            

            // if match * (if the key exists, i.e. update only) pass
            por.EtagToMatch = "*";
            client.PutObject(por);

            por.EtagToMatch = null;

            // test if-none-match * (if key is new, i.e. create only) fail
            por.EtagToNotMatch = "*";
            try
            {
                client.PutObject(por);
                Assert.Fail("Expected 412 response code");
            }
            catch (AmazonS3Exception e)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.PreconditionFailed, e.StatusCode);
            }

            por.Key = "key-2";

            // if-non-match * (if the key is new i.e. create only) pass
            client.PutObject(por);

            por.EtagToNotMatch = null;

            por.Key = "key-3";

            // if-match * (if the key exists i.e. update only) fail
            por.EtagToMatch = "*";
            try
            {
                //client.PutObject(por); STORAGE - 14736 
                //Assert.Fail("Expected 412 response code");  STORAGE - 14736
            }
            catch (AmazonS3Exception e)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.PreconditionFailed, e.StatusCode);
            }
        }

        [TestMethod]
        public void TestObjectRetentionPeriod()
        {
            string key = "retention-1";
            string content = "sample retention content ...";

            PutObjectRequestECS por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                RetentionPeriod = 5,
                ContentBody = content
            };

            client.PutObject(por);

            try
            {
                client.AppendObject(temp_bucket, key, "nogonna work her no more ...");
                Assert.Fail("Allowed to update an object under retention");
            } catch (AmazonS3Exception e)
            {
                Assert.AreEqual("ObjectUnderRetention", e.ErrorCode);
            }

            System.Threading.Thread.Sleep(5000);

            client.AppendObject(temp_bucket, key, "update now works ...");
        }

        [TestMethod]
        public void TestObjectRetentionPolicy()
        {
            string key = "retention-1";
            string content = "sample retention content ...";

            PutObjectRequestECS por = new PutObjectRequestECS()
            {
                BucketName = temp_bucket,
                Key = key,
                RetentionPolicy = "hold-me",
                ContentBody = content
            };

            client.PutObject(por);
        }

        [TestMethod]
        public void TestStandardAwsStuff()
        {

            ListObjectsRequest request = new ListObjectsRequest()
            {
                BucketName = temp_bucket,
                Prefix = "folder1/sub2/",
                Delimiter = "/",
                MaxKeys = 1
            };

            client.ListObjects(request);

            InitiateMultipartUploadRequest initRequest = new InitiateMultipartUploadRequest()
            {
                BucketName = temp_bucket,
                Key = "mpu-1",
            };

            InitiateMultipartUploadResponse initResponse = client.InitiateMultipartUpload(initRequest);

            client.AbortMultipartUpload(new AbortMultipartUploadRequest()
            {
                BucketName = temp_bucket,
                Key = "mpu-1",
                UploadId = initResponse.UploadId
            });

        }

        [TestMethod]
        public void TestObjectWithMetadata()
        {
            string key = "meta-1";
            string content = "sample object data content ...";

            PutObjectRequest por = new PutObjectRequest()
            {
                BucketName = temp_bucket,
                Key = key,
                ContentBody = content,
            };

            por.Metadata.Add("555", "55555");
            por.Metadata.Add("bbb", "bbbbb");
            por.Metadata.Add("b_b_b", "bubub");
            por.Metadata.Add("aaa", "aaaaa");
            por.Metadata.Add("a_a_a", "auaua");
            por.Metadata.Add("111", "11111");

            PutObjectResponse response = client.PutObject(por);
            Assert.AreEqual(response.HttpStatusCode, System.Net.HttpStatusCode.OK);

        }
    }
}
