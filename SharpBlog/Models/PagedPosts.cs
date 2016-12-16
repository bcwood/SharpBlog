using System.Collections.Generic;
using System.Linq;

namespace SharpBlog.Models
{
    public class PagedPosts
    {
        public List<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
        public int PageNumber { get; set; }
    }

    public static class IEnumerableExtensions
    {
        public static PagedPosts Paged(this IEnumerable<Post> source, int page, int itemsPerPage)
        {
            var postList = source.ToList();
            var paged = new PagedPosts();
            paged.Posts = postList.Skip((page - 1) * itemsPerPage)
                                  .Take(itemsPerPage)
                                  .ToList();

            paged.TotalPosts = postList.Count;
            paged.PageNumber = page;

            return paged;
        }
    }
}