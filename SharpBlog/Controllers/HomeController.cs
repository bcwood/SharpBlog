using System.Web.Mvc;
using SharpBlog.Models;
using SharpBlog.Parsers;

namespace SharpBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var posts = MarkdownParser.ParseFiles(Server.MapPath("~/Posts"));
            
            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}