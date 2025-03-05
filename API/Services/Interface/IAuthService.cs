using API.Dtos;
using API.Dtos.Request;
using API.Dtos.Response;

namespace API.Services.Interface;

public interface IAuthService
{
    Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto);
    Task<ResultWithDataDto<AuthResponseDto>> RegisterAsync(RegisterDto dto);
}
