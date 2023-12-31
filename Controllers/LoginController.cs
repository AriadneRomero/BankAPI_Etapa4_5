using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankApi.Data.DTOs;
using BankAPI.Data.BankModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService loginService; //inyectar el servicio
    private IConfiguration config;
    public LoginController(LoginService loginService, IConfiguration config)
    {
        this.loginService = loginService;
        this.config = config;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Login(AdminDto adminDto)
    {
        var admin = await loginService.GetAdmin(adminDto);

        if(admin is null)
            return BadRequest( new { message = "Credenciales inválidas." });
        
        string jwtToken = GenerateToken(admin);

        return Ok( new { token = jwtToken } );
    }

    private string GenerateToken(Administrator admin)
    {
        var claims = new[] 
        {
            new Claim(ClaimTypes.Name, admin.Name),
            new Claim(ClaimTypes.Email, admin.Email),
            new Claim("AdminType", admin.AdminType)
            //Claim: Conjunto de valores que tiene que ver con la identidad del usuario
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //credencial para llevar una llave digital

        var securityToken = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: creds);
        
        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

    [HttpPost("authenticate/client")]
    public async Task<IActionResult> LoginClient(ClientDto clientDto)
    {
        var client = await loginService.GetClient(clientDto);

        if(client is null)
            return BadRequest( new { message = "Credenciales inválidas2." });
        
        string jwtToken = GenerateClientToken(client);

        return Ok( new { token = jwtToken } );
    }

    private string GenerateClientToken(Client client)
    {
        var claims = new[] 
        {
            new Claim(ClaimTypes.Name, client.Name),
            new Claim(ClaimTypes.Email, client.Email),
            //Claim: Conjunto de valores que tiene que ver con la identidad del usuario
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //credencial para llevar una llave digital

        var securityToken = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: creds);
        
        string token = new JwtSecurityTokenHandler().WriteToken(securityToken); //Token Serializado

        return token;
    }


}