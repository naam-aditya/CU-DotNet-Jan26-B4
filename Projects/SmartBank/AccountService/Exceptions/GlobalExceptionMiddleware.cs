namespace AccountService.Exceptions;

public class GlobalExceptionMiddleware(string message) : Exception(message) { } 