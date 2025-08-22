using EMC.BuildingBlocks.Dtos;
using EMC.BuildingBlocks.Interfaces;

namespace EMC.BuildingBlocks.Application.AppBuilders
{
    public class AddressAppBuilder : IAddressAppBuilder
    {
        public Task<AddressAppModel> Build(Address.Protos.AddressDto address,bool isCreate,bool isFavorit)
        {
            var model = new AddressAppModel
            {
                AddressId = address.AddressId,
                City = address.City,
                CityId=address.Cityid,
                Street = address.Street,
                StreetNumber = address.StreetNumber,
                Floor = address.Floor,
                FloorLetter = address.FloorLetter,
                Complement = address.Complement,
                State= address.State,
                Country= address.Country,
                ZipCode = address.Zipcode,
                IsCreate = isCreate,
                IsFavorit = isFavorit,
            };
            return Task.FromResult(model);
        }

        public Task<AddressAppModel> Build(AddressGenericDto person)
        {
            throw new NotImplementedException();
        }
    }
}
