namespace Mostra.Application.Exceptions
{
    public class ConflictException : DomainException
    {
        public override int StatusCode => 409;
        public ConflictException(string message) : base(message) { }
    }
}