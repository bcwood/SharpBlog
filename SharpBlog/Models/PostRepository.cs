using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpBlog.Parsers;

namespace SharpBlog.Models
{
    public class PostRepository
    {
        private IEnumerable<Post> GetPosts()
        {
            if (Cache.Posts == null)
            {
                var posts = MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Posts"));
                SearchProvider.AddToIndex(posts);
                Cache.Posts = posts;
            }

            return Cache.Posts.Where(p => p.IsActive)
                              .OrderByDescending(p => p.Date);
        }

        public PagedPosts GetPostsPaged(int page)
        {
            return GetPosts().Paged(page, Config.Settings.PostsPerPage);
        }

        public Post GetPost(string slug)
        {
            return GetPosts().SingleOrDefault(p => p.Slug == slug);
        }

        public PagedPosts SearchPosts(string query, int page)
        {
            return SearchProvider.Search(query)
                                 .Paged(page, Config.Settings.PostsPerPage);
        }

        public PagedPosts GetPostsTagged(string tagSlug, int page)
        {
            return GetPosts().Where(p => p.Tags.Any(t => t.Slug == tagSlug))
                             .Paged(page, Config.Settings.PostsPerPage);
        }

        private IEnumerable<Post> GetPages()
        {
            if (Cache.Pages == null)
            {
                var pages = MarkdownParser.ParseFiles(HttpContext.Current.Server.MapPath("~/Content/Pages"));
                SearchProvider.AddToIndex(pages);
                Cache.Pages = pages;
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