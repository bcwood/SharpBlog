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

        public string Theme { get; set; }

        public List<Link> Links { get; set; }

        private Config()
        {
            
        }

        public static Config Settings
        {
            get
            {
                if (HttpContext.Current.Application["CONFIGURATION"] == null)
                {
                    string configJson = File.ReadAllText(HttpContext.Current.Server.MapPath("~/config.json"));
                    HttpContext.Current.Application["CONFIGURATION"] = JsonConvert.DeserializeObject<Config>(configJson);
                }

                return HttpContext.Current.Application["CONFIGURATION"] as Config;
            }
        }
    }

    public class Link
    {
        public string Title { get; set; }
        public string Url { get; set; }

        [JsonProperty("fontawesome_icon")]
        public string FontAwesomeIcon { get; set; }
    }
}