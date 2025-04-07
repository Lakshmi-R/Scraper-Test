namespace Scraper.Repository.Models;
public record SearchResultsModel
{
	public Guid Id { get; set; }

	public string SearchUrl { get; set; }

	public string SearchKeyword {  get; set; }	

	public List<int> Position {  get; set; }	

	public DateTime Date { get; set; }
}
