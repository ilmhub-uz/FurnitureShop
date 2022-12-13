namespace FurnitureShop.Common.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    { }
}

public class NotFoundException<T> : NotFoundException
{
    public NotFoundException() : base($"Given object {typeof(T).Name} is not found")
    { }
}
