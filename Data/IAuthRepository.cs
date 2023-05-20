using Auth.Dtos.User;
using Menu.Dtos.User;

namespace Auth.Data;

public interface IAuthRepository{
    Task<(UserRegisterDto user, string? error)> Register(UserRegisterDto user);

    Task<LoginResponseDto> Login(string username, string password);

}