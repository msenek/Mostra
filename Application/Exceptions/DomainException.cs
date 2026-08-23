namespace Mostra.Application.Exceptions
{
    public abstract class DomainException : Exception
    {

        public virtual int StatusCode => 400;

        protected DomainException(string message) : base(message) { }
    }
}