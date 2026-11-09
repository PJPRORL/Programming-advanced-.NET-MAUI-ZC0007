using AutoMapper;
using AutoMapper.Configuration.Conventions;
using GameLibraryAPI.Data.UnitOfWork;
using GameLibraryAPI.Dto;
using GameLibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace GameLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public GameController(IMapper mapper, IUnitOfWork context)
        {
            _mapper = mapper;
            _context = context;
        }

        //
        //  To do
        //
        







    }
}
