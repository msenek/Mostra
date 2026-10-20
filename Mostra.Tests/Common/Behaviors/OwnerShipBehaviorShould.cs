using MediatR;
using Moq;
using Mostra.Application.Common.Behaviors;
using Mostra.Application.Exceptions;
using Mostra.Application.Interfaces;
using Mostra.Domain.Entities;
using Xunit;

namespace Mostra.Tests.Common.Behaviors
{
    public class OwnerShipBehaviorShould
    {
        public class FakeBusinessRequest : IRequest<string>, IBusinessOwnedRequest
        {
            public int BusinessId { get; set; }
        }

        public class FakeCategoryRequest : IRequest<string>, ICategoryOwnedRequest
        {
            public int CategoryId { get; set; }
        }

        public class FakeProductRequest : IRequest<string>, IProductOwnedRequest
        {
            public int ProductId { get; set; }
        }

        public class FakePlainRequest : IRequest<string> { }

        private readonly Mock<ICurrentUserService> _currentUser = new();
        private readonly Mock<IBusinessRepository> _businessRepo = new();
        private readonly Mock<ICategoryRepository> _categoryRepo = new();
        private readonly Mock<IProductRepository> _productRepo = new();

        public OwnerShipBehaviorShould()
        {
            _currentUser.Setup(c => c.MerchantId).Returns(1);
        }

        private OwnerShipBehavior<TRequest, string> CreateBehavior<TRequest>() where TRequest : IRequest<string>
        {
            return new OwnerShipBehavior<TRequest, string>(
                _currentUser.Object,
                _businessRepo.Object,
                _categoryRepo.Object,
                _productRepo.Object);
        }

        [Fact]
        public async Task ContinueWithNext()
        {
            _businessRepo.Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Business { Id = 10, MerchantId = 1 });

            var behavior = CreateBehavior<FakeBusinessRequest>();

            var result = await behavior.Handle(
                new FakeBusinessRequest { BusinessId = 10 },
                (ct) => Task.FromResult("ok"),
                CancellationToken.None);

            Assert.Equal("ok", result);
        }

        [Fact]
        public async Task ThrowExceptionWhenUserNotOwner()
        {
            _businessRepo.Setup(r => r.GetByIdAsync(11, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Business { Id = 11, MerchantId = 9 });

            var behavior = CreateBehavior<FakeBusinessRequest>();

            await Assert.ThrowsAsync<ForbiddenException>(() =>
                behavior.Handle(
                    new FakeBusinessRequest { BusinessId = 11 },
                    (ct) => Task.FromResult("ok"),
                    CancellationToken.None));
        }

        [Fact]
        public async Task ThrowNotFoundWhenBusinessDoesNotExist()
        {
            _businessRepo.Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Business?)null);

            var behavior = CreateBehavior<FakeBusinessRequest>();

            await Assert.ThrowsAsync<NotFoundException>(() =>
                behavior.Handle(
                    new FakeBusinessRequest { BusinessId = 10 },
                    (ct) => Task.FromResult("ok"),
                    CancellationToken.None));
        }

        [Fact]
        public async Task ThrowForbiddenWhenCategoryBelongsToAnotherUsersBusiness()
        {
            _categoryRepo.Setup(r => r.GetByIdAsync(20, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category { Id = 20, BusinessId = 10 });
            _businessRepo.Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Business { Id = 10, MerchantId = 999 });

            var behavior = CreateBehavior<FakeCategoryRequest>();

            await Assert.ThrowsAsync<ForbiddenException>(() =>
                behavior.Handle(
                    new FakeCategoryRequest { CategoryId = 20 },
                    (ct) => Task.FromResult("ok"),
                    CancellationToken.None));
        }

        [Fact]
        public async Task ThrowForbiddenWhenProductBelongsToAnotherUsersBusiness()
        {
            _productRepo.Setup(r => r.GetByIdAsync(30, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Product { Id = 30, BusinessId = 10 });
            _businessRepo.Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Business { Id = 10, MerchantId = 999 });

            var behavior = CreateBehavior<FakeProductRequest>();

            await Assert.ThrowsAsync<ForbiddenException>(() =>
                behavior.Handle(
                    new FakeProductRequest { ProductId = 30 },
                    (ct) => Task.FromResult("ok"),
                    CancellationToken.None));
        }

        [Fact]
        public async Task SkipCheckWhenRequestHasNoOwnershipInterface()
        {
            var behavior = CreateBehavior<FakePlainRequest>();

            var result = await behavior.Handle(
                new FakePlainRequest(),
                (ct) => Task.FromResult("ok"),
                CancellationToken.None);

            Assert.Equal("ok", result);
            _businessRepo.Verify(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}