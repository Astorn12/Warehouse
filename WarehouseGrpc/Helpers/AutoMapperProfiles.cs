using AutoMapper;
using WarehouseGrpc.Model;

namespace WarehouseGrpc.Helpers
{
    public class AutoMapperProfiles :Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<EquipmentToAdd,Equipment>();
            CreateMap<Equipment,EquipmentToReturn>();
        }
    }
}