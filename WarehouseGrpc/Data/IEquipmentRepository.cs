using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseGrpc.Model;

namespace WarehouseGrpc.Data
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetAllEquipments();
        Task<Confirmation> AddEquipment(Equipment equipment);
        Task<bool> Exists(string serialNumber);
    }
}