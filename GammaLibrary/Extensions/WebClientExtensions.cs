using System;
using System.Net;
using System.Threading.Tasks;

namespace GammaLibrary.Extensions
{
    public static class WebClientExtensions
    {
        public static async Task DownloadFileTaskAsync(
            this WebClient webClient,
            string address,
            string fileName,
            IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progress)
        {
            try
            {
                webClient.DownloadProgressChanged += ProgressChangedHandler;
                await webClient.DownloadFileTaskAsync(address, fileName);
            }
            finally
            {
                webClient.DownloadProgressChanged -= ProgressChangedHandler;
            }

            void ProgressChangedHandler(object ps, DownloadProgressChangedEventArgs pe)
            {
                progress.Report((pe.BytesReceived, pe.ProgressPercentage, pe.TotalBytesToReceive));
            }
        }

        public static async Task<string> DownloadStringTaskAsync(
            this WebClient webClient,
            string address,
            IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progress)
        {
            try
            {
                webClient.DownloadProgressChanged += ProgressChangedHandler;
                return await webClient.DownloadStringTaskAsync(address);
            }
            finally
            {
                webClient.DownloadProgressChanged -= ProgressChangedHandler;
            }

            void ProgressChangedHandler(object ps, DownloadProgressChangedEventArgs pe)
            {
                progress.Report((pe.BytesReceived, pe.ProgressPercentage, pe.TotalBytesToReceive));
            }
        }

        public static async Task<byte[]> DownloadDataTaskAsync(
            this WebClient webClient,
            string address,
            IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progress)
        {
            try
            {
                webClient.DownloadProgressChanged += ProgressChangedHandler;
                return await webClient.DownloadDataTaskAsync(address);
            }
            finally
            {
                webClient.DownloadProgressChanged -= ProgressChangedHandler;
            }

            void ProgressChangedHandler(object ps, DownloadProgressChangedEventArgs pe)
            {
                progress.Report((pe.BytesReceived, pe.ProgressPercentage, pe.TotalBytesToReceive));
            }
        }

        public static async Task UploadFileTaskAsync(
            this WebClient webClient,
            string address,
            string fileName,
            IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progress)
        {
            try
            {
                webClient.DownloadProgressChanged += ProgressChangedHandler;
                await webClient.UploadFileTaskAsync(address, fileName);
            }
            finally
            {
                webClient.DownloadProgressChanged -= ProgressChangedHandler;
            }

            void ProgressChangedHandler(object ps, DownloadProgressChangedEventArgs pe)
            {
                progress.Report((pe.BytesReceived, pe.ProgressPercentage, pe.TotalBytesToReceive));
            }
        }

        public static async Task<string> UploadStringTaskAsync(
            this WebClient webClient,
            string address,
            string data,
            IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progress)
        {
            try
            {
                webClient.DownloadProgressChanged += ProgressChangedHandler;
                return await webClient.UploadStringTaskAsync(address, data);
            }
            finally
            {
                webClient.DownloadProgressChanged -= ProgressChangedHandler;
            }

            void ProgressChangedHandler(object ps, DownloadProgressChangedEventArgs pe)
            {
                progress.Report((pe.BytesReceived, pe.ProgressPercentage, pe.TotalBytesToReceive));
            }
        }

        public static async Task<byte[]> UploadDataTaskAsync(
            this WebClient webClient,
            string address,
            byte[] data,
            IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progress)
        {
            try
            {
                webClient.DownloadProgressChanged += ProgressChangedHandler;
                return await webClient.UploadDataTaskAsync(address, data);
            }
            finally
            {
                webClient.DownloadProgressChanged -= ProgressChangedHandler;
            }

            void ProgressChangedHandler(object ps, DownloadProgressChangedEventArgs pe)
            {
                progress.Report((pe.BytesReceived, pe.ProgressPercentage, pe.TotalBytesToReceive));
            }
        }
    }
}