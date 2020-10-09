using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace PodeKestrel
{
    public class PodeResponseHeaders
    {
        public string this[string name]
        {
            get => (Headers.ContainsKey(name) ? (string)Headers[name] : string.Empty);
            set => Set(name, value);
        }

        public int Count => Headers.Count;
        public ICollection<string> Keys => Headers.Keys;

        private IHeaderDictionary Headers;

        public PodeResponseHeaders(IHeaderDictionary headers)
        {
            Headers = headers;
        }

        public bool ContainsKey(string name)
        {
            return Headers.ContainsKey(name);
        }

        public StringValues Get(string name)
        {
            return Headers.ContainsKey(name) ? Headers[name] : default(StringValues);
        }

        public void Set(string name, string value)
        {
            if (Headers.ContainsKey(name))
            {
                Headers.Remove(name);
            }

            Headers.Add(name, value);
        }

        public void Add(string name, string value)
        {
            Headers.Append(name, value);
        }

        public void Remove(string name)
        {
            if (Headers.ContainsKey(name))
            {
                Headers.Remove(name);
            }
        }

        public void Clear()
        {
            Headers.Clear();
        }

    }
}