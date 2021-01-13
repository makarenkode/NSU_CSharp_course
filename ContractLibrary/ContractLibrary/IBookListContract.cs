using ContractLibrary.JsonModels;
using System.Collections.Generic;

namespace ContractLibrary
{
    public interface IBookListContract
    {
        List<JsonBook> JBooks { get; set; }
    }
}

