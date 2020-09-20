using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarehouseGrpc.Model;

namespace WarehouseGrpc.Data
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private  DataContext _context;

        public EquipmentRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<Confirmation> AddEquipment(Equipment equipment)
        {
            await _context.Equipments.AddAsync(equipment);
            await _context.SaveChangesAsync();

            var eq=await _context.Equipments. FirstOrDefaultAsync(x=>x.SerialNumber.Equals(equipment.SerialNumber));

            if(eq==null)
                return new Confirmation{
                    Code=404,
                    Message="Equipment with this Serial Number has been already added"
                };

            
            return new Confirmation{
                Code=200,
                Message="Product has been succesfuly added"
            };
        }

        public async Task<bool> Exists(string serialNumber)
        {
            var alreadyExist= await _context.Equipments.FirstOrDefaultAsync(x=>x.SerialNumber.Equals(serialNumber));

            return alreadyExist !=null;
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipments()
        {
          return await  _context.Equipments.Where(x=>true).ToListAsync();
        }
    }
}