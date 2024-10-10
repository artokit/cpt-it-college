namespace Application.Exceptions;

public class EmailIsExistingException(string message) : BadRequestException(message);
