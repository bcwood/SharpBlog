using System.Collections.Generic;
using System.Web;

namespace SharpBlog.Models
{
    public static class Cache
    {
        private const string POSTS_KEY = "posts";
        private const string PAGES_KEY = "pages";
        private const string CONFIG_KEY = "configuration";

        public static List<Post> Posts
        {
            get { return Get(POSTS_KEY) as List<Post>; }
            set { Set(POSTS_KEY, value); }
        }

        public static List<Post> Pages
        {
            get { return Get(PAGES_KEY) as List<Post>; }
            set { Set(PAGES_KEY, value); }
        }

        public static Config Configuration
        {
            get { return Get(CONFIG_KEY) as Config; }
            set { Set(CONFIG_KEY, value); }
        }

        private static object Get(string key)
        {
            return HttpContext.Current.Application[key];
        }

        private static void Set(string key, object value)
        {
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application[key] = value;
            HttpContext.Current.Application.UnLock();
        }
    }
}