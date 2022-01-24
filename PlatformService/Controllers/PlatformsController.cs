using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Repositories;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : Controller
    {
        public PlatformsController(IMapper mapper, IPlatformRepository platformRepository)
        {
            _mapper = mapper;
            _platformRepository = platformRepository;
        }

        private readonly IMapper _mapper;

        private readonly IPlatformRepository _platformRepository;

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAll()
        {
            var platforms = await _platformRepository.GetAll();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<PlatformReadDto>> GetById(int id)
        {
            var platforms = await _platformRepository.GetById(id);

            return Ok(_mapper.Map<PlatformReadDto>(platforms));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<PlatformReadDto>> Create(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);
            await _platformRepository.CreatePlatform(platform);
            var createdPlatform = await _platformRepository.GetById(platform.Id);
            
            return Ok(_mapper.Map<PlatformReadDto>(createdPlatform));
        }
    }
}