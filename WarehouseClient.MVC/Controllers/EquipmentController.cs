using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using WarehouseClient.MVC.Models;
using WarehouseGrpc;
using static WarehouseGrpc.Equipments;

namespace WarehouseClient.MVC.Controllers
{
    public class EquipmentController : Controller
    {
        GrpcChannel _channel;
        EquipmentsClient _client;

        public EquipmentController()
        {
            string serverAddress = "https://localhost:5002";
            _channel = GrpcChannel.ForAddress(serverAddress);
            _client = new Equipments.EquipmentsClient(_channel);
        }
        public IActionResult Index()
        {
            
            return View();
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EquipmentToAdd equipment)
        {
            var reply = await _client.AddEquipmentAsync(equipment);

            if(reply.Code==200)
                return RedirectToAction("List");
            else{ 
                ViewBag.Message = String.Format(reply.Message);
                return View();
                }  
        }
 
        public IActionResult List()
        {
            var equipmentsRequest = new EquipmentsRequest();
            EquipmentsListResponse reply = _client.GetAllEquipments(equipmentsRequest);
            return View(reply);
        }
    }
}