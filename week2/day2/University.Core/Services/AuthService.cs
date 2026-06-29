using University.Core.DTOs;
using University.Core.Forms;

namespace University.Core.Services
    {
    public interface IAuthService
        {
        Task<UserDto> Register ( RegisterForm form );
        Task<UserDto> Login ( LoginForm form );
        }
    }