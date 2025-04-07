using Scraper.Repository.Models;
namespace Scraper.Core.Interfaces
{
    public interface IScraperService
    {
        Task<List<int>> GetSearchCountAsync(string keyWord, string SearchUrl, int MaxCount);
    }
}
