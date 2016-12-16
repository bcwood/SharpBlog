using System.Web.Mvc;
using SharpBlog.Models;

namespace SharpBlog.Controllers
{
    public class HomeController : Controller
    {
        private PostRepository repo = new PostRepository();

        [Route("")]
        [Route("page/{pageNum}", Name = "PagedPosts")]
        public ActionResult Index(int pageNum = 1)
        {
            var posts = repo.GetPostsPaged(pageNum);
            return View(posts);
        }

        [Route("posts/{slug}", Name = "Post")]
        public ActionResult Post(string slug)
        {
            Post post = repo.GetPost(slug);

            if (post != null)
                return View(post);
            else
                return View("404");
        }

        [Route("tag/{slug}", Name = "Tag")]
        [Route("tag/{slug}/page/{pageNum}", Name = "PagedTag")]
        public ActionResult Tag(string slug, int pageNum = 1)
        {
            ViewBag.Tag = slug;
            var posts = repo.GetPostsTagged(slug, pageNum);
            return View(posts);
        }

        [Route("search", Name = "Search")]
        [Route("search/page/{pageNum}", Name = "PagedSearch")]
        public ActionResult Search(string q, int pageNum = 1)
        {
            ViewBag.Query = q;
            var posts = repo.SearchPosts(q, pageNum);
            return View(posts);
        }

        [Route("{slug}", Name = "Page")]
        public ActionResult Page(string slug)
        {
            Post page = repo.GetPage(slug);

            if (page != null)
                return View(page);
            else
                return View("404");
        }
    }
}