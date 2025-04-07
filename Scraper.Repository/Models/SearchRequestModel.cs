namespace Scraper.Repository.Models;

public class SearchRequestModel
{
	public required string SearchKeyword { get; set; }

	public required string SearchUrl { get; set;}	

	public required int MaxCount { get; set;}
}
