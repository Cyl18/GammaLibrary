﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GammaLibrary.Extensions;
// ReSharper disable AssignNullToNotNullAttribute

namespace GammaLibrary.Enhancements
{
    [Obsolete("This class is going to be deleted.")]
    public class WebClientEx : WebClient
    {
        public CookieContainer CookieContainer { get; set; } = new CookieContainer();
        public bool AutoRetry { get; set; } = true;
        public int MaxRetries { get; set; } = 2;

        public WebClientEx()
        {
            Encoding = Encoding.UTF8;
        }

        public WebClientEx(IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progress) : this()
        {
            DownloadProgressChanged += (s, e) => progress.Report((e.BytesReceived, e.ProgressPercentage, e.TotalBytesToReceive));
        }

        public WebClientEx(CookieContainer container) : this()
        {
            CookieContainer = container;
        }

        // TODO english
        /// <summary>
        /// 下载一个文件.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="fileName">此参数所指定的文件所在的文件夹将被自动创建</param>
        public new void DownloadFile(string address, string fileName)
        {
            fileName = Path.GetFullPath(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
            base.DownloadFile(address, fileName);
        }

        /// <summary>
        /// 下载一个文件.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="fileName">此参数所指定的文件所在的文件夹将被自动创建</param>
        public new void DownloadFile(Uri address, string fileName)
        {
            fileName = Path.GetFullPath(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
            base.DownloadFile(address, fileName);
        }

        public new void DownloadFileAsync(Uri address, string fileName)
        {
            fileName = Path.GetFullPath(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
            base.DownloadFileAsync(address, fileName);
        }

        public new void DownloadFileAsync(Uri address, string fileName, object userToken)
        {
            fileName = Path.GetFullPath(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
            base.DownloadFileAsync(address, fileName, userToken);
        }

        /// <summary>
        /// 下载一个文件.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="fileName">此参数所指定的文件所在的文件夹将被自动创建</param>
        public new Task DownloadFileTaskAsync(string address, string fileName)
        {
            fileName = Path.GetFullPath(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
            return base.DownloadFileTaskAsync(address, fileName);
        }

        /// <summary>
        /// 下载一个文件.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="fileName">此参数所指定的文件所在的文件夹将被自动创建</param>
        public new Task DownloadFileTaskAsync(Uri address, string fileName)
        {
            fileName = Path.GetFullPath(fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName)!);
            return base.DownloadFileTaskAsync(address, fileName);
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var retries = 0;

            while (true)
            {
                try
                {
                    var r = base.GetWebRequest(address);
                    if (r is HttpWebRequest request) request.CookieContainer = CookieContainer;
                    return r;
                }
                catch (WebException)
                {
                    if (!AutoRetry || ++retries == MaxRetries) throw;
                }
            }
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            var retries = 0;

            while (true)
            {
                try
                {
                    var response = base.GetWebResponse(request, result);
                    ReadCookies(response);
                    return response;
                }
                catch (WebException)
                {
                    if (!AutoRetry || ++retries == MaxRetries) throw;
                }
            }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var retries = 0;

            while (true)
            {
                try
                {
                    var response = base.GetWebResponse(request);
                    ReadCookies(response);
                    return response;
                }
                catch (WebException)
                {
                    if (!AutoRetry || ++retries == MaxRetries) throw;
                }
            }
        }

        private void ReadCookies(WebResponse r)
        {
            if (r is HttpWebResponse response)
            {
                var cookies = response.Cookies;
                CookieContainer.Add(cookies);
            }
        }
    }
}
