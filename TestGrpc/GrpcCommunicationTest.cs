using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Net.Client;
using NUnit.Framework;
using WarehouseGrpc;

using static WarehouseGrpc.Equipments;

namespace TestGrpc
{
    public class GrpcCommunicationTest
    {
        GrpcChannel _channel;
        EquipmentsClient _client;
        Random _random;
        EquipmentsRequest _equipmentsRequest;

        string _uniqSerialNumber;
        EquipmentsListResponse _warehouseBeforeAdd;

        [SetUp]
        public void Setup()
        {
            string serverAddress = "https://localhost:5002";
            _channel = GrpcChannel.ForAddress(serverAddress);
            _client = new EquipmentsClient(_channel);
            _random= new Random();
            _equipmentsRequest = new EquipmentsRequest();

            _warehouseBeforeAdd =  _client.GetAllEquipments(_equipmentsRequest);

            _uniqSerialNumber = findUniqSerialNumber(_warehouseBeforeAdd.EquipmentList,10);

        }

        [Test]
        public async Task AddNewEquipment()
        {
            EquipmentToAdd newEquipment= new EquipmentToAdd{
                Name="Nowy Sprzęt Testowy",
                SerialNumber=_uniqSerialNumber,
                Type=EquipmentType.Agd
                
            };

            await _client.AddEquipmentAsync(newEquipment);

            var warehouseAfterAdd= _client.GetAllEquipments(_equipmentsRequest);

            int warehouseBeforeSize=_warehouseBeforeAdd.EquipmentList.Count;
            int warehouseAfterSize=warehouseAfterAdd.EquipmentList.Count;

            Assert.AreEqual(1,warehouseAfterSize-warehouseBeforeSize );
        }

        [Test]
        public async Task AddTwoEquipmentWithSameSerialNumber(){

            EquipmentToAdd doubledEquipment= new EquipmentToAdd{
                Name="Nowy Sprzęt Testowy",
                SerialNumber=_uniqSerialNumber,
                Type=EquipmentType.Agd
            };
            await _client.AddEquipmentAsync(doubledEquipment);

            doubledEquipment.Name="Doubled Equipment";
            doubledEquipment.Type=EquipmentType.Vehicle;

            await _client.AddEquipmentAsync(doubledEquipment);

            var warehouseAfterAdd= _client.GetAllEquipments(_equipmentsRequest);

            int warehouseBeforeSize=_warehouseBeforeAdd.EquipmentList.Count;
            int warehouseAfterSize=warehouseAfterAdd.EquipmentList.Count;

            
            Assert.AreEqual(1,warehouseAfterSize-warehouseBeforeSize );

        }

        [Test]
        public async Task AddEquipmentWithToLongSerialNumber(){

            EquipmentToAdd errorEquipment= new EquipmentToAdd{
                Name="Nowy Sprzęt Testowy",
                SerialNumber=findUniqSerialNumber(_warehouseBeforeAdd.EquipmentList, 12),
                Type=EquipmentType.Agd
            };
            await _client.AddEquipmentAsync(errorEquipment);

            var warehouseAfterAdd= _client.GetAllEquipments(_equipmentsRequest);

            int warehouseBeforeSize=_warehouseBeforeAdd.EquipmentList.Count;
            int warehouseAfterSize=warehouseAfterAdd.EquipmentList.Count;

            
            Assert.AreEqual(0,warehouseAfterSize-warehouseBeforeSize );

        }

        

        private string findUniqSerialNumber(RepeatedField<EquipmentToReturn> equipments,int n)
        {

            bool notFinded=true;
            string newSerialNumber="";
            while(notFinded){
           newSerialNumber= RandomString(n);

            var equipmentWithIdenticalSN=equipments.FirstOrDefault(x=>x.SerialNumber.Equals(newSerialNumber));

            if(equipmentWithIdenticalSN==null){
                notFinded=false;
            }

            }

            return newSerialNumber;
        }

      
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        
    }
}