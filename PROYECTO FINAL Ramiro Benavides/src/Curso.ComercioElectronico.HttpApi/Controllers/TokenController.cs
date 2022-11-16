using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Proyecto.Ecommerce.WebAPI.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Curso.ComercioElectronico.HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtConfiguration jwtConfiguration;
        private readonly IClienteAppService clienteAppService;

        public TokenController(IOptions<JwtConfiguration> options, IClienteAppService clienteAppService)
        {
            this.jwtConfiguration = options.Value;
            this.clienteAppService = clienteAppService;
        }


        [HttpPost]
        public async Task<string> TokenAsync(UserInput input)
        {

            var clientes = await clienteAppService.GetClientesDtoAsync();
            var userTest = new List<string>();
            var passwordTest = new List<string>();
            foreach (var cliente in clientes)
            {
                userTest.Add(cliente.Usuario);
            }

            foreach (var cliente in clientes)
            {
                passwordTest.Add(cliente.Contraseña);
            }

            if (userTest.Contains(input.UserName)==false || passwordTest.Contains(input.Password)==false)
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }

            //2. Generar claims
            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, input.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", input.UserName),
                        //new Claim("Email", user.Email)
                        //Other...
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwtConfiguration.Expires),
                signingCredentials: signIn);
 

           var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


            return jwt;
        }
    }

    public class UserInput
    {

        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
