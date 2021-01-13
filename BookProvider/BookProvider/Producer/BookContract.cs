using ContractLibrary;
using ContractLibrary.JsonModels;
using System.Collections.Generic;

namespace ExternalApi.Producer
{
    public class BookContract : IBookListContract
    {
       public  List<JsonBook> JBooks { get; set; }
    }
}
