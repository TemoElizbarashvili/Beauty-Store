using Beauty.Shared.Models;

namespace Beauty.Web.Models
{
    public class CartViewModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double CartTotal { get; set; }
    }
}
