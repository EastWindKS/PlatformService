using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Repositories;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataService.Http;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController(IMapper mapper, IPlatformRepository platformRepository, ICommandDataClient commandDataClient)
        {
            _mapper = mapper;
            _platformRepository = platformRepository;
            _commandDataClient = commandDataClient;
        }

        private readonly IMapper _mapper;
        private readonly IPlatformRepository _platformRepository;
        private readonly ICommandDataClient _commandDataClient;

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
            var platformReadDto = _mapper.Map<PlatformReadDto>(createdPlatform);

            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception)
            {
                throw new Exception("Failed in Create method");
            }

            return Ok(platformReadDto);
        }
    }
}