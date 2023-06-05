using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MyDbContext _context;
        public UserController(IConfiguration config, MyDbContext myDbContext) {
            _config = config;
            _context = myDbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {

            var user = await _context.Users.Where(e => e.Username == dto.Username).FirstOrDefaultAsync();
            if (user == null)
            {
                return Conflict("User not found");
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: dto.Password,
                salt: user.Salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));

            if (hashed != user.Password) {
                return Unauthorized("Wrong username or password");
            }

            var tokenHanlder = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!)),
                        SecurityAlgorithms.HmacSha256
                )
            };
            var token = tokenHanlder.CreateToken(tokenDescription);
            var stringifiedToken = tokenHanlder.WriteToken(token);

            var refTokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!)),
                        SecurityAlgorithms.HmacSha256
                )
            };
            var refToken = tokenHanlder.CreateToken(refTokenDescription);
            var stringifiedRefToken = tokenHanlder.WriteToken(refToken);
            user.RefreshToken = stringifiedRefToken.ToString();
            user.RefeshTokenExp = DateTime.Now.AddMinutes(15);
            await _context.SaveChangesAsync();
            return Ok(new LoginResponseDto
            {
                Token = stringifiedToken,
                RefreshToken = stringifiedRefToken
            });
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddUser(LoginRequestDto dto)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: dto.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
                ));

            var user = new User
            {
                Username = dto.Username,
                Password = hashed,
                Salt = salt
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
        {
            var tokenHanlder = new JwtSecurityTokenHandler();
            try
            {
                tokenHanlder.ValidateToken(dto.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JWT:Issuer"],
                    ValidAudience = _config["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!))
                }, out SecurityToken validatedToken);
                var user = _context.Users.Where(e => e.RefreshToken == dto.RefreshToken).FirstOrDefault();

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Issuer = _config["JWT:Issuer"],
                    Audience = _config["JWT:Audience"],
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!)),
                            SecurityAlgorithms.HmacSha256
                    )
                };
                var token = tokenHanlder.CreateToken(tokenDescription);
                var stringifiedToken = tokenHanlder.WriteToken(token);
                user.RefeshTokenExp = DateTime.UtcNow.AddMinutes(15);
                await _context.SaveChangesAsync();
                return Ok(true + " " + stringifiedToken);
            }
            catch
            {
                return Unauthorized();
            }
        }

    }
}
