using AutoMapper;
using backend.Domain.Entities;
using backend.DTOs;
using backend.DTOs.Mapping;
using backend.Extension;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TitleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Title> _repository;

        public TitleController(IRepository<Title> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TitleDTO>>> Get()
        {
            
            var title = await _repository.GetAllAsync();
            var titleDTO = _mapper.Map<IEnumerable<TitleDTO>>(title);
            return Ok(titleDTO);
        }

        [HttpGet("{id:int}", Name = "GetTitle")]
        public async Task<ActionResult<TitleDTO>> Get(int id) 
        {
            var title = await _repository.GetAsync(t => t.Id == id);
            if(title is null)
            {
                return NotFound($"Cargo com id= {id} não encontrado");
            }

            var titleDTO = _mapper.Map<TitleDTO>(title);
            return Ok(title);
        }
    }
}
