using Mapster;
using PromotionEngine.Application.Features.Promotions.Shared;
using PromotionEngine.Application.Features.Promotions.Shared.Repositories;
using PromotionEngine.Entities;
using System.Reflection;

namespace PromotionEngine.Application.Features.Promotions.Get.V1
{
    public class HandlerTests
    {
        private readonly Mock<IRepository> _repositoryMock;
        private readonly Handler _handler;

        public HandlerTests()
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(Mapping))!);
            _repositoryMock = new Mock<IRepository>();
            _handler = new Handler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_ReturnsPromotion_IfFound()
        {
            // Arrange
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                CountryCode = "DE",
                CreatedDate = DateTime.Now,
                Images = new List<string>() { "Image3", "Image4" },
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Enabled,
                EndValidityDate = DateTime.Now.AddDays(2),
                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        "DE",
                        new DisplayContent()
                        {
                            Description = "Description",
                            DiscountDescription = "Discount Description",
                            DiscountTitle = "Discount Title",
                            Title = "Title"
                        }
                    }
                },
                Discounts = new List<Discount>()
                {
                    new StoreDiscount()
                    {
                        FinalPrice = 1,
                        HasPrice = true,
                        LowestPriceLast30Days = 1,
                        OriginalPrice = 1,
                        PriceType = "Type1",
                        UnitsToBuy = 1,
                        UnitsToPay = 1
                    }
                }
            };
            var request = new Request("DE", "DE", promotion.Id);
            _repositoryMock.Setup(r => r.GetAsync(request.CountryCode, request.PromotionId, default))
                .ReturnsAsync(promotion);

            // Act
            var response = await _handler.HandleAsync(request, default);

            // Assert
            _repositoryMock.Verify(r => r.GetAsync(request.CountryCode, request.PromotionId, default), Times.Once);
            Assert.NotNull(response.Promotion);
            Assert.Equal(response.Promotion.PromotionId, promotion.Id);
        }

        [Fact]
        public async Task HandleAsync_ReturnsNotFound_IfNotFound()
        {
            // Arrange
            var guid = new Guid();
            var request = new Request("DE", "DE", guid);
            _repositoryMock.Setup(r => r.GetAsync(request.CountryCode, request.PromotionId, default))
                .ReturnsAsync(default(Promotion));

            // Act
            var response = await _handler.HandleAsync(request, default);

            // Assert
            _repositoryMock.Verify(r => r.GetAsync(request.CountryCode, request.PromotionId, default), Times.Once);
            Assert.True(response.NotFound);
        }

        [Fact]
        public async Task HandleAsync_ReturnsExceptionOccurred_IfExceptionOccurres()
        {
            // Arrange
            var guid = new Guid();
            var request = new Request("DE", "DE", guid);
            var exception = new Exception();
            _repositoryMock.Setup(r => r.GetAsync(request.CountryCode, request.PromotionId, default))
                .Throws(exception);

            // Act
            var response = await _handler.HandleAsync(request, default);

            // Assert
            _repositoryMock.Verify(r => r.GetAsync(request.CountryCode, request.PromotionId, default), Times.Once);
            Assert.True(response.ExceptionOccurred);
            Assert.Equal(response.Exception, exception);
        }
    }
}
