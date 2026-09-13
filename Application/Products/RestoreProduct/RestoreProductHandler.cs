using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Application.Products.RestoreProduct;

public class RestoreProductHandler : IRequestHandler<RestoreProductRequestDto, RestoreProductResponseDto>
{
    private readonly IProductRepository _repository;
    private readonly IBusinessRepository _businessRepository;
    private readonly ICurrentUserService _currentUser;

    public RestoreProductHandler(IProductRepository repository, IBusinessRepository businessRepository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _businessRepository = businessRepository;
        _currentUser = currentUser;
    }

    public async Task<RestoreProductResponseDto> Handle(RestoreProductRequestDto request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken, includeDeleted: true);
        if (product == null)
            throw new NotFoundException($"Product with ID {request.Id} not found.");

        var business = await _businessRepository.GetByIdAsync(product.BusinessId, cancellationToken);
        if (business == null || business.MerchantId != _currentUser.MerchantId)
            throw new ForbiddenException("You don't have permission on this product.");

        product.Restore();
        await _repository.UpdateProductAsync(product, cancellationToken);

        return new RestoreProductResponseDto 
        { 
            Id = product.Id, 
            IsDeleted = product.IsDeleted
        };

    }
}