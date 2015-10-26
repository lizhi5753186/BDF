using System;
using AutoMapper;
using Bdf.Sample.Domain.Model;
using Sample.Application.Dtos.Order;
using Sample.Application.Dtos.Product;
using Sample.Application.Dtos.User;

namespace Sample.Application
{
    public static class DtoMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<AddressDto, Address>();
            Mapper.CreateMap<UserDto, User>()
                .ForMember(uMermber => uMermber.ContactAddress, mceUto => mceUto.ResolveUsing<AddressResolver>().FromMember(fm => fm.ContactAddress))
                .ForMember(uMember => uMember.DeliveryAddress, mceUto =>
                        mceUto.ResolveUsing<AddressResolver>().FromMember(fm => fm.DeliveryAddress));

            Mapper.CreateMap<User, UserDto>()
               .ForMember(udoMember => udoMember.ContactAddress, mceU =>
                   mceU.ResolveUsing<InversedAddressResolver>().FromMember(fm => fm.ContactAddress))
                   .ForMember(udoMember => udoMember.DeliveryAddress, mceU =>
                       mceU.ResolveUsing<InversedAddressResolver>().FromMember(fm => fm.DeliveryAddress));

            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<ProductDto, Product>();
            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<CategoryDto, Category>();
            Mapper.CreateMap<ShoppingCart, ShoppingCartDto>();
            Mapper.CreateMap<ShoppingCartDto, ShoppingCart>();
            Mapper.CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
            Mapper.CreateMap<ShoppingCartItemDto, ShoppingCartItem>();
            Mapper.CreateMap<OrderItem, OrderItemDto>();
            Mapper.CreateMap<OrderItemDto, OrderItem>();
            Mapper.CreateMap<Order, OrderDto>()
                .ForMember(odtoMember => odtoMember.UserContact,
                    mceO => mceO.ResolveUsing(o => o.User.Contact))
                .ForMember(odtoMember => odtoMember.UserPhone,
                    mceO => mceO.ResolveUsing(o => o.User.PhoneNumber))
                .ForMember(odtoMember => odtoMember.UserEmail,
                    mceO => mceO.ResolveUsing(o => o.User.Email))
                .ForMember(odtoMember => odtoMember.UserId,
                    mceO => mceO.ResolveUsing(o => o.User.Id))
                .ForMember(odtoMember => odtoMember.UserName,
                    mceO => mceO.ResolveUsing(o => o.User.UserName))
                .ForMember(odtoMember => odtoMember.UserAddressCountry,
                    mceO => mceO.ResolveUsing(o => o.User.DeliveryAddress.Country))
                .ForMember(odtoMember => odtoMember.UserAddressState,
                    mceO => mceO.ResolveUsing(o => o.User.DeliveryAddress.State))
                .ForMember(odtoMember => odtoMember.UserAddressCity,
                    mceO => mceO.ResolveUsing(o => o.User.DeliveryAddress.City))
                .ForMember(odtoMember => odtoMember.UserAddressStreet,
                    mceO => mceO.ResolveUsing(o => o.User.DeliveryAddress.Street))
                .ForMember(odtoMember => odtoMember.UserAddressZip,
                    mceO => mceO.ResolveUsing(o => o.User.DeliveryAddress.Zip))
                .ForMember(odtoMember => odtoMember.Status,
                    mceO => mceO.ResolveUsing(o =>
                    {
                        switch (o.Status)
                        {
                            case OrderStatus.Created:
                                return OrderStatusDto.Created;
                            case OrderStatus.Delivered:
                                return OrderStatusDto.Delivered;
                            case OrderStatus.Dispatched:
                                return OrderStatusDto.Dispatched;
                            case OrderStatus.Paid:
                                return OrderStatusDto.Paid;
                            case OrderStatus.Picked:
                                return OrderStatusDto.Picked;
                            default:
                                throw new InvalidOperationException();
                        }
                    }));
            Mapper.CreateMap<OrderDto, Order>();
            Mapper.CreateMap<ProductCategorization, ProductCategorizationDto>();
            Mapper.CreateMap<ProductCategorizationDto, ProductCategorization>();
            Mapper.CreateMap<Role, RoleDto>();
            Mapper.CreateMap<RoleDto, Role>();
        }
    }
}