namespace Application.Exceptions;

public class PostNotFoundException(string message) : NotFoundException(message);
