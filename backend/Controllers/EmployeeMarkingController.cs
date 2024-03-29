using AutoMapper;
using backend.Domain.Entities;
using backend.DTOs;
using backend.Exceptions;
using backend.Extension;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace backend.Controllers
{
    //[Authorize]
    [Route("v1/api/[controller]")]
    [ApiController]
    
    public class EmployeeMarkingController : ControllerBase
    {
        private readonly IEmployeeMarkingService _employeeMarkingService;
        private JsonSerializerOptions _serializerOptions;
        private readonly IMapper _mapper;

        public EmployeeMarkingController(IEmployeeMarkingService employeeMarkingService, IMapper mapper)
        {
            _employeeMarkingService = employeeMarkingService;
            _mapper = mapper;
            //Configurando o JsonSerializer para formatação dos dados corretos
            _serializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeMarking>>> GetAll() 
        { 
            var employeeMarking = await _employeeMarkingService.GetAllAsync();
            return Ok(employeeMarking);

        }

        //[HttpGet("markings/{id:int}")]
        //public async Task<ActionResult<IEnumerable<EmployeeMarking>>> GetEmployeeID(int id)
        //{
         //   var employeeMarking = await _employeeMarkingService.GetAsync(id);
          //  return Ok(JsonSerializer.Serialize(employeeMarking, _serializerOptions));

        //}

        [HttpGet("{employee_id:int}")]
        public async Task<ActionResult<IEnumerable<EmployeeMarkingDTOReport>>> Get(int employee_id, DateTime initialdate, DateTime finaldate) 
        {

            try
            {
                var teste = User;
                return Ok(await _employeeMarkingService.GetAsync(employee_id, initialdate, finaldate, User.GetProfileId(), User.GetUserId()));
            }
            catch (ErrorServiceException e)
            {
                return BadRequest(new { error = $"{e.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<EmployeeMarking>>> Post(EmployeMarkingDTO employeMarkingDTO)
        {
            try
            {
                var employeeMarkingCreated = await _employeeMarkingService.Add(employeMarkingDTO);
                return Ok(employeeMarkingCreated);
            }
            catch (ErrorServiceException e)
            {

                return BadRequest(new {error = e.Message});
            }
            

        }

        [HttpPut]
        public async Task<ActionResult<EmployeeMarking>> Put(EmployeMarkingDTO employeMarkingDTO)
        {
            if (employeMarkingDTO is null)
            {
                return BadRequest("Dados Invalidos");
            }

            try
            {
                var employUpdate = await _employeeMarkingService.Update(employeMarkingDTO);
                return new CreatedAtRouteResult("GetEmployee", new { id = employUpdate.Id }, employUpdate);
            }
            catch (ErrorServiceException e)
            {

                return BadRequest(new { error = $"{e.Message}" });
            }
        }
    }
}
