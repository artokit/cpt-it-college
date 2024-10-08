namespace Application.Exceptions;

public class UserIsDeleteException(string message) : BadRequestException(message);
