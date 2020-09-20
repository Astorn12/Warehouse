using System;
using FluentValidation;

namespace WarehouseGrpc.Model
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber  { get; set; }
        public EquipmentType Type { get; set; }
    }


    
}