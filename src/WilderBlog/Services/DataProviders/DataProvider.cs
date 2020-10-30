using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace WilderBlog.Services.DataProviders
{
    public abstract class DataProvider<T>
    {
        protected string _path;

        public DataProvider(IHostEnvironment env, string path)
        {
            _path = Path.Combine(env.ContentRootPath, $@"Data{Path.DirectorySeparatorChar}{path}");
        }

        public virtual IEnumerable<T> Get()
        {
            var json = File.ReadAllText(_path);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

    }
}