using System;
using System.Threading.Tasks;

namespace GetLocation.Interfaces.List
{
    public interface IListService
    {
        Task<ListReponse> GetList(string accessToken);
    }
}
