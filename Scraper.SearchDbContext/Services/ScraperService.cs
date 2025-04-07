using Scraper.Core.Interfaces;
using System.Text.RegularExpressions;
using Scraper.Common;

// file scoped namespace
namespace Scraper.Core.Services;
public class ScraperService : IScraperService
{

    private readonly IHttpClientFactory httpCientFactory;
    
    public ScraperService(IHttpClientFactory factory)
    {
        httpCientFactory = factory;
    }
    public async Task<List<int>> GetSearchCountAsync(string keyWord, string SearchUrl, int MaxCount)
    {
        var positions = new List<int>();
        using var httpClient = httpCientFactory.CreateClient();

        string urlTemplate = "search?q=#SearchText#&num=#MaxResultsCount#";
       
        string searchUrl = urlTemplate
            .Replace("#SearchText#", Uri.EscapeDataString(keyWord))
            .Replace("#MaxResultsCount#", MaxCount.ToString());

        string fullUrl = $"{ScraperConstants.BaseUrl}/{searchUrl}";

        var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
        request.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

        var response = await httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();


        var regex = new Regex(ScraperConstants.RegExpression, RegexOptions.IgnoreCase);
        var matches = regex.Matches(content);


        for (int i = 0; i < matches.Count; i++)
        {
            var extractedUrl = matches[i].Groups[1].Value;

            if (extractedUrl.Contains(SearchUrl, StringComparison.OrdinalIgnoreCase))
            {
                positions.Add(i + 1);
            }
        }

        return positions.Count > 0 ? positions : new List<int> { -1 };
    }
}
