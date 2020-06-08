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
    public class BucketTest
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
            BasicAWSCredentials creds = new BasicAWSCredentials(ConfigurationManager.AppSettings["S3_ACCESS_KEY_ID"], ConfigurationManager.AppSettings["S3_SECRET_KEY"]);

            AmazonS3Config cc = new AmazonS3Config()
            {
                ForcePathStyle = true,
                ServiceURL = ConfigurationManager.AppSettings["S3_ENDPOINT"],
                SignatureVersion = ConfigurationManager.AppSettings["SIGNATURE_VERSION"],
                SignatureMethod = SigningAlgorithm.HmacSHA1,
                UseHttp = false,
            };
            client = new ECSS3Client(creds, cc);
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
        public void TestPutBucketFileSystemEnabled()
        {
            PutBucketRequestECS request = new PutBucketRequestECS()
            {
                BucketName = temp_bucket,
                EnableFileSystem = true
            };

            PutBucketResponseECS response = client.PutBucket(request);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void TestPutBucketFileVPoolID()
        {
            PutBucketRequestECS request = new PutBucketRequestECS()
            {
                BucketName = temp_bucket,
                VirtualPoolId = ConfigurationManager.AppSettings["VPOOL_ID"]
            };

            PutBucketResponseECS response = client.PutBucket(request);
            Assert.IsNotNull(response);
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