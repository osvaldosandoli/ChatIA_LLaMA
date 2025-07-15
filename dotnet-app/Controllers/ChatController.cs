using Microsoft.AspNetCore.Mvc;
using dotnet_app.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace dotnet_app.Controllers
{
    public class ChatController : Controller
    {
        private readonly HttpClient _httpClient;

        public ChatController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://web-api:8000");
            _httpClient.Timeout = TimeSpan.FromMinutes(10); 
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Chat());
        }

        [HttpPost]
        public async Task<IActionResult> Index(Chat request)
        {
            Console.WriteLine($"Mensagem enviada: {request.Message}");
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/chat")
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(new { message = request.Message }),
                        Encoding.UTF8, "application/json"
                    )
                };
                requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await _httpClient.SendAsync(requestMessage);
                var responseJson = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Resposta da API: {responseJson}");

                if (!response.IsSuccessStatusCode)
                {
                    request.Reply = $"Erro ao chamar API Python: {response.StatusCode} - {responseJson}";
                }
                else
                {
                    var parsed = JsonDocument.Parse(responseJson);
                    request.Reply = parsed.RootElement.TryGetProperty("reply", out var r) ? r.GetString() : "Erro na resposta";
                }
            }
            catch (Exception ex)
            {
                request.Reply = $"Erro ao processar requisição: {ex.Message}";
            }

            return View(request);
        }
    }
}