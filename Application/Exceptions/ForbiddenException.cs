namespace Mostra.Application.Exceptions
{
    public class ForbiddenException : DomainException
    {
        public override int StatusCode => 403;
        public ForbiddenException(string message) : base(message) { }
    }
}