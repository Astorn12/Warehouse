using System;
using FluentValidation;
using WarehouseGrpc.Data;
using WarehouseGrpc.Model;

namespace WarehouseGrpc.Validators
{
    public class EquipmentValidator : AbstractValidator<Equipment>
    {
        private  IEquipmentRepository _repo;
       
        public EquipmentValidator(IEquipmentRepository repo)
        {
            _repo = repo;
            RuleFor(x => x.Name).Length(0, 50);
            RuleFor(x => x.SerialNumber).Length(0,11);
            RuleFor(x=>x.Type).Must(type=>Enum.IsDefined(typeof(EquipmentType),type));
            RuleFor(x=>x.SerialNumber).MustAsync(async(serialNumber,cancellation)=> !await  this._repo.Exists(serialNumber)).WithMessage("This poduct has been already in the database");
        }
    }

}