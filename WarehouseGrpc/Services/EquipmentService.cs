using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Google.Protobuf.Collections;
using Grpc.Core;
using WarehouseGrpc.Data;
using WarehouseGrpc.Model;
using WarehouseGrpc.Validators;

namespace WarehouseGrpc.Services
{
    public class EquipmentService : Equipments.EquipmentsBase
    {

        private IEquipmentRepository _repo;
        private IMapper _mapper;

        private  IValidator<Equipment> _validator;

        public EquipmentService(IEquipmentRepository repo, IMapper mapper, IValidator<Equipment> validator)
        {
            _repo = repo;
            _mapper = mapper;
            _validator = validator;
        }
        public override async Task<Confirmation> AddEquipment(EquipmentToAdd request, ServerCallContext context)
        {

            Equipment equipment = _mapper.Map<Equipment>(request);

            

            var precipitate = await _validator.ValidateAsync(equipment);

            if (precipitate.IsValid)
                return await _repo.AddEquipment(equipment);
            else
                return new Confirmation()
                {
                    Code = 404,
                    Message = precipitate.ToString()
                };
        }



        public override async Task<EquipmentsListResponse> GetAllEquipments(EquipmentsRequest request, ServerCallContext context)
        {

            var equipmentsFromDb = await _repo.GetAllEquipments();
            var equipmentsListToReturn = _mapper.Map<List<EquipmentToReturn>>(equipmentsFromDb);

            var response = new EquipmentsListResponse();

            foreach (var equipment in equipmentsListToReturn)
            {
                response.EquipmentList.Add(equipment);
            }
            return response;
        }

    }
}