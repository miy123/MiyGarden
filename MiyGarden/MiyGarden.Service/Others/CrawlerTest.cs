using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiyGarden.Service.Others
{
    public class Crawler
    {
        public event EventHandler<OnStartEvent> OnStart;
        public event EventHandler<OnCompletedEvent> OnCompleted;
        public event EventHandler<Exception> OnError;
        public CookieContainer CookieContainer { set; get; }
        public async Task<string> Start(Uri uri, WebProxy proxy = null)
        {
            return await Task.Run(async () =>
            {
                var pageSource = string.Empty;
                try
                {
                    this.OnStart?.Invoke(this, new OnStartEvent(uri));
                    var watch = new Stopwatch();
                    watch.Start();
                    var handler = new HttpClientHandler()
                    {
                        UseCookies = true,
                        CookieContainer = new CookieContainer(),
                        AllowAutoRedirect = false,
                    };

                    if (proxy != null)
                    {
                        handler.Proxy = proxy;
                        handler.UseProxy = true;
                    }

                    var client = new HttpClient(handler)
                    {
                        Timeout = TimeSpan.FromMilliseconds(5000),
                    };

                    var request = new HttpRequestMessage(HttpMethod.Get, uri);
                    request.Headers.Add("Accept", "*/*");
                    request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36");
                    request.Headers.Add("Connection", "keep-alive");
                    request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    // 儲存 Cookie
                    foreach (Cookie cookie in handler.CookieContainer.GetCookies(uri))
                    {
                        handler.CookieContainer.Add(cookie);
                    }

                    pageSource = await response.Content.ReadAsStringAsync();
                    watch.Stop();
                    var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    var milliseconds = watch.ElapsedMilliseconds;

                    this.OnCompleted?.Invoke(this, new OnCompletedEvent(uri, threadId, milliseconds, pageSource));
                }
                catch (Exception ex)
                {
                    this.OnError?.Invoke(this, ex);
                }
                return pageSource;
            });
        }
    }

    public class OnStartEvent
    {
        public Uri Uri { set; get; }

        public OnStartEvent(Uri uri)
        {
            this.Uri = uri;
        }
    }

    public class OnCompletedEvent
    {
        public Uri Uri { set; get; }
        public int ThreadId { set; get; }
        public string PageSource { set; get; }
        public long MilliSeconds { set; get; }
        public OnCompletedEvent(Uri uri, int threadId, long milliSeconds, string pageSource)
        {
            this.Uri = uri;
            this.ThreadId = threadId;
            this.MilliSeconds = milliSeconds;
            this.PageSource = pageSource;
        }
    }
}
