using AutoMapper;
using DotnetCoding.Core.Models;
using DotnetCoding.Core.Requests;
using DotnetCoding.Core.Responses;

namespace DotnetCoding.Services.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductDetails, ProductListResponse>()
                .ForMember(x => x.Status, e => e.MapFrom(m => GetStatus(m.Status)))
                .ReverseMap()
                .ForMember(x => x.Id, e => e.Ignore());

            CreateMap<CreateProductRequest, ProductDetails>()
                .ReverseMap();

            CreateMap<ProductQueue, ProductApprovalResponse>()
                .ForMember(x => x.QueueId, e => e.MapFrom(m => m.Id))
                .ForMember(x => x.ProductId, e => e.MapFrom(m => m.Product!.Id))
                .ForMember(x => x.ProductName, e => e.MapFrom(m => m.Product!.Name))
                .ForMember(x => x.RequestReason, e => e.MapFrom(m => m.RequestReason))
                .ForMember(x => x.RequestDate, e => e.MapFrom(m => m.RequestedDate))
                .ReverseMap()
                .ForMember(x => x.Id, e => e.Ignore());
        }

        private static string GetStatus(ProductStatus status) => status switch
        {
            ProductStatus.ApprovalRequired => "Approval Required",
            ProductStatus.Active => "Active",
            ProductStatus.Deleted => "Deleted",
            _ => throw new ArgumentOutOfRangeException("Unkown status."),
        };
    }
}
