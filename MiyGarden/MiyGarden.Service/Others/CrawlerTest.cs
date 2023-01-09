using MiyGarden.Models.Enums;
using MiyGarden.Service.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
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
            return await Task.Run(() =>
            {
                var pageSource = string.Empty;
                try
                {
                    this.OnStart?.Invoke(this, new OnStartEvent(uri));
                    var watch = new Stopwatch();
                    watch.Start();
                    var request = (HttpWebRequest)WebRequest.Create(uri);
                    request.Accept = "*/*";
                    request.ContentType = MediaType.ApplicationUrlencoded.GetDescription();
                    request.AllowAutoRedirect = false;
                    //User-Agent
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36";
                    request.Timeout = 5000;
                    request.KeepAlive = true;
                    request.Method = "GET";
                    if (proxy != null)
                        request.Proxy = proxy;
                    request.CookieContainer = this.CookieContainer;
                    request.ServicePoint.ConnectionLimit = int.MaxValue;
                    var response = (HttpWebResponse)request.GetResponse();
                    foreach (Cookie item in response.Cookies)//登錄？
                    {
                        this.CookieContainer.Add(item);
                    }
                    var stream = response.GetResponseStream();
                    var reader = new StreamReader(stream, Encoding.UTF8);
                    pageSource = reader.ReadToEnd();
                    watch.Stop();
                    var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    var millisends = watch.ElapsedMilliseconds;
                    reader.Close();
                    stream.Close();
                    request.Abort();
                    response.Close();
                    this.OnCompleted?.Invoke(this, new OnCompletedEvent(uri, threadId, millisends, pageSource));
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
