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
        public async Task<ActionResult<List<UsuarioDto>>> GetUsuarios()
        {
            var usuarioList = await _usuarioRepository.GetAllAsync();
            return Ok(usuarioList.Select(usuario => _mapper.Map<UsuarioDto>(usuario)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(_mapper.Map<UsuarioDto>(usuario));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUsuario([FromBody] UsuarioDto novoUsuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(novoUsuarioDto);
            await _usuarioRepository.AddAsync(usuario);

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Usuario>>> UpdateUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id) return BadRequest("O ID fornecido na rota não corresponde ao ID no corpo da requisição.");

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            var dbUsuario = await _usuarioRepository.GetByIdAsync(usuarioDto.Id);

            if (dbUsuario == null) return NotFound("Usuário não encontrado");

            dbUsuario.Nome = usuario.Nome;
            dbUsuario.Sobrenome = usuario.Sobrenome;
            dbUsuario.Email = usuario.Email;
            dbUsuario.Escolaridade = usuario.Escolaridade;
            dbUsuario.DataNascimento = usuario.DataNascimento;

            await _usuarioRepository.UpdateAsync(dbUsuario);

            return NoContent();
        }

        [HttpDelete("{id}")]
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
