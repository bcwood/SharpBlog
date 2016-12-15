using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpBlog.Parsers;

namespace SharpBlog.Models
{
    public class PostRepository
    {
        public IEnumerable<Post> GetPosts()
        {
            if (Cache.Posts == null)
            {
                var posts = MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Posts"));
                SearchProvider.AddPostsToIndex(posts);
                Cache.Posts = posts;
            }

            return Cache.Posts.Where(p => p.IsActive)
                              .OrderByDescending(p => p.Date);
        }

        public Post GetPost(string slug)
        {
            return GetPosts().SingleOrDefault(p => p.Slug == slug);
        }

        public List<Post> SearchPosts(string query)
        {
            return SearchProvider.Search(query);
        }

        public List<Post> GetPostsTagged(string tagSlug)
        {
            return GetPosts().Where(p => p.Tags.Any(t => t.Slug == tagSlug))
                             .OrderByDescending(p => p.Date)
                             .ToList();
        }

        public IEnumerable<Post> GetPages()
        {
            if (Cache.Pages == null)
            {
                Cache.Pages = MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Pages"));
            }

            return Cache.Pages.Where(p => p.IsPublished);
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