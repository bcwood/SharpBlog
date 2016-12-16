using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace SharpBlog.Models
{
    public class Config
    {
        [JsonProperty("site_title")]
        public string SiteTitle { get; set; }

        [JsonProperty("site_description")]
        public string SiteDescription { get; set; }

        [JsonProperty("site_url")]
        public string SiteUrl { get; set; }

        [JsonProperty("site_logo")]
        public string SiteLogo { get; set; }

        [JsonProperty("posts_per_page")]
        public int PostsPerPage { get; set; } = 10;

        public string Theme { get; set; } = "staypuft";

        public List<Link> Links { get; set; }

        public class Link
        {
            public string Title { get; set; }
            public string Url { get; set; }

            [JsonProperty("fontawesome_icon")]
            public string FontAwesomeIcon { get; set; }
        }

        private Config() { }

        public static Config Settings
        {
            get
            {
                if (Cache.Configuration == null)
                {
                    string configJson = File.ReadAllText(HttpContext.Current.Server.MapPath("~/config.json"));
                    Cache.Configuration = JsonConvert.DeserializeObject<Config>(configJson);
                }

                return Cache.Configuration;
            }
        }
    }
}