using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using the_key___technology_GmbH.Models;

namespace the_key___technology_GmbH.Controllers
{
    public class TheKeyAcademyController : Controller
    {
        static string DateTime_ISOFormat = "yyyy-MM-dd HH:mm";
        public TheKeyAcademyController()
        {
        }

        public IActionResult Index()
        {
            // fetch('https://www.thekey.academy/wp-json/wp/v2/posts').then(console.log)

            var beiträge = new List<Blogbeitrag>();
            beiträge.Add(new Blogbeitrag()
            {
                Id = 1,
                Author = "Ramzi",
                Title = "Ramzis Beitrag",
                Date = new DateTime(2023, 3, 1).ToString(DateTime_ISOFormat),
                Categories = new string[] { "My Category" },
                Link = "www.example.com"
            });

            ViewBag.BeiträgeJSON = JsonConvert.SerializeObject(beiträge);

            return View();
        }
    }
}
