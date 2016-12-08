using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using SharpBlog.Parsers;

namespace SharpBlog.Models
{
    public class PostRepository
    {
        public Post GetPost(string slug)
        {
            var post = MarkdownParser.ParseFile(HttpContext.Current.Server.MapPath($"~/Content/Posts/{slug}.md"));

            return post.IsActive ? post : null;
        }

        public List<Post> GetPosts()
        {
            var posts = MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Posts"));

            return posts.Where(p => p.IsActive)
                        .OrderByDescending(p => p.Date)
                        .ToList();
        }

        public List<Post> GetPostsTagged(string tag)
        {
            var posts = GetPosts();

            return posts.Where(p => p.Tags.Contains(tag) && p.IsActive)
                        .OrderByDescending(p => p.Date)
                        .ToList();
        }

        public Post GetPage(string slug)
        {
            var page = MarkdownParser.ParseFile(HttpContext.Current.Server.MapPath($"~/Content/Pages/{slug}.md"));

            return page.IsActive ? page : null;
        }

        public List<Post> GetPagesInMenu()
        {
            var pages = MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Pages"));

            return pages.Where(p => p.IsActive && p.IncludeInMenu)
                        .OrderBy(p => p.Order)
                        .ToList();
        }
    }
}