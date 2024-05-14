using AutoMapper;
using WebShop.Models.DbModels;
using WebShop.Models.RequestDTOs;
using WebShop.Models.ResponseDTOs;

namespace WebShop.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // DbModels to Response DTOs
        CreateMap<Address, AddressDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Customer, CustomerDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryDtos, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)));
        
        CreateMap<Shipment, ShipmentDto>();
        
        CreateMap<CustomerOrder, CustomerDto>()
            .ForMember(dest => dest.OrderDtos, opt => opt.MapFrom(src => src.Order));
        
        CreateMap<OrderProducts, OrderDto>()
            .ForMember(dest => dest.ProductDtos, opt => opt.MapFrom(src => src.Product));
        
        CreateMap<ProductCategory, ProductDto>()
            .ForMember(dest => dest.CategoryDtos, opt => opt.MapFrom(src => src.Category));
        
        // Requests
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<CreateCategoryDto, Category>();
    }
}