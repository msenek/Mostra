using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Businesses.DeleteBusiness
{
    public class DeleteBusinessHandler : IRequestHandler<DeleteBusinessRequestDto, Unit>
    {
        private readonly IBusinessRepository _repository;
        public DeleteBusinessHandler(IBusinessRepository repository) => _repository = repository;

        public async Task<Unit> Handle(DeleteBusinessRequestDto request, CancellationToken cancellationToken)
        {
            var business = await _repository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Business not found.");

            business.MarkAsDeleted();
            await _repository.DeleteAsync(business, cancellationToken);

            return Unit.Value;
        }
    }
}