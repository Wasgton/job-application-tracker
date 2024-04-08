using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JobApplicationTracker.Application.Dto;
using Microsoft.IdentityModel.Tokens;

namespace JobApplicationTracker.Application.UseCases.AuthUseCases;

public interface ICreateTokenUseCase
{
    public JWTOutput Execute();
}

public class CreateTokenUseCase : ICreateTokenUseCase
{
    private IConfiguration _config;
    private readonly int _timeToExpire = 60;
    public CreateTokenUseCase(IConfiguration configuration)
    {
        _config = configuration;
    }

    public JWTOutput Execute()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            null,
            expires: DateTime.Now.AddMinutes(_timeToExpire),
            signingCredentials: credentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(sectoken);
        return new JWTOutput
        {
            Token = token,
            ExpireIn = _timeToExpire
        };
    }
}