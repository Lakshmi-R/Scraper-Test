using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scraper.Core.Interfaces;
using Scraper.Repository.Interfaces;
using Scraper.Repository.Models;

namespace Scraper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IScraperService _scraperService;
        private readonly ISearchResultsRepo _searchResultsRepo;
        private readonly ILogger<SearchController> _logger;
        public SearchController(IScraperService service, ILogger<SearchController> logger,ISearchResultsRepo repo)
        {
            _scraperService = service;
            _logger = logger;
            _searchResultsRepo = repo;
        }

        [HttpPost("InsertSearchResults")]
        public async Task<IActionResult> GetSearchResults([FromBody] SearchRequestModel searchRequestModel)
        {
            try
            {
                var SearchResultsList = await _scraperService.GetSearchCountAsync(searchRequestModel.SearchKeyword, searchRequestModel.SearchUrl, searchRequestModel.MaxCount);
                if(SearchResultsList.Any()) {
                   await _searchResultsRepo.InsertSearchResultsAsync(searchRequestModel.SearchUrl, searchRequestModel.SearchKeyword, SearchResultsList);                 
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the data.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        [HttpGet("SearchResults")]
        public async Task<IActionResult> GetSearchResultDataFromRepo()
        {
            try
            {
                var scraperResult = await _searchResultsRepo.GetSearchResultsAsync();
                return Ok(scraperResult.Any() ? scraperResult.ToList() : new List<SearchResultsModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the data.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("Position")]
        public async Task<IActionResult> GetPostion()
        {
            try
            {
                var res = await _searchResultsRepo.GetKeywordPositionsAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the index position.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
