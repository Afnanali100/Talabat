namespace Talabat.Helpers
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }   
        public int PageIndex { get; set; }

        public int Count { get; set; }  

        public IReadOnlyList<T> Data { get; set; }

        public Pagination( int Count,int PageSize , int PageIndex ,IReadOnlyList<T> Data)
        {
            this.Count = Count;
            this.PageSize = PageSize ;
            this.PageIndex = PageIndex ;
            this.Data = Data ;
        }


    }
}
