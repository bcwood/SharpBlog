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

        public ActionResult Post(string slug)
        {
            Post post = repo.GetPost(slug);
            return View(post);
        }

        public ActionResult Tag(string slug)
        {
            Post post = repo.GetPostsTagged(slug);
            return View(post);
        }

        public ActionResult Page(string slug)
        {
            Post page = repo.GetPage(slug);
            return View(page);
        }
    }
}