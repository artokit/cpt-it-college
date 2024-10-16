namespace Application.Exceptions;

public class IdempotencyKeyConflictException(string message) : ConflictException(message);
