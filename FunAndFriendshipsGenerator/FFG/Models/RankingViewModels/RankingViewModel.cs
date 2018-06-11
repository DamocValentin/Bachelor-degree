using Data.Core.Domain;
using System.Collections.Generic;

namespace FFG.Models.RankingViewModels
{
    public class RankingViewModel
    {
        public List<User> Users { get; set; }
        public List<double> Points { get; set; }
        public List<string> WhiteSpaces { get; set; }
        public int NumberOfPages { get; set; }
        public int PageNumber { get; set; }
    }
}
