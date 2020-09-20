using Microsoft.EntityFrameworkCore;
using WarehouseGrpc.Model;

namespace WarehouseGrpc.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<Equipment> Equipments { get; set; }
    }
}