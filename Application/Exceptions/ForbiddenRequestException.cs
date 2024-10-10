namespace Application.Exceptions;

public abstract class ForbiddenRequestException(string message) : Exception(message);
