using MediatR;
using MORR.Application.Common.Extentions;
using MORR.Application.DTOs.ProductDTOs;
using MORR.Domain.Repositories.Query;

namespace MORR.Application.Pipelines.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int id) : IRequest<ProductDto>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public GetProductByIdQueryHandler(IProductQueryRepository productQueryRepository)
        {
            this._productQueryRepository = productQueryRepository;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productQueryRepository.GetById(request.id, cancellationToken);

                if (product is null)
                {
                    return new ProductDto();
                }

                var productData = product.ToDto();

                return productData;
            }
            catch (Exception)
            {

                return new ProductDto();
            }
        }
    }
}
