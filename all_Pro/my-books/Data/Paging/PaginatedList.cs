namespace my_books.Data.Paging
{
    public class PaginatedList<T>:List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public PaginatedList(List<T>items ,int count ,int pageIndx  ,int pageSie )
        {
            PageIndex = pageIndx;
            TotalPages=(int)Math.Ceiling(items.Count/(double)pageSie);
            this.AddRange(items);

        }
        public bool HasPreviousPage
        {
             get { return this.PageIndex > 1; }
        }
        public bool HasNextPage
        {
            get { return this.PageIndex < TotalPages; }
        }
        public    static PaginatedList<T> Create( IQueryable <T> source ,int pageindex ,int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageindex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items,count,pageindex,pageSize);
        }
    }
}
