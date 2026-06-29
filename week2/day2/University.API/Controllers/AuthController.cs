using Microsoft.AspNetCore.Mvc;
using University.API.Filters;
using University.API.Helpers;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers
    {
    [Route ("api/[controller]")]
    [ApiController]
    [TypeFilter (typeof (ApiExceptionFilter))]
    public class AuthController : ControllerBase
        {
        private readonly IJwtTokenHelper _jwtTokenHelper;
        private readonly IAuthService _authService;

        public AuthController ( IJwtTokenHelper jwtTokenHelper, IAuthService authService )
            {
            _jwtTokenHelper = jwtTokenHelper;
            _authService = authService;
            }

        [HttpPost ("register")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register ( [FromBody] RegisterForm form )
            {
            var dto = await _authService.Register (form);
            return Ok (new { message = "User registered successfully", data = dto });
            }

        [HttpPost ("login")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login ( [FromBody] LoginForm form )
            {
            var user = await _authService.Login (form);
            var token = _jwtTokenHelper.GenerateToken (user);
            return Ok (new { message = "Login successful", token });
            }
        }
    }