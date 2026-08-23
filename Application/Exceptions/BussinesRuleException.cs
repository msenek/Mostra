namespace Mostra.Application.Exceptions
{
    public class BusinessRuleException : DomainException
    {
        public override int StatusCode => 400;
        public BusinessRuleException(string message) : base(message) { }
    }
}