using System;
using System.IO;

namespace BusinessLayer {
    public static class ProfileImageManager {
        private static readonly string ImagesFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Profiles");

        static ProfileImageManager() {
            Directory.CreateDirectory(ImagesFolder);
        }

        public static string SaveImage(string sourceFilePath) {
            if (string.IsNullOrWhiteSpace(sourceFilePath))
                return string.Empty;

            string extension = Path.GetExtension(sourceFilePath);

            string fileName =
                $"{Guid.NewGuid()}{extension}";

            string destinationPath =
                Path.Combine(ImagesFolder, fileName);

            File.Copy(sourceFilePath, destinationPath, true);

            return Path.Combine("Images", "Profiles", fileName);
        }

        public static bool DeleteImage(string relativePath) {
            if (string.IsNullOrWhiteSpace(relativePath))
                return false;

            string fullPath =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

            if (!File.Exists(fullPath))
                return false;

            File.Delete(fullPath);

            return true;
        }

        public static string ReplaceImage(string oldRelativePath,
                                            string newSourceFilePath) {
            DeleteImage(oldRelativePath);

            return SaveImage(newSourceFilePath);
        }
    }
}
