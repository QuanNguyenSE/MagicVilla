using MagicVilla.Web.Models.Dto;

namespace MagicVilla.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO regisRequestDTO);
    }
}
