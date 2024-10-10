namespace Application.Exceptions;

public class FailedLoginException(string message) : ForbiddenRequestException(message);
