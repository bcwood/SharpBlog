using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpBlog.Parsers;

namespace SharpBlog.Models
{
    public class PostRepository
    {
        public List<Post> GetPosts()
        {
            if (Cache.Posts == null)
            {
                Cache.Posts = MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Posts"));
            }

            return Cache.Posts.Where(p => p.IsActive)
                              .OrderByDescending(p => p.Date)
                              .ToList();
        }

        public Post GetPost(string slug)
        {
            return GetPosts().SingleOrDefault(p => p.Slug == slug && p.IsActive);
        }

        public List<Post> GetPostsTagged(string tagSlug)
        {
            return GetPosts().Where(p => p.Tags.Any(t => t.Slug == tagSlug) && p.IsActive)
                             .OrderByDescending(p => p.Date)
                             .ToList();
        }

        public List<Post> GetPages()
        {
            if (Cache.Pages == null)
            {
                Cache.Pages = MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Pages"));
            }

            return Cache.Pages.Where(p => p.IsPublished)
                              .ToList();
        }

        public Post GetPage(string slug)
        {
            return GetPages().SingleOrDefault(p => p.Slug == slug);
        }

        public List<Post> GetPagesInMenu()
        {
            return GetPages().Where(p => p.IncludeInMenu)
                             .OrderBy(p => p.Order)
                             .ToList();
        }
    }
}