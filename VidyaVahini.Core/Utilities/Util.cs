using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VidyaVahini.Core.Utilities
{
    public static class Util
    {
        public static string ApplyReplacements(string emailBody, Dictionary<string, string> replacements)
        {
            if (replacements != null && replacements.Any() && !string.IsNullOrEmpty(emailBody))
            {
                foreach (var replacement in replacements)
                {
                    emailBody = emailBody.Replace(replacement.Key, replacement.Value);
                }
            }
            return emailBody ?? string.Empty;
        }

        public static string GetPasswordHash(string passwordSalt, string password)
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(password + passwordSalt));

        public static string GetMedia(int? mediaTypeId, string source)
        {
            switch (mediaTypeId)
            {
                case 1:
                    return GetMediaFromFileSystem(source);

                case 2:
                    return GetYouTubeUrl(source);

                case 3:
                    return GetImageUrl(source);

                default:
                    return string.Empty;
            }
        }
        private static string GetMediaFromFileSystem(string source)
        {
            string outputStream = null;
            if (string.IsNullOrWhiteSpace(source))
                return outputStream;

            if (File.Exists(source))
            {
                using (MemoryStream responseStream = new MemoryStream())
                {
                    using (Stream fileStream = File.Open(source, FileMode.Open))
                    {
                        fileStream.CopyTo(responseStream);
                        fileStream.Close();
                        responseStream.Position = 0;
                        byte[] bytes = responseStream.ToArray();
                        string base64 = Convert.ToBase64String(bytes);
                        outputStream = base64;
                    }
                }
            }
            return outputStream;
        }


        private static string GetYouTubeUrl(string source)
            => source;
        private static string GetImageUrl(string source)
           => source;

        public static string SaveMediaToFileSystem(string mediaBase64String, string mediaPath, string mediaFileName)
        {
            if (string.IsNullOrWhiteSpace(mediaBase64String) || string.IsNullOrWhiteSpace(mediaPath) || string.IsNullOrWhiteSpace(mediaFileName))
            {
                return null;
            }

            if (!Directory.Exists(mediaPath))
            {
                Directory.CreateDirectory(mediaPath);
            }

            string mediafilePath = $@"{mediaPath}/{mediaFileName}";

            File.WriteAllBytes(mediafilePath, Convert.FromBase64String(mediaBase64String));

            return mediafilePath;
        }

        public static bool DeleteMediaFromFileSystem(string mediaFilePath)
        {
            if (File.Exists(mediaFilePath))
            {
                File.Delete(mediaFilePath);
                return true;
            }

            else
            {
                return false;
            }
        }

        public static void DeleteDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }

        public static Stream ToStream(this string mediaString)
        {
            if (string.IsNullOrEmpty(mediaString))
                return null;
            byte[] byteArray = Encoding.ASCII.GetBytes(mediaString);

            return byteArray.ToStream();
        }
        public static byte[] ToByteArray(this string mediaString)
        {
            if (string.IsNullOrEmpty(mediaString))
                return null;
            byte[] byteArray = Encoding.ASCII.GetBytes(mediaString);

            return byteArray;
        }
        public static Stream ToStream(this byte[] mediaArray)
        {
            if (mediaArray != null && mediaArray.Length > 0)
            {
                Stream stream = new MemoryStream(mediaArray);
                return stream;
            }
            return null;
        }
        public static string ToString(this MemoryStream stream)
        {
            byte[] bytes = stream.ToArray();
            return Convert.ToBase64String(bytes);
        }
        public static string GetNewGuid(string FileName)
        {
            return Guid.NewGuid().ToString();
        }

        public static DateTime GetIstDateTime()
        {
            return DateTime.UtcNow.AddHours(5.5);
        }


    }
}
