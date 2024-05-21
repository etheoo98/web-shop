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

        CreateMap<Shipment, ShipmentDto>()
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress));

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.AddressDto, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.OrderDtos, opt => opt.MapFrom(src => src.CustomerOrders.Select(co => co.Order)));

        CreateMap<Order, OrderDto>()                             
            .ForMember(dest => dest.ShipmentId, opt => opt.MapFrom(src => src.FkShipmentId))
            .ForMember(dest => dest.ShipmentDetails, opt => opt.MapFrom(src => src.ShipmentDetails))
            .ForMember(dest => dest.OrderProductDtos, opt => opt.MapFrom(src => src.OrderProducts));

        CreateMap<CustomerOrder, CustomerOrderDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.FkCustomerId))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.FkOrderId));

        CreateMap<OrderProducts, OrderProductDto>()
           .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryDtos, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)))
            .ForMember(dest => dest.DiscountDto, opt => opt.MapFrom(src => src.Discount));       

        CreateMap<OrderProducts, OrderDto>()
           .ForMember(dest => dest.OrderProductDtos, opt => opt.MapFrom(src => src.Product));

        CreateMap<ProductCategory, CategoryDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Category.Description));

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

        //CreateMap<CreateOrderDto, Order>()
        //   .ForMember(dest => dest.FkCustomerId, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<CreateOrderItemDto, OrderProducts>()
            .ForMember(dest => dest.FkProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));    
        
        CreateMap<CreateCustomerOrderDto, CustomerOrder>()
            .ForMember(dest => dest.FkCustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.FkOrderId, opt => opt.MapFrom(src => src.OrderId));
    }
}