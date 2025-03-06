using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api_filmes_senai.Domains;
using api_filmes_senai.DTO;
using api_filmes_senai.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.Email!, loginDTO.Senha!);
                if (usuarioBuscado == null)
                {
                    return NotFound("Usuário não encontrado, email ou senha inválidos!");
                }
                //1º passo - definir as claims () que serão fornecidos no token(PayLoad)
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,usuarioBuscado.Email!),
                    new Claim(JwtRegisteredClaimNames.Name,usuarioBuscado.Nome!),

                    new Claim("Nome da Claim","Valor da Claim")
                };

                //2º passo - definir a key do projeto
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

                //3º passo - definir  as credencias do token (HEADER)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);    

                //4º passo - gerar o token
                var token = new JwtSecurityToken
                    (   //emissor de token
                        issuer: "api_fimes_senai",
                        //destinatario do token
                        audience: "api_fimes_senai",
                        //dadis deinidos na claims
                        claims: claims,
                        //tempo de expiração do token
                        expires: DateTime.Now.AddMinutes(5),
                        //credenciais do token
                        signingCredentials: creds
                    );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    }
                    );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);  
            }
        }
    }
}
