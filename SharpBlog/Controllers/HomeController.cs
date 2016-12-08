using System.Web.Mvc;
using SharpBlog.Models;

namespace SharpBlog.Controllers
{
    public class HomeController : Controller
    {
        private PostRepository repo = new PostRepository();

        public ActionResult Index()
        {
            var posts = repo.GetPosts();
            return View(posts);
        }

        [Route("posts/{slug}", Name = "Post")]
        public ActionResult Post(string slug)
        {
            Post post = repo.GetPost(slug);
            return View(post);
        }

        [Route("tag/{slug}", Name = "Tag")]
        public ActionResult Tag(string slug)
        {
            ViewBag.Tag = slug;
            var posts = repo.GetPostsTagged(slug);
            return View(posts);
        }

        [Route("{slug}", Name = "Page")]
        public ActionResult Page(string slug)
        {
            Post page = repo.GetPage(slug);
            return View(page);
        }
    }
}