using api_filmes_senai.Domains;
using api_filmes_senai.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;
        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }
        /// <summary>
        /// endpoint para listar os filmes pelo id do Gênero
        /// </summary>
        /// <returns>Listar pelo Gênero</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_generoRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Endpoint para Cadastrar o Gênero
        /// </summary>
        /// <param name="novoGenero">Cadastrar</param>
        /// <returns>Cadastrar Gênenro</returns>
        [Authorize]   
        [HttpPost]
        public IActionResult Post(Genero novoGenero)
        {
            try
            {
                _generoRepository.Cadastrar(novoGenero);

                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Endpoint para buscar um Gênero pelo seu id
        /// </summary>
        /// <param name="id">Id do Gênero buscado</param>
        /// <returns>Gênero buscado</returns>

        [HttpGet("BuscarPorId/{id}")]

        public IActionResult GetById(Guid id)
        {
            try
            {
                Genero generoBuscado = _generoRepository.BuscarPorId(id);

                return Ok(generoBuscado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// ENdpoint para deletar o Gênero pelo seu id
        /// </summary>
        /// <param name="id">Id do Gênero que ira ser deletado</param>
        /// <returns>Deletar Gênero</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _generoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Endpoint para atualizar o Gênero pelo seu id
        /// </summary>
        /// <param name="id">Id do Gênero para atualizar</param>
        /// <returns>Atualizar Gênero</returns>
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Genero genero)
        {
            try
            {
                _generoRepository.Atualizar(id, genero);

                return NoContent();
            }
            catch (Exception e)
            {

               return BadRequest(e.Message);
            }
        }

    }
}