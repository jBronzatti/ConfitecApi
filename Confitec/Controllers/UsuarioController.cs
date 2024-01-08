using AutoMapper;
using Confitec.Data;
using Confitec.Dtos;
using Confitec.Models;
using Confitec.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Confitec.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Usuario>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return Ok(await _usuarioRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Usuario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Usuario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUsuario([FromBody] UsuarioCreateDto novoUsuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(novoUsuarioDto);
            await _usuarioRepository.AddAsync(usuario);

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Usuario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Usuario>>> UpdateUsuario(int id, [FromBody] UsuarioCreateDto usuario)
        {
            var dbUsuario = await _usuarioRepository.GetByIdAsync(id);

            if (dbUsuario == null) return NotFound("Usuário não encontrado");

            dbUsuario.Nome = usuario.Nome;
            dbUsuario.Sobrenome = usuario.Sobrenome;
            dbUsuario.Email = usuario.Email;
            dbUsuario.Escolaridade = usuario.Escolaridade;
            dbUsuario.DataNascimento = usuario.DataNascimento;

            await _usuarioRepository.UpdateAsync(dbUsuario);

            return Ok(dbUsuario);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Usuario))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            await _usuarioRepository.DeleteAsync(usuarioExistente);

            return NoContent();
        }
    }
}
