namespace Mostra.Application.Exceptions
{
    public class NotFoundException : DomainException
    {
        public override int StatusCode => 404;
        public NotFoundException(string message) : base(message) { }
    }
}