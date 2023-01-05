using Cursos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PSW_Cursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_Cursos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly cursosContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(cursosContext context, ILogger<WeatherForecastController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public IEnumerable<Estudiante> Get()
        {
            return _context.Estudiante.ToList();
        }
    }
}
