using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Models
{
    public class Pagination
    {
        public int inCurrentPage { get; set; }
        public int inPageSize { get; set; }
        public int inTotalPages { get; set; }
        public Int64 inTotalRecords { get; set; }
        public string stNoOfRecordsMessage { get; set; }
        public int inPaginationSize { get; set; } = 2;
        public List<int> lstPageNumbers { get; set; }
        public string stQueryString { get; set; }
    }
}
