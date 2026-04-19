namespace GulbahceSepeti.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity '{name}' with key '{key}' was not found.") { }
}

public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Bu işlem için yetkiniz bulunmamaktadır.") { }
    public ForbiddenException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("Bir veya daha fazla doğrulama hatası oluştu.")
    {
        Errors = errors;
    }
}

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}
