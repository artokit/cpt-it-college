namespace Application.Exceptions;

public class UnknownRoleException(string message) : BadRequestException(message);
