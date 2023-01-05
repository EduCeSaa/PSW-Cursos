using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cursos.Helper;
using Cursos.Models;
using Cursos.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSW_Cursos.Models;

namespace Cursos.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController:Controller
    {
        cursosContext context; 
        
        public UsuariosController(cursosContext _context)
        {
            context = _context;
        }
        
        public async Task<IActionResult> Get()
        {
            List<UsuarioVM> Usuarios = await context.Usuarios.Select(x=>new UsuarioVM(){
                IdUsuario = x.IdUsuario,
                Usuario = x.Usuario
            }).ToListAsync();
            return Ok(Usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UsuarioVM Usuarios = await context.Usuarios.Where(x=>x.IdUsuario == id).Select(x=>new UsuarioVM(){
                IdUsuario = x.IdUsuario,
                Usuario = x.Usuario
            }).SingleOrDefaultAsync();
            return Ok(Usuarios);
        }

        [HttpPost]

        public async Task<IActionResult> Post(Usuarios Usuario)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            }

            if(await context.Usuarios.Where(x=>x.Usuario == Usuario.Usuario).AnyAsync())
            {
                return BadRequest(ErrorHelper.Response(400, $"El usuario {Usuario.Usuario} ya existe."));
            }

            HashedPassword Password = HashHelper.Hash(Usuario.Clave);
            Usuario.Clave = Password.Password;
            Usuario.Sal = Password.Salt;
            context.Usuarios.Add(Usuario);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {id=Usuario.IdUsuario}, new UsuarioVM(){
                IdUsuario = Usuario.IdUsuario,
                Usuario = Usuario.Usuario
            });
        }
    }
}