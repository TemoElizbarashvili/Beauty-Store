using Beauty.Shared.Models;

namespace Beauty.Web.Models
{
    public class StoreViewModel
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
        public IEnumerable<Feedback> Feedbacks { get; set; } = Enumerable.Empty<Feedback>();
    }
}
