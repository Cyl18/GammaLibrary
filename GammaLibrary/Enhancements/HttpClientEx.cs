using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace GammaLibrary.Enhancements
{
    public class HttpClientEx : HttpClient
    {
        public HttpClientHandler MessageHandler { get; }
        public bool UseProxy
        {
            get => MessageHandler.UseProxy;
            set => MessageHandler.UseProxy = value;
        }

        public IWebProxy Proxy
        {
            get => MessageHandler.Proxy;
            set => MessageHandler.Proxy = value;
        }

        public bool UseCookies
        {
            get => MessageHandler.UseCookies;
            set => MessageHandler.UseCookies = value;
        }

        public CookieContainer CookieContainer
        {
            get => MessageHandler.CookieContainer;
            set => MessageHandler.CookieContainer = value;
        }

        public HttpClientEx()
        {
        }

        public HttpClientEx(HttpMessageHandler handler) : base(handler)
        {
            MessageHandler = handler as HttpClientHandler;
        }

        public HttpClientEx(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
        {
            MessageHandler = handler as HttpClientHandler;
        }
    }
}
