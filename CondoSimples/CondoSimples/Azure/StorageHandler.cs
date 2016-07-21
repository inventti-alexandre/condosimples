using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CondoSimples.Azure
{
    public static class StorageHandler
    {
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
        static CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        static CloudBlobContainer container = blobClient.GetContainerReference("fotostg");

        public static void UploadImage(string idImage, HttpPostedFileBase file, string prefix)
        {
            string[] extension = file.FileName.Split('.');

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(prefix + idImage + "." + extension[extension.Length - 1]);
            blockBlob.UploadFromStream(file.InputStream);
        }

        public static Uri GetImageUri(string idImage)
        {
            var img = container.GetBlockBlobReference(idImage);
            return img.Uri;
        }
    }
}
