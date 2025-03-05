using API.Dtos;
using API.Dtos.Request;
using API.Dtos.Response;
using API.Persistence.Entities;
using API.Persistence.UnitOfWork;
using API.Services.Interface;

namespace API.Services.Implementation;

public class AuthService : IAuthService
{

    private readonly IUnitOfWork _ufw;
    private readonly TokenService _tokenService;

    public AuthService( IUnitOfWork ufw, TokenService tokenService)
    {
        _ufw = ufw;
        _tokenService = tokenService;
    }

    public async Task<ResultWithDataDto<AuthResponseDto>> LoginUser(LoginDto dto)
    {
        var user = await _ufw.Users.GetByUserName(dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Invalid Username or Password");
        }
        return GenerateAuthResponse(user);
    }

    public async Task<ResultWithDataDto<AuthResponseDto>> RegisterAsync(RegisterDto dto)
    {
        var user = await _ufw.Users.GetByUserName(dto.Username);
        if(user is not null)
        {
            return ResultWithDataDto<AuthResponseDto>.Failure("Username already exists");
        }

        User newUser = new User
        {
            Username = dto.Username,
            Password = GenerateHashedPassword(dto.Password),
        };

        await _ufw.Users.InsertAsync(newUser);
        await _ufw.SaveChangesAsync();
        return GenerateAuthResponse(newUser);
    }
    private string GenerateHashedPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }
    private ResultWithDataDto<AuthResponseDto> GenerateAuthResponse(User user)
    {
        var loggedInUser = new LoggedInUser(user.Id, user.Username);
        var token = _tokenService.GenerateJwt(loggedInUser);

        var authResponse = new AuthResponseDto(loggedInUser, token);
        return ResultWithDataDto<AuthResponseDto>.Success(authResponse);
    }

}
