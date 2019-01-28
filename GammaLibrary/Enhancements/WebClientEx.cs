using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using GammaLibrary.Extensions;

namespace GammaLibrary.Enhancements
{
    public class WebClientEx : WebClient
    {
        private IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progressHandler;
        public CookieContainer CookieContainer { get; set; } = new CookieContainer();
        public bool AutoRetry { get; set; } = true;
        public int MaxRetries { get; set; } = 3;

        public WebClientEx()
        {
            Encoding = Encoding.UTF8;
        }

        public WebClientEx(IProgress<(long bytesReceived, int percentage, long bytesToReceive)> progress) : this()
        {
            progressHandler = progress;
            DownloadProgressChanged += (s, e) => progress.Report((e.BytesReceived, e.ProgressPercentage, e.TotalBytesToReceive));
        }

        public WebClientEx(CookieContainer container) : this()
        {
            CookieContainer = container;
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
