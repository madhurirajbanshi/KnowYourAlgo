namespace API.Dtos.Response;

public record AuthResponseDto(LoggedInUser user, string Token);