using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sample.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            HttpClientHandler h = new HttpClientHandler();
            h.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };


            using (var client = new HttpClient(h))
            {

                try
                {
                    var request = new HttpRequestMessage();
                    request.RequestUri = new Uri("https://sample.webapi:443/counter");

                    var response = await client.SendAsync(request);

                    var counter = await response.Content.ReadAsStringAsync();
                    ViewData["message"] = $"counter actual value: {counter}";
                }
                catch (Exception exp)
                {
                    _logger.LogCritical(exp.Message);
                    ViewData["message"] = exp.Message;
                }
            }
        }
    }
}