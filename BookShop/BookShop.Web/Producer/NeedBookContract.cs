using ContractLibrary;


namespace BookShop.Web.Producer
{
    public class NeedBookContract : IBookContract
    {
        public int BookQuantity { get; set; }
    }
}
