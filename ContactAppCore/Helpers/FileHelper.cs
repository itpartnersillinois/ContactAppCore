using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ContactAppCore.Helpers
{
    public class FileHelper
    {
        private string accessKey;

        private string bucketName;

        private string secretKey;

        private string urlPath;

        public FileHelper(string accessKey, string bucketName, string secretKey, string urlPath)
        {
            this.accessKey = accessKey;
            this.bucketName = bucketName;
            this.secretKey = secretKey;
            this.urlPath = urlPath;
        }

        public async Task<string> AddCv(Stream stream, string name, string file)
        {
            return await AddFile(stream, "profile_cv", name, file);
        }

        public async Task<string> AddPhoto(Stream stream, string name, string file)
        {
            var img = Image.FromStream(stream);
            if (img.Width == 244 && img.Height == 292)
            {
                return await AddFile(stream, "profile_photo", name, file);
            }
            return "";
        }

        public async Task<string> DeleteCv(string name)
        {
            name = name.Replace("@illinois.edu", "");
            return await DeleteFiles(name, "profile_cv", new string[] { ".pdf", ".doc", ".docx", ".rtf" });
        }

        public async Task<string> DeletePhoto(string name)
        {
            return await DeleteFiles(name, "profile_photo", new string[] { ".png", ".gif", ".jpg", ".jpeg" });
        }

        private async Task<string> AddFile(Stream stream, string path, string name, string file)
        {
            var key = $"{path}/{name.Replace("@illinois.edu", "") + Path.GetExtension(file)}";
            try
            {
                using (var client = new AmazonS3Client(new BasicAWSCredentials(this.accessKey, this.secretKey), RegionEndpoint.USEast2))
                {
                    var transfer = new TransferUtility(client);
                    var uploadRequest = new TransferUtilityUploadRequest { InputStream = stream, BucketName = this.bucketName, CannedACL = S3CannedACL.PublicRead, Key = key };
                    await transfer.UploadAsync(uploadRequest);
                }
                return urlPath + "/" + key;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private async Task<string> DeleteFiles(string name, string path, IEnumerable<string> extensions)
        {
            var key = $"{path}/{name}";
            try
            {
                using (var client = new AmazonS3Client(new BasicAWSCredentials(this.accessKey, this.secretKey), RegionEndpoint.USEast2))
                {
                    foreach (var e in extensions)
                    {
                        await client.DeleteObjectAsync(new DeleteObjectRequest { BucketName = bucketName, Key = key + e });
                    }
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}