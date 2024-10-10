namespace Application.Exceptions;

public class InvalidUpdatePostStatusException(string message) : BadRequestException(message);
