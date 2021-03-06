﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoppingApi.Domain;
using System.Linq;
using System.Threading.Tasks;
using ShoppingApi.Models.Catalog;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;

namespace ShoppingApi.Controllers
{
    public class CatalogController : ControllerBase
    {
        private readonly ShoppingDataContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfig;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ShoppingDataContext context, IConfiguration config, IMapper mapper, MapperConfiguration mapperConfig, ILogger<CatalogController> logger)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
            _mapperConfig = mapperConfig;
            _logger = logger;
        }

        [HttpPost("catalog")]
        public async Task<ActionResult> AddItem([FromBody] PostCatalogRequest newItem)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogInformation("Got a bad request. Looked like this {@newItem}", newItem);
                return BadRequest(ModelState);
            } else
            {
                var item = _mapper.Map<ShoppingItem>(newItem);
                _context.ShoppingItems.Add(item);
                await _context.SaveChangesAsync();
                var response = _mapper.Map<GetCatalogResponseSummaryItem>(item);
                return StatusCode(201, response);
            }
        }

        [HttpGet("catalog")]
        public async Task<ActionResult> GetFullCatalog()
        {
            var data = await _context
                .ShoppingItems
                .TagWith("catalog#getfullcatalog") // adds tag to logs
                .Where(item => item.InInventory)
                //.Select(item => _mapper.Map<GetCatalogResponseSummaryItem>(item))
                .ProjectTo<GetCatalogResponseSummaryItem>(_mapperConfig)
                .ToListAsync();

            var response = new GetCatalogResponse
            {
                Data = data
            };

            _logger.LogInformation("Got a get on catalog.");

            return Ok(response);
        }
    }
}
