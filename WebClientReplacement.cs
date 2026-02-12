using System.Net;
using System.Text;

namespace DS_Game_Maker
{
    public class WebClientReplacement
    {
        private readonly HttpClient httpClient;
        public const byte NoTimeOut = 0;

        public WebClientReplacement()
        {
            HttpClientHandler httpClientHandler = new()
            {
                AutomaticDecompression = DecompressionMethods.All
            };

            httpClient = new HttpClient(httpClientHandler);
        }

        public async Task<HTTP> GetAsync(string uri, int timeoutInMilliseconds = 15000)
        {
            if (timeoutInMilliseconds != NoTimeOut)
            {
                httpClient.Timeout = TimeSpan.FromMilliseconds(timeoutInMilliseconds);
            }

            try
            {
                using HttpResponseMessage response = await httpClient.GetAsync(uri);
                string responseBody = await response.Content.ReadAsStringAsync();
                bool success = response.StatusCode == HttpStatusCode.OK;

                return new HTTP
                {
                    Success = success,
                    Response = success ? responseBody : ""
                };
            }
            catch (Exception ex)
            {
                return new HTTP
                {
                    Success = false,
                    Response = ex.Message
                };
            }

        }

        public async Task<HTTP> PostAsync(string uri, string data, string contentType, int timeoutInMilliseconds = 15000)
        {
            if (timeoutInMilliseconds != NoTimeOut)
            {
                httpClient.Timeout = TimeSpan.FromMilliseconds(timeoutInMilliseconds);
            }

            try
            {
                using HttpContent content = new StringContent(data, Encoding.UTF8, contentType);

                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    Content = content,
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(uri)
                };

                using HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

                string responseBody = await response.Content.ReadAsStringAsync();
                bool success = response.StatusCode == HttpStatusCode.OK;

                return new HTTP
                {
                    Success = success,
                    Response = success ? responseBody : ""
                };
            }
            catch (Exception ex)
            {
                return new HTTP
                {
                    Success = false,
                    Response = ex.Message
                };
            }
        }

        public struct HTTP
        {
            public bool Success;
            public string Response;
        }
    }
}
