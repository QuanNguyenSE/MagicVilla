using MagicVilla.Web.Models;

namespace MagicVilla.Web.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responeModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
