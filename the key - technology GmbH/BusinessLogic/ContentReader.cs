using System.Net.Http.Headers;

namespace the_key___technology_GmbH.BusinessLogic
{
    public class ContentReader
    {
        private static HttpClient client = new();

        static ContentReader() {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        public static async Task<string> GetContent(string path) {
            try
            {
                return await client.GetStringAsync(path);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
