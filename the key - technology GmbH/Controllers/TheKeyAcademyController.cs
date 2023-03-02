using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using the_key___technology_GmbH.BusinessLogic;
using the_key___technology_GmbH.Models;

namespace the_key___technology_GmbH.Controllers
{
    public class TheKeyAcademyController : Controller
    {
        private HttpClient client = new();

        public TheKeyAcademyController()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult React()
        {
            return View();
        }

        public string GetRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101);
            return randomNumber.ToString();
        }

        public async Task<string> GetBlogData()
        {
            string postsJson = await client.GetStringAsync("https://www.thekey.academy/wp-json/wp/v2/posts");
            string authorsJson = await client.GetStringAsync("https://www.thekey.academy/wp-json/wp/v2/users");
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
