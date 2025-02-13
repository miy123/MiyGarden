using System;
using System.Collections.Generic;

namespace MiyGarden.Service.Algorithm
{
    public class TokenBucketLimiter
    {
        private readonly Dictionary<string, BucketInfo> _buckets = new Dictionary<string, BucketInfo>();

        public int TryAcquireToken(string key, int permits, string launchQueueConsumeKey = null)
        {
            // 取得當前時間（毫秒）
            long currTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            // 從記憶體取得令牌桶資訊
            if (!_buckets.TryGetValue(key, out var bucketInfo))
            {
                return 0; // 若無數據則返回 0
            }

            long? lastUpdateTime = bucketInfo.LastUpdateTime;
            int currTokenCount = bucketInfo.CurrTokenCount;
            int maxCapacity = bucketInfo.MaxCapacity;
            int insertRate = bucketInfo.InsertRate;
            int interval = bucketInfo.Interval;

            int localCurrTokenCount = maxCapacity; // 預設令牌數量為最大容量

            if (lastUpdateTime.HasValue)
            {
                // 計算應該補充的令牌數量
                int reversePermits = (int)(((Math.Abs(currTime - lastUpdateTime.Value)) / interval) * insertRate);
                int expectCurrTokenCount = reversePermits + currTokenCount;
                localCurrTokenCount = Math.Min(expectCurrTokenCount, maxCapacity);

                // 若有補充令牌，則更新最後時間
                if (reversePermits > 0)
                {
                    bucketInfo.LastUpdateTime = currTime;
                }
            }
            else
            {
                // 若 `last_update_time` 不存在，則初始化
                bucketInfo.LastUpdateTime = currTime;
            }

            // 讀取啟動隊列的消耗數量
            int launchQueueConsumeCount = 0;
            int isOverRate = 0;

            if (!string.IsNullOrEmpty(launchQueueConsumeKey) && _buckets.TryGetValue(launchQueueConsumeKey, out var launchQueueInfo))
            {
                launchQueueConsumeCount = launchQueueInfo.ConsumeCount;
                isOverRate = launchQueueInfo.IsOverRate;
            }

            // 判斷是否允許請求
            if (localCurrTokenCount - permits - launchQueueConsumeCount >= 0 && isOverRate == 0)
            {
                bucketInfo.CurrTokenCount = localCurrTokenCount - permits;
                return 1; // 允許請求
            }
            else
            {
                bucketInfo.CurrTokenCount = localCurrTokenCount;
                return -1; // 拒絕請求
            }
        }

        private class BucketInfo
        {
            public long? LastUpdateTime { get; set; }
            public int CurrTokenCount { get; set; }
            public int MaxCapacity { get; set; }
            public int InsertRate { get; set; }
            public int Interval { get; set; }
            public int ConsumeCount { get; set; }
            public int IsOverRate { get; set; }
        }
    }
}
