using MediatR;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;

namespace Mostra.Application.Common.Behaviors
{
    public class OwnerShipBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>

    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBusinessRepository _businessRepository;

        public OwnerShipBehavior(ICurrentUserService currentUserService, IBusinessRepository businessRepository, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _currentUserService = currentUserService;
            _businessRepository = businessRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var merchantId = _currentUserService.MerchantId;

            if (request is IBusinessOwnedRequest ownedRequest)
            {
                var business = await _businessRepository.GetByIdAsync(ownedRequest.BusinessId, cancellationToken);
                if (business == null)
                {
                    throw new NotFoundException("Bussines not found");
                }

                if (business.MerchantId != merchantId)
                {
                    throw new ForbiddenException("You don't have permission on this business.");
                }
            }
                if (request is ICategoryOwnedRequest categoryOwnedRequest)
                {

                    var category = await _categoryRepository.GetByIdAsync(categoryOwnedRequest.CategoryId, cancellationToken);
                    if (category == null)
                    {
                        throw new NotFoundException("Category id not found");
                    }

                    var categoryBusiness = await _businessRepository.GetByIdAsync(category.BusinessId, cancellationToken);
                    if (categoryBusiness == null)
                    {
                        throw new NotFoundException("Business id not found");
                    }

                    if (categoryBusiness.MerchantId != merchantId)
                    {
                        throw new ForbiddenException("You don't have permission");
                    }
                }

                if (request is IProductOwnedRequest productOwnedRequest)
                {
                    var product = await _productRepository.GetByIdAsync(productOwnedRequest.ProductId, cancellationToken);
                    if (product == null)
                    {
                        throw new NotFoundException("Product not found");
                    }
                    var productBusiness = await _businessRepository.GetByIdAsync(product.BusinessId, cancellationToken);
                    if (productBusiness == null)
                    {
                        throw new NotFoundException("Business not found");
                    }
                    if (productBusiness.MerchantId != merchantId)
                    {
                        throw new ForbiddenException("You don't have permission");
                    }
                }
            
            return await next();

        }
    }
}
