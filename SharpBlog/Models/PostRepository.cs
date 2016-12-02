using System.Collections.Generic;
using System.Web;
using SharpBlog.Parsers;

namespace SharpBlog.Models
{
    public class PostRepository
    {
        public Post GetPost(string slug)
        {
            return MarkdownParser.ParseFile(HttpContext.Current.Server.MapPath($"~/Content/Posts/{slug}.md"));
        }

        public List<Post> GetPosts()
        {
            return MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Posts"));
        }

        public Post GetPage(string slug)
        {
            return MarkdownParser.ParseFile(HttpContext.Current.Server.MapPath($"~/Content/Pages/{slug}.md"));
        }

        public List<Post> GetPages()
        {
            return MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Pages"));
        }
    }
}