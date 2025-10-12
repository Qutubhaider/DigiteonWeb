using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolarCastleWeb.Models
{
    public class PaginationService
    {
        public static Pagination getPagination(Int64 fiTotalCount, int fiCurrentPage, int fiPageSize, Int64 fiStartIndex, Int64 fiEndIndex)
        {
            int liTotalPages = (int)Math.Ceiling(decimal.Divide(fiTotalCount, fiPageSize));
            Pagination loPagination = new Pagination()
            {
                inTotalRecords = fiTotalCount,
                inPageSize = fiPageSize,
                inCurrentPage = fiCurrentPage,
                inTotalPages = liTotalPages,
                stNoOfRecordsMessage = getNoOfRecordsMessage(fiStartIndex, fiEndIndex, fiTotalCount),
            };
            loPagination.lstPageNumbers = getPageNumbers(fiCurrentPage, liTotalPages, loPagination.inPaginationSize);
            return loPagination;
        }
        public static List<int> getPageNumbers(int fiCurrentPage, int fiTotalPages, int fiPaginationSize)
        {
            List<int> loPageNumbers = new List<int>();
            if (fiTotalPages > 0)
            {
                for (int liPreviousPage = fiCurrentPage - 1; liPreviousPage > 0 && liPreviousPage >= fiCurrentPage - fiPaginationSize; liPreviousPage--)
                    loPageNumbers.Add(liPreviousPage);

                loPageNumbers.Add(fiCurrentPage);

                for (int liNextPage = fiCurrentPage + 1; liNextPage <= fiTotalPages && liNextPage <= fiCurrentPage + fiPaginationSize; liNextPage++)
                    loPageNumbers.Add(liNextPage);

                loPageNumbers.Sort();
            }
            return loPageNumbers;
        }
        public static string getNoOfRecordsMessage(Int64 fiStartIndex, Int64 fiEndIndex, Int64 fiTotalRecords)
        {
            string lsMessage = string.Empty;
            if (fiTotalRecords <= 1)
                lsMessage = "Showing " + fiStartIndex + " - " + fiEndIndex + " of " + fiTotalRecords + " record";
            else
                lsMessage = "Showing " + fiStartIndex + " - " + fiEndIndex + " of " + fiTotalRecords + " records";
            return lsMessage;
        }
    }
}
