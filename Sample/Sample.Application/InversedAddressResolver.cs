using AutoMapper;
using Bdf.Sample.Domain.Model;
using Sample.Application.Dtos.Order;

namespace Sample.Application
{
    public class InversedAddressResolver : ValueResolver<Address, AddressDto>
    {
        protected override AddressDto ResolveCore(Address source)
        {
            return new AddressDto
            {
                City = source.City,
                Country = source.Country,
                State = source.State,
                Street = source.Street,
                Zip = source.Zip
            };
        }
    }
}