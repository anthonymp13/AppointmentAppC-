using AppointmentAppBackend;
using AppointmentAppBackend.Model;
using AppointmentAppBackend.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace AppointmentAppBackend.Services;

public class JWTManagerService : IJWTManagerService 
{
	private readonly List<Users> users = new(){
		new Users(1, "Bob", "BobPassword"),
		new Users(2, "Fred", "FredPassword"),
		new Users(3, "George", "GeorgePassword")
	};

	private readonly IConfiguration _configuration;

	public JWTManagerService(IConfiguration iconfiguration)
	{
		_configuration = iconfiguration;
	}

	public Tokens Authenticate(Users user)
	{

		var username = user.Username;
		var password = user.Password;

		foreach (var currentUser in users)
        {

			// Generate JSON token
			if (currentUser.Username == username && currentUser.Password == password)
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]);
				var claim = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()) };

				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claim),
					Expires = DateTime.UtcNow.AddMinutes(10),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
			
				return new Tokens { Token = tokenHandler.WriteToken(token) };
			}
        }

		return null;

	}
}

