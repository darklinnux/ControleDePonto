using backend.Domain.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace backend.Controllers
{
    [Route("v1/api/[controller]")]
    [Authorize]
    [ApiController]
    public class MarkingController : ControllerBase
    {
        private readonly IMarkingService _markingService;
        private JsonSerializerOptions _serializerOptions;

        public MarkingController(IMarkingService markingService)
        {
            _markingService = markingService;
            //Configurando o JsonSerializer para formatação dos dados corretos
            _serializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marking>>> Get() 
        {
            return Ok( await _markingService.GetAllAsync());
        }
    }
}
