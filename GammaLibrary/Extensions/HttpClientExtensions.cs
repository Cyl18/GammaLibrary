﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable AssignNullToNotNullAttribute

namespace GammaLibrary.Extensions
{
    // todo static httpclient
    public static class HttpClientExtensions
    {

        public static async Task DownloadAsync(this HttpClient client, string requestUri, string destination,
            IProgress<double>? progress = default, CancellationToken cancellationToken = default)
        {
            if (destination is null) throw new ArgumentNullException(nameof(destination));

            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(destination)) ??
                                      throw new InvalidOperationException($"Invalid destination: {destination}"));
            using var fs = File.OpenWrite(destination);
            await client.DownloadAsync(requestUri, fs, progress, cancellationToken);
        }


        public static async Task DownloadAsync(this HttpClient client, string requestUri, Stream destination,
            IProgress<double>? progress = default, CancellationToken cancellationToken = default)
        {
            using var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            var contentLength = response.Content.Headers.ContentLength;
            response.EnsureSuccessStatusCode();

            using var download = await response.Content.ReadAsStreamAsync();
            if (progress == null || !contentLength.HasValue)
            {
                await download.CopyToAsync(destination);
                return;
            }

            var relativeProgress = new Progress<long>(totalBytes => progress.Report((float)totalBytes / contentLength.Value));
            await download.CopyToAsync(destination, 81920, relativeProgress, cancellationToken);
            progress.Report(1);
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize = 8192, IProgress<long>? progress = default, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!source.CanRead)
                throw new ArgumentException("Has to be readable", nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (!destination.CanWrite)
                throw new ArgumentException("Has to be writable", nameof(destination));
            if (bufferSize < 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead);
            }
        }

        public static async Task<string> PostStringAsync(this HttpClient client, string address, HttpContent data)
        {
            var response = await client.PostAsync(address, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // TODO 这里 .NET 5 加了新的 API 考虑删除
        public static Task<string> PostStringAsync(this HttpClient client, string address, Dictionary<string, string?> data)
        {
            // 这里上面加了问号要warning 删了问号也要warning...
#pragma warning disable CS8620
            return client.PostStringAsync(address, new FormUrlEncodedContent(data));
#pragma warning restore CS8620
        }

        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string address)
        {
            var str = await client.GetStringAsync(address);
            return str.JsonDeserialize<T>();
        }

        public static async Task<T> PostJsonAsync<T>(this HttpClient client, string address, HttpContent data)
        {
            var response = await client.PostAsync(address, data);
            response.EnsureSuccessStatusCode();
            var str = await response.Content.ReadAsStringAsync();
            return str.JsonDeserialize<T>();
        }

        public static Task<T> PostJsonAsync<T>(this HttpClient client, string address, Dictionary<string, string?> data)
        {
#pragma warning disable CS8620
            return client.PostJsonAsync<T>(address, new FormUrlEncodedContent(data));
#pragma warning restore CS8620
        }
    }

    public class HttpForm : Dictionary<string, string>
    {
    }
}
