using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoSimples.Azure
{
    public static class StorageHandler
    {
        static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
        static CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        static CloudBlobContainer container = blobClient.GetContainerReference("fotostg");

        public static void UploadImage(string idImage, byte[] image)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(idImage);
            blockBlob.UploadFromByteArray(image, 0, image.Length);
        }

        public static Uri GetImageUri(string idImage)
        {
            var img = container.GetBlockBlobReference(idImage);
            return img.Uri;
        }
    }
}
