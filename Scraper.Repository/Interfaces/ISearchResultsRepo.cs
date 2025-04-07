using Scraper.Repository.Models;

namespace Scraper.Repository.Interfaces;

public interface ISearchResultsRepo 
{
	Task InsertSearchResultsAsync(string url, string SearchString, List<int> position);
	Task<List<SearchResultsModel>> GetSearchResultsAsync();

	Task<List<int>> GetPositionAsync();

	Task<KeywordPositionModel> GetKeywordPositionsAsync();
}
