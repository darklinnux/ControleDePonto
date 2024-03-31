using backend.Context;
using backend.Domain.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using backend.DTOs;
using backend.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private JsonSerializerOptions _serializerOptions;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

            //Configurando o JsonSerializer para formatação dos dados corretos
            _serializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> ListAll()
        {
            var employees = await _employeeService.GetAllAsync();

            
            return Ok(employees);
            //return Ok(JsonSerializer.Serialize(employees, _serializerOptions));
        }

        [HttpGet("{id:int}", Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> GetByID(int id)
        {
            if (id == 0) 
            { 
                BadRequest("Dados Invalidos");
            }

            try
            {
                var employee = await _employeeService.GetAsync(id);
                return Ok(JsonSerializer.Serialize(employee, _serializerOptions));

            }catch(ErrorServiceException e)
            {
                return BadRequest( new { error = $"{e.Message}" });
            }

        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(EmployeeDTOCreate employeeDTO)
        {
            if (employeeDTO is null)
            {
                return BadRequest("Dados Invalidos");
            }

            try
            {
                var employCreated = await _employeeService.Add(employeeDTO);
                return new CreatedAtRouteResult("GetEmployee", new { id = employCreated.Id }, employCreated);
            }
            catch (ErrorServiceException e)
            {

                return BadRequest(new {error = $"{e.Message}"});
            }
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> Put(EmployeeDTO employeeDTO)
        {
            if (employeeDTO is null)
            {
                return BadRequest("Dados Invalidos");
            }

            try
            {
                var employUpdate = await _employeeService.Update(employeeDTO);
                return new CreatedAtRouteResult("GetEmployee", new { id = employUpdate.Id }, employUpdate);
            }
            catch (ErrorServiceException e)
            {

                return BadRequest(new { error = $"{e.Message}" });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                BadRequest("Dados Invalidos");
            }
            try
            {

                var employee = await _employeeService.Delete(id);
                return Ok(employee);
            }
            catch (ErrorServiceException e)
            {

                return BadRequest(new {error =$"{e.Message}"});
            }
            
        }
    }
}
