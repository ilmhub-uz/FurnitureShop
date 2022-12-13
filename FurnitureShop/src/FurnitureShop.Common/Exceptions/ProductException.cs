namespace FurnitureShop.Common.Exceptions;

public class BadRequestException : Exception
{
    public int ErrorCode { get; set; }
    public BadRequestException(string message) : base(message) { }
}