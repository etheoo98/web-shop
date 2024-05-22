using AutoMapper;
using WebShop.Models.DbModels;
using WebShop.Models.RequestDTOs;
using WebShop.Models.ResponseDTOs;

namespace WebShop.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Response - Map database model to DTO
        CreateMap<Address, AddressDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Discount, DiscountDto>();
        CreateMap<ShippingAddress, ShippingAddressDto>();

        CreateMap<Customer, CustomerDto>()
          .ForMember(dest => dest.AddressDto, opt => opt.MapFrom(src => src.Address));

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CustomerId,
                opt => opt.MapFrom(src => src.CustomerOrders.Select(co => co.Customer.Id).FirstOrDefault()))
            .ForMember(dest => dest.ProductDtos, opt => opt.MapFrom(src => src.OrderProducts.Select(op => op.Product)));

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryDtos, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
            .ForMember(dest => dest.DiscountDto, opt => opt.MapFrom(src => src.Discount));

        CreateMap<Shipment, ShipmentDto>()
           .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress));

        CreateMap<CustomerOrder, CustomerDto>()
            .ForMember(dest => dest.OrderDtos, opt => opt.MapFrom(src => src.Order));

        CreateMap<OrderProducts, OrderDto>()
            .ForMember(dest => dest.ProductDtos, opt => opt.MapFrom(src => src.Product));

        CreateMap<ProductCategory, ProductDto>()
            .ForMember(dest => dest.CategoryDtos, opt => opt.MapFrom(src => src.Category));

        // Requests - Map DTO to database model
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCustomerDto, Customer>();
        CreateMap<CreateDiscountDto, Discount>();
        CreateMap<EditProductDto, Product>();
        CreateMap<CreateShipmentDetailsDto, Shipment>();
        CreateMap<AddressDto, Address>();
        CreateMap<CreateShippingAddressDto, ShippingAddress>();
        CreateMap<CreateOrderDto, Order>();

        CreateMap<CreateOrderItemDto, OrderProducts>()
            .ForMember(dest => dest.FkProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<CreateCustomerOrderDto, CustomerOrder>()
            .ForMember(dest => dest.FkCustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.FkOrderId, opt => opt.MapFrom(src => src.OrderId));
    }
}