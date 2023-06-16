using Beauty.Shared.Models;

namespace Beauty.Web.Models
{
    public class ShopVIewModel
    {
        public IEnumerable<Product> Products { set; get; }
        public bool IsAllShown { get; set; } = false;
    }
}
