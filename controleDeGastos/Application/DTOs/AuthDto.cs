namespace controleDeGastos.Application.DTOs;

public record RegisterDto(string Nome, string Email, string Senha);

public record LoginDto(string Email, string Senha);

public record RefreshTokenDto(string RefreshToken);

public record UserDto(int Id, string Nome, string Email);

public record AuthResponseDto(string Token, string RefreshToken, UserDto User);
