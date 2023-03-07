using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using the_key___technology_GmbH.BusinessLogic;
using the_key___technology_GmbH.Models;

namespace the_key___technology_GmbH.Controllers
{
    public class TheKeyAcademyController : Controller
    {
        private readonly IConfiguration configuration;

        public TheKeyAcademyController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.path = configuration["basePath"];
            return View();
        }

        public IActionResult React()
        {
            return View();
        }

        public IActionResult ReactWebsocket()
        {
            return View();
        }

        public async Task<string> GetBlogData()
        {
            string postsJson = await ContentReader.GetContent(configuration["basePath"] + "/wp-json/wp/v2/posts");
            string authorsJson = await ContentReader.GetContent(configuration["basePath"] + "/wp-json/wp/v2/users");
            IEnumerable<Blogbeitrag> posts = new List<Blogbeitrag>();
            IEnumerable<Authors> authors = new List<Authors>();
            try
            {
                posts = JsonConvert.DeserializeObject<IEnumerable<Blogbeitrag>>(postsJson);
                authors = JsonConvert.DeserializeObject<IEnumerable<Authors>>(authorsJson);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var result = from post in posts
                    join author in authors
                    on post.author equals author.id
                    select new
                    {
                        id = post.id,
                        title = post.title,
                        authorId = post.author,
                        authorName = author.name,
                        link = post.link,
                        date = post.date,
                        type = post.type,
                        categories = post.categories,
                        tags = post.tags,
                        theMap = WordCount.GetWordCount(post.content.rendered),
                    };

            return JsonConvert.SerializeObject(result);
        }
    }
}
