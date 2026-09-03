namespace Mostra.Application.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public override int StatusCode => 401;
        public UnauthorizedException(string message) : base(message) { }
    }
}