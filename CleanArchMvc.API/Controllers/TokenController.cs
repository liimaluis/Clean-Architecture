using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate authenticate;
        private readonly IConfiguration configuration;

        public TokenController(IAuthenticate authenticate, IConfiguration configuration) 
        {
            this.authenticate = authenticate;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {
            var result = await authenticate.Authenticate(loginModel.Email, loginModel.Password);

            if (result)
            {
                return GenerateToken(loginModel);
                //return Ok("Login successfully");
            }
            else
            {
                return BadRequest("Invalid Login attempt");
            }
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel registerModel)
        {
            var result = await authenticate.ResgisterUser(registerModel.Email, registerModel.Password);

            if (result)
            {
                return Ok($"User {registerModel.Email} login successfully");
            }
            else
            {
                return BadRequest("Invalid Register User");
            }
        }

        private UserToken GenerateToken(LoginModel loginModel)
        {
            //declaração do usuário
            var claims = new[]
            {
                new Claim("email", loginModel.Email),
                new Claim("valor", "valor para agregar"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));

            //gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken
                (
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
