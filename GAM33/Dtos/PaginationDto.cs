using Gma33.Core.Entites;

namespace GAM33.Dtos
{
    public class PaginationDto<T>
    {
        public int Results { get; set; }
        public int PageSize { get; set; }
        public MetaDatePaginationDto MetaData { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public PaginationDto(int pageindex, int pagesize, IReadOnlyList<T> data, int TotalCount)
        {
            PageSize = pagesize;
            Data = data;
            Results = TotalCount;
            MetaData = new MetaDatePaginationDto
            {
                CurrentPage = pageindex,
                limit = pagesize,
                NextPage = pageindex < (int)Math.Ceiling((double)TotalCount / PageSize) ? pageindex + 1 : (int?)null,
                NumberOfPages = (int)Math.Ceiling((double)TotalCount / pagesize),
            };

        }

    }
}
