using System.Web.Mvc;
using SharpBlog.Models;
using SharpBlog.Parsers;

namespace SharpBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var posts = MarkdownParser.ParseFiles(Server.MapPath("~/Content/Posts"));

            return View(posts);
        }

        public ActionResult Post(string slug)
        {
            Post post = MarkdownParser.ParseFile(Server.MapPath($"~/Content/Posts/{slug}.md"));

            return View(post);
        }

        public ActionResult Page(string slug)
        {
            Post post = MarkdownParser.ParseFile(Server.MapPath($"~/Content/Pages/{slug}.md"));

            return View(post);
        }
    }
}