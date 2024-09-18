using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromotionEngine.Application.Features.Promotions.Shared;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.Features.Promotions.Get.V1
{
    public class PromotionsControllerTests
    {
        private readonly Mock<IHandler<Request, Response>> _handlerMock;
        private readonly Mock<ILogger<PromotionsController>> _loggerMock;
        private readonly PromotionsController _controller;

        public PromotionsControllerTests()
        {
            _handlerMock = new Mock<IHandler<Request, Response>>();
            _loggerMock = new Mock<ILogger<PromotionsController>>();
            var context = new DefaultHttpContext();
            _controller = new PromotionsController(_handlerMock.Object, _loggerMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = context
                }
            };
        }

        [Fact]
        public async Task Get_ReturnsOkPromotion_IfFound()
        {
            // Arrange
            var promotionModel = new PromotionModel()
            {
                PromotionId = Guid.NewGuid(),
                Texts = new PromotionTextsModel()
                {
                    Description = "Description",
                    DiscountDescription = "Discount Description",
                    DiscountTitle = "Discount Title",
                    Title = "Title"
                },
                Images = new List<string>() { "Image3", "Image4" },
                EndValidityDate = DateTime.Now.AddDays(2),                
                Discounts = new List<PromotionDiscountModel>()
                {
                    new PromotionDiscountModel()
                    {
                        Type = "Store",
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
            var countryCode = "DE";
            var languageCode = "DE";
            var request = new Request(countryCode, languageCode, promotionModel.PromotionId);
            var response = new Response().SetPromotion(promotionModel);
            _handlerMock.Setup(r => r.HandleAsync(request, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Get(countryCode, languageCode, promotionModel.PromotionId, default);

            // Assert
            _handlerMock.Verify(r => r.HandleAsync(request, default), Times.Once);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(result.As<OkObjectResult>().Value, promotionModel);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_IfNotFound()
        {
            // Arrange
            var countryCode = "DE";
            var languageCode = "DE";
            var request = new Request(countryCode, languageCode, new Guid());
            var response = new Response().SetNotFound();
            _handlerMock.Setup(r => r.HandleAsync(request, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Get(countryCode, languageCode, request.PromotionId, default);

            // Assert
            _handlerMock.Verify(r => r.HandleAsync(request, default), Times.Once);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_ReturnsInternalServerError_IfNotFound()
        {
            // Arrange
            var countryCode = "DE";
            var languageCode = "DE";
            var request = new Request(countryCode, languageCode, new Guid());
            var response = new Response().SetException(new Exception());
            _handlerMock.Setup(r => r.HandleAsync(request, default))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Get(countryCode, languageCode, request.PromotionId, default);

            // Assert
            _handlerMock.Verify(r => r.HandleAsync(request, default), Times.Once);
            Assert.Equal(result.As<ObjectResult>().StatusCode, 500);
        }
    }
}
