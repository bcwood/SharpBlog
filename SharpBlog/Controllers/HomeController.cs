using System.Web.Mvc;
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
    }
}