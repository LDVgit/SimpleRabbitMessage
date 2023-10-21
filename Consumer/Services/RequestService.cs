namespace Consumer.Services
{
    using Consumer.Entities;
    using Consumer.Infrastructure;
    using Consumer.Interfaces;

    /// <inheritdoc />
    public class RequestService : IRequestService
    {
        private readonly DataContext _dataContext;
        private readonly HttpClient _httpClient;

        public RequestService(DataContext dataContext, HttpClient httpClient)
        {
            _dataContext = dataContext;
            _httpClient = httpClient;
        }

        public async Task ExecuteAsync(string url)
        {
            if (!IsUrl(url, out var outUri))
            {
                Console.WriteLine("Не является ссылкой: {0}", url);
                return;
            }

            try
            {
                var site = await SendGetRequest(outUri);
                if (site is null)
                {
                    return;
                }

                await _dataContext.AddAsync(site);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка сохранения {0}", ex.Message);
            }
        }

        /// <summary>
        /// Отправка Get запроса.
        /// </summary>
        private async Task<Site> SendGetRequest(Uri uri)
        {
            if (uri is null)
            {
                return null;
            }

            try
            {
                var response = await _httpClient.GetAsync(uri);
                var body = await response.Content.ReadAsStringAsync();

                return new Site
                {
                    Date = DateTimeOffset.Now,
                    Response = body,
                    Url = uri.AbsoluteUri
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка отправки запроса по ссылке: {0}. Ошибка: {1}", uri.AbsoluteUri, ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Проверка является ли ссылкой
        /// </summary>
        private bool IsUrl(string url, out Uri outUri)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}