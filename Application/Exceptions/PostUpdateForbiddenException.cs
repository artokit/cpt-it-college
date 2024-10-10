namespace Application.Exceptions;

public class PostUpdateForbiddenException(string message) : ForbiddenRequestException(message);
