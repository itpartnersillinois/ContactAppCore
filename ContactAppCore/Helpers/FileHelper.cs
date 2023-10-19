using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ContactAppCore.Helpers {

    public class FileHelper {
        private readonly string _accessKey;

        private readonly string _bucketName;

        private readonly string _fileSuffix;

        private readonly string _secretKey;

        private readonly string _urlPath;

        public FileHelper(string accessKey, string bucketName, string secretKey, string urlPath, string fileSuffix) {
            _accessKey = accessKey;
            _bucketName = bucketName;
            _fileSuffix = fileSuffix.Trim();
            _secretKey = secretKey;
            _urlPath = urlPath;
        }

        public async Task<string> AddCv(Stream stream, string name, string file) => await AddFile(stream, "profile_cv", name, file);

        public string AddPhoto(Stream stream, string name, string file, int height, int width, int minimumHeight, int minimumWidth, out string errorMessage) {
            var extension = Path.GetExtension(file).ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(extension) || !PhotoHelper.MimeTypes.ContainsKey(extension)) {
                errorMessage = "Error: File does not have a proper extension";
                return "";
            }
            if (minimumHeight > 0 && minimumWidth > 0 && height == minimumHeight && width == minimumWidth) {
                // fixed size, do not deviate from this
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                var img = Image.Load(memoryStream.ToArray());
                if (img.Width == minimumWidth || img.Height == minimumHeight) {
                    errorMessage = $"Error: File does not match dimensions {minimumWidth} width and {minimumHeight} height (actual file is {img.Width}x{img.Height})";
                    return "";
                }
            } else if (height > 0 && width > 0 && minimumHeight > 0 && minimumWidth > 0) {
                // scale down and crop to maximum size, do not accept below minimum size
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                var img = Image.Load(memoryStream.ToArray());
                if (img.Width < minimumWidth || img.Height < minimumHeight) {
                    errorMessage = $"Error: File does not match minimum dimensions {minimumWidth} width and {minimumHeight} height (actual file is {img.Width}x{img.Height})";
                    return "";
                }
                if (img.Width > width || img.Height > height) {
                    img.Mutate(i => i.Resize(width, 0));
                    if (img.Height > height) {
                        img.Mutate(i => i.Crop(new Rectangle(0, (img.Height - height) / 2, width, height)));
                        using var memoryStreamWebP = new MemoryStream();
                        img.SaveAsWebp(memoryStreamWebP);
                        errorMessage = "";
                        return AddFile(memoryStreamWebP, "profile_photo", name, Path.GetFileNameWithoutExtension(file) + ".webp").Result;
                    } else {
                        errorMessage = $"Error: File does not match dimensions and cannot be cropped (actual file is {img.Width} width by {img.Height} height)";
                        return "";
                    }
                }
            }
            errorMessage = "";
            return AddFile(stream, "profile_photo", name, file).Result;
        }

        public async Task<string> DeleteCv(string name) => await DeleteFiles(name.Replace("@illinois.edu", ""), "profile_cv", PhotoHelper.DocumentExtensions);

        public async Task<string> DeletePhoto(string name) => await DeleteFiles(ProcessName(name), "profile_photo", PhotoHelper.MimeTypes.Keys);

        private async Task<string> AddFile(Stream stream, string path, string name, string file) {
            var key = $"{path}{_fileSuffix}/{ProcessName(name) + Path.GetExtension(file)}";
            try {
                using (var client = new AmazonS3Client(new BasicAWSCredentials(_accessKey, _secretKey), RegionEndpoint.USEast2)) {
                    var transfer = new TransferUtility(client);
                    var uploadRequest = new TransferUtilityUploadRequest { InputStream = stream, BucketName = _bucketName, CannedACL = S3CannedACL.PublicRead, Key = key };
                    await transfer.UploadAsync(uploadRequest);
                }
                return _urlPath + "/" + key;
            } catch (Exception e) {
                return e.Message;
            }
        }

        private async Task<string> DeleteFiles(string name, string path, IEnumerable<string> extensions) {
            var key = $"{path}{_fileSuffix}/{name}";
            try {
                using (var client = new AmazonS3Client(new BasicAWSCredentials(_accessKey, _secretKey), RegionEndpoint.USEast2)) {
                    foreach (var e in extensions) {
                        await client.DeleteObjectAsync(new DeleteObjectRequest { BucketName = _bucketName, Key = key + e });
                    }
                }
                return string.Empty;
            } catch (Exception e) {
                return e.Message;
            }
        }

        private string ProcessName(string name) => name.Replace("@illinois.edu", "");
    }
}