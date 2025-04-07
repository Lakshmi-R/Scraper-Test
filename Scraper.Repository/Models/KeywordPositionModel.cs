using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Repository.Models
{
    public class KeywordPositionModel
    {
        public string SearchKeyword { get; set; }
        public List<int> Position { get; set; }
    }
}
