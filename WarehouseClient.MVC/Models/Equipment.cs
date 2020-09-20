using WarehouseGrpc;

namespace WarehouseClient.MVC.Models
{
    public class Equipment
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public EquipmentType Type { get; set; }
    }
}