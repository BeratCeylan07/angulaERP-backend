﻿using AutoMapper;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.UpdateCustomer;
using ERPServer.Application.Features.Depots.CreateDepot;
using ERPServer.Application.Features.Depots.UpdateDepot;
using ERPServer.Application.Features.Orders.CreateOrder;
using ERPServer.Application.Features.Orders.UpdateOrder;
using ERPServer.Application.Features.Products.UpdateProduct;
using ERPServer.Application.Features.Products.CreateProduct;
using ERPServer.Application.Features.RecipeDetails.CreateRecipeDetail;
using ERPServer.Application.Features.RecipeDetails.UpdateRecipeDetail;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;

namespace ERPServer.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
        
        CreateMap<CreateDepotCommand, Depot>();
        CreateMap<UpdateDepotCommand, Depot>();

        
        // Burdaki işlemde şunu yaptık, Bizim product içerisinde bir type diye alanımız avr bu type alanı bize createproductcommandan gelen typevalue değerini bir Enum dönüştürerek set et ededik.
        CreateMap<CreateProductCommand, Product>().ForMember(member =>
            member.Type, options => 
            options.MapFrom(prop => 
            ProductTypeEnum.FromValue(prop.TypeValue)));
        
        // Burdaki işlemde şunu yaptık, Bizim product içerisinde bir type diye alanımız avr bu type alanı bize createproductcommandan gelen typevalue değerini bir Enum dönüştürerek set et ededik.
        CreateMap<UpdateProductCommand, Product>().ForMember(member =>
            member.Type, options => 
            options.MapFrom(prop => 
                ProductTypeEnum.FromValue(prop.TypeValue)));
        
        
        CreateMap<CreateRecipeDetailCommand, RecipeDetail>();
        CreateMap<UpdateRecipeDetailCommand, RecipeDetail>();


        CreateMap<CreateOrderCommand, Order>()
            .ForMember(member => member.Details,
                options => options.MapFrom(p => p.Details.Select(s => new OrderDetail()
                {
                    Price = s.Price,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToList()));
        
        CreateMap<UpdateOrderCommand, Order>()
            .ForMember(member => member.Details,
                options => options.MapFrom(p => p.Details.Select(s => new OrderDetail()
                {
                    Price = s.Price,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToList()));

    }
}