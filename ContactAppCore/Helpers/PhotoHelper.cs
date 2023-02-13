using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppCore.Helpers {

    public class PhotoHelper {

        public static readonly List<string> DocumentExtensions = new List<string> {
            ".pdf", ".doc", ".docx", ".rtf"
        };

        public static readonly Dictionary<string, string> MimeTypes = new Dictionary<string, string> {
            {  ".gif", "image/gif"},
            {  ".giff", "image/gif"},
            {  ".jpg", "image/jpeg"},
            {  ".jpeg", "image/jpeg"},
            {  ".png", "image/png"},
            {  ".webp", "image/webp"}
        };

        private static readonly string notFound = "https://ed-resources-images.s3.us-east-2.amazonaws.com/profile_photo/_unknown.jpg";
        private HttpClient wc = new HttpClient();

        public static string DetermineImage(string photoUrl) => string.IsNullOrWhiteSpace(photoUrl) ? notFound : photoUrl;

        public async Task<IActionResult> GetPhoto(string photoUrl) {
            var url = DetermineImage(photoUrl);
            var extension = Path.GetExtension(url);
            var stream = await wc.GetStreamAsync(url);
            return new FileStreamResult(stream, MimeTypes[extension]);
        }
    }
}