namespace Application.Exceptions;

public class RefreshTokenInvalid(string message) : BadRequestException(message);
