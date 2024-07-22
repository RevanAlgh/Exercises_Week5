using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace TheaterApiConsoleTest
{
    class Program
    {
        private static readonly HttpClientHandler handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
        };
        private static readonly HttpClient client = new HttpClient(handler);

        static async Task Main(string[] args)
        {
            // Set the base address of the API
            client.BaseAddress = new Uri("http://localhost:5000/api/"); // Update to match your API base address
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Test GET Movie by ID
                await GetMovie(3);

                // Test POST Create Movie
                await CreateMovie();

                // Test PUT Update Movie
                await UpdateMovie(3);

                // Test DELETE Movie by ID
                await DeleteMovie(3);

                // Test GET Author by ID
                await GetAuthor(3);

                // Test POST Create Author
                await CreateAuthor();

                // Test PUT Update Author
                await UpdateAuthor(3);

                // Test DELETE Author by ID
                await DeleteAuthor(3);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }

            // Pause to view results
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        private static async Task GetMovie(int id)
        {
            try
            {
                string url = $"movies/{id}";
                Console.WriteLine($"GET {url}");
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var movie = await response.Content.ReadFromJsonAsync<MovieDto>();
                Console.WriteLine(JsonSerializer.Serialize(movie));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error getting movie: {e.Message}");
            }
            Console.WriteLine("\n");
        }

        private static async Task CreateMovie()
        {
            var newMovie = new CreateMovieDto
            {
                MovieTitle = "Born a singer",
                ImdbRating = 8.0,
                YearReleased = 2024,
                Budget = 100,
                BoxOffice = 500,
                Language = "English",
                AuthorID = 1
            };

            try
            {
                string url = "movies";
                Console.WriteLine($"POST {url}");
                HttpResponseMessage response = await client.PostAsJsonAsync(url, newMovie);
                response.EnsureSuccessStatusCode();
                var movie = await response.Content.ReadFromJsonAsync<MovieDto>();
                Console.WriteLine(JsonSerializer.Serialize(movie));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error creating movie: {e.Message}");
            }
            Console.WriteLine("\n");
        }

        private static async Task UpdateMovie(int id)
        {
            var updatedMovie = new UpdateMovieDto
            {
                MovieTitle = "Updated Movie",
                ImdbRating = 9.0,
                YearReleased = 2023,
                Budget = 150,
                BoxOffice = 600,
                Language = "Spanish",
                AuthorID = 1
            };

            try
            {
                string url = $"movies/{id}";
                Console.WriteLine($"PUT {url}");
                HttpResponseMessage response = await client.PutAsJsonAsync(url, updatedMovie);
                response.EnsureSuccessStatusCode();
                var movie = await response.Content.ReadFromJsonAsync<MovieDto>();
                Console.WriteLine(JsonSerializer.Serialize(movie));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error updating movie: {e.Message}");
            }
            Console.WriteLine("\n");
        }

        private static async Task DeleteMovie(int id)
        {
            try
            {
                string url = $"movies/{id}";
                Console.WriteLine($"DELETE {url}");
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"Deleted Movie with ID {id}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error deleting movie: {e.Message}");
            }
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
        }

        private static async Task GetAuthor(int id)
        {
            try
            {
                string url = $"authors/{id}";
                Console.WriteLine($"GET {url}");
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var author = await response.Content.ReadFromJsonAsync<AuthorDto>();
                Console.WriteLine(JsonSerializer.Serialize(author));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error getting author: {e.Message}");
            }
            Console.WriteLine("\n");
        }

        private static async Task CreateAuthor()
        {
            var newAuthor = new CreateAuthorDto
            {
                AuthorName = "New Author"
            };

            try
            {
                string url = "authors";
                Console.WriteLine($"POST {url}");
                HttpResponseMessage response = await client.PostAsJsonAsync(url, newAuthor);
                response.EnsureSuccessStatusCode();
                var author = await response.Content.ReadFromJsonAsync<AuthorDto>();
                Console.WriteLine(JsonSerializer.Serialize(author));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error creating author: {e.Message}");
            }
            Console.WriteLine("\n");
        }

        private static async Task UpdateAuthor(int id)
        {
            var updatedAuthor = new UpdateAuthorDto
            {
                AuthorName = "Updated Author"
            };

            try
            {
                string url = $"authors/{id}";
                Console.WriteLine($"PUT {url}");
                HttpResponseMessage response = await client.PutAsJsonAsync(url, updatedAuthor);
                response.EnsureSuccessStatusCode();
                var author = await response.Content.ReadFromJsonAsync<AuthorDto>();
                Console.WriteLine(JsonSerializer.Serialize(author));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error updating author: {e.Message}");
            }
            Console.WriteLine("\n");
        }

        private static async Task DeleteAuthor(int id)
        {
            try
            {
                string url = $"authors/{id}";
                Console.WriteLine($"DELETE {url}");
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"Deleted Author with ID {id}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error deleting author: {e.Message}");
            }
            Console.WriteLine("\n");
        }
    }

    public class MovieDto
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public float ImdbRating { get; set; }
        public int YearReleased { get; set; }
        public decimal Budget { get; set; }
        public decimal BoxOffice { get; set; }
        public string Language { get; set; }
        public int AuthorID { get; set; }
        public List<string> MovieAuthors { get; set; }
    }

    public class CreateMovieDto
    {
        public string MovieTitle { get; set; }
        public double ImdbRating { get; set; }
        public int YearReleased { get; set; }
        public decimal Budget { get; set; }
        public decimal BoxOffice { get; set; }
        public string Language { get; set; }
        public int AuthorID { get; set; }
    }

    public class UpdateMovieDto
    {
        public string MovieTitle { get; set; }
        public double ImdbRating { get; set; }
        public int YearReleased { get; set; }
        public decimal Budget { get; set; }
        public decimal BoxOffice { get; set; }
        public string Language { get; set; }
        public int AuthorID { get; set; }
    }

    public class AuthorDto
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public List<string> MovieAuthors { get; set; }
    }

    public class CreateAuthorDto
    {
        public string AuthorName { get; set; }
    }

    public class UpdateAuthorDto
    {
        public string AuthorName { get; set; }
    }
}
