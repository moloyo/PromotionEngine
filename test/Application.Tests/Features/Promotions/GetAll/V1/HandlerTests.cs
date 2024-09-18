using Mapster;
using PromotionEngine.Application.Features.Promotions.Shared;
using PromotionEngine.Application.Features.Promotions.Shared.Repositories;
using PromotionEngine.Entities;
using System.Reflection;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1
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
            var promitions = new List<Promotion>() { promotion };
            var request = new Request("DE", "DE");
            _repositoryMock.Setup(r => r.GetAll(request.CountryCode, default))
                .Returns(GetPromotionsStream(promitions));

            // Act
            var response = await _handler.HandleAsync(request, default);

            // Assert
            _repositoryMock.Verify(r => r.GetAll(request.CountryCode, default), Times.Once);
            Assert.Equal(response.Promotions.Count(), promitions.Count());
        }

        [Fact]
        public async Task HandleAsync_ReturnsExceptionOccurred_IfExceptionOccurres()
        {
            // Arrange
            var request = new Request("DE", "DE");
            var exception = new Exception();
            _repositoryMock.Setup(r => r.GetAll(request.CountryCode, default))
                .Throws(exception);

            // Act
            var response = await _handler.HandleAsync(request, default);

            // Assert
            _repositoryMock.Verify(r => r.GetAll(request.CountryCode, default), Times.Once);
            Assert.True(response.ExceptionOccurred);
            Assert.Equal(response.Exception, exception);
        }

        private async IAsyncEnumerable<Promotion> GetPromotionsStream(IEnumerable<Promotion> promotions)
        {
            foreach (var promotion in promotions)
                yield return promotion;

            await Task.CompletedTask;
        }
    }
}
