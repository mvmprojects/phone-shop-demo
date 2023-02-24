namespace Phoneshop.Domain.Models
{
    public class PhoneResourceParameters
    {
        // The idea is collect all endpoint parameters into a class.
        // This way, the controller endpoint(s) do not need to be
        // modified when more parameters such as filters are added.

        public string SearchQuery { get; set; }

        // Paging Parameters

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        //public int PageSize
        //{
        //    get
        //    {
        //        return _pageSize;
        //    }
        //    set
        //    {
        //        _pageSize = (value > maxPageSize) ? maxPageSize : value;
        //    }
        //}

        // Filter Parameters

        // filter on Brand name
        public string Brand { get; set; }

        // filter on Price
        // todo include Greater Than and Less Than methods
        public string Price { get; set; }
    }
}
