using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navix.Blazor.WebAssembly
{
    public class NavigationIntent : IReadOnlyDictionary<string, string>
    {
        private readonly IDictionary<string, string> _parameters;

        public NavigationIntent(string path, IDictionary<string, string> parameters)
        {
            Path = path;
            _parameters = parameters;
        }

        public NavigationIntent(string path)
            : this(path, new Dictionary<string, string>())
        {
        }

        public string Path { get; }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _parameters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => _parameters.Count;

        public bool ContainsKey(string key) => _parameters.ContainsKey(key);

        public bool TryGetValue(string key, out string value) => _parameters.TryGetValue(key, out value!);

        public string this[string key] => _parameters[key];

        public IEnumerable<string> Keys => _parameters.Keys;

        public IEnumerable<string> Values => _parameters.Values;

        internal string ToQueryString() => 
            string.Join("&", _parameters
                .Where(it => !string.IsNullOrWhiteSpace(it.Key))
                .Select(it => {
                    var (key, value) = it;
                    var encodedKey = HttpUtility.UrlEncodeUnicode(key);
                    var encodedValue = HttpUtility.UrlEncodeUnicode(value);
                    return $"{encodedKey}={encodedValue}";
                }));
    }
}