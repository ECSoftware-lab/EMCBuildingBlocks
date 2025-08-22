using EMC.Address.Protos;
using EMC.BuildingBlocks.Dtos;

namespace EMC.BuildingBlocks.Interfaces
{
    public interface IAddressAppBuilder
    {
        Task<AddressAppModel> Build(AddressGenericDto person); 
        Task<AddressAppModel> Build(AddressDto address, bool isCreate, bool isFavorit);
    }
}
