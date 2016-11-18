using System.Web.Mvc;
using SharpBlog.Models;
using SharpBlog.Parsers;

namespace SharpBlog.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Detail(string slug)
        {
            Post post = MarkdownParser.ParseFile(Server.MapPath($"~/Posts/{slug}.md"));
            return View(post);
        }
    }
}