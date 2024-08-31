// Copyright � 2017 Valdis Iljuconoks.
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Concurrent;
using DbLocalizationProvider.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace DbLocalizationProvider.AspNetCore.Cache
{
    public class InMemoryCacheManager : ICacheManager
    {
        internal static readonly ConcurrentDictionary<string, bool> Entries = new ConcurrentDictionary<string, bool>();
        private readonly IMemoryCache _memCache;

        public InMemoryCacheManager(IMemoryCache memCache)
        {
            _memCache = memCache;
        }

        public void Insert(string key, object value)
        {
            _memCache.Set(key, value);
            Entries.TryAdd(key, true);
        }

        public object Get(string key)
        {
            return _memCache.Get(key);
        }

        public void Remove(string key)
        {
            _memCache.Remove(key);
            Entries.TryRemove(key, out var _);
        }

        public event CacheEventHandler OnInsert;
        public event CacheEventHandler OnRemove;
    }
}
