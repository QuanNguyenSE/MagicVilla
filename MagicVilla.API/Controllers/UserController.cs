using MagicVilla.API.Models;
using MagicVilla.API.Models.Dto;
using MagicVilla.API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected APIResponse _respone;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _respone = new APIResponse();
        }
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            LoginResponseDTO loginReponse = await _userRepository.Login(loginRequestDTO);
            if (loginReponse.User == null || String.IsNullOrEmpty(loginReponse.Token))
            {
                _respone.IsSuccess = false;
                _respone.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _respone.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_respone);
            }
            _respone.IsSuccess = true;
            _respone.StatusCode = System.Net.HttpStatusCode.OK;
            _respone.Result = loginReponse;
            return Ok(_respone);
        }
        [HttpPost("register")]
        public async Task<ActionResult<APIResponse>> Register([FromBody] RegisterationRequestDTO regisDTO)
        {
            bool isunique = _userRepository.IsUniqueUser(regisDTO.UserName);
            if (isunique)
            {
                LocalUser user = await _userRepository.Register(regisDTO);
                if (user == null)
                {
                    _respone.IsSuccess = false;
                    _respone.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    _respone.ErrorMessages.Add("Error while registering");
                    return BadRequest(_respone);
                }
                _respone.IsSuccess = true;
                _respone.StatusCode = System.Net.HttpStatusCode.OK;
                _respone.Result = user;
                return Ok(_respone);
            }
            _respone.IsSuccess = false;
            _respone.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _respone.ErrorMessages.Add("UserName is exist");
            return BadRequest(_respone);
        }
    }
}