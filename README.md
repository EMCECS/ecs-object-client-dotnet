# ECS Object Client .NET

ECS Object Client .NET is an S3 client that incorporates all the S3 API extensions only available in ECS including metadata search, object updates, retention and more.

## Installing

Our .NET SDK does not include any binaries, therefore the .dlls must be downloaded and compiled in Visual Studio before being used as a reference for any project. In order to reference the .dlls in your project:

* Download or clone this repository 
* Open .sln file in Visual Studio
* Build solution
* Import binaries to current project

## Included APIs

* AppendObject()
* PutBucket() (with ECS features)
* QueryObjects() (with ECS features)
* ListBucketMetaSearchKeys()
* ListSystemMetaSearchKeys()

## License

Copyright 2017 EMC Corporation.  All Rights Reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
