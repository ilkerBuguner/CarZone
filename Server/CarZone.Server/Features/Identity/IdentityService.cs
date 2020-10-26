namespace CarZone.Server.Features.Identity
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using CarZone.Server.Data;
    using CarZone.Server.Data.Models;
    using CarZone.Server.Features.Common.Models;
    using CarZone.Server.Features.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;

    using static CarZone.Server.Features.Common.Constants;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly CarZoneDbContext data;

        public IdentityService(
            UserManager<User> userManager,
            CarZoneDbContext data)
        {
            this.userManager = userManager;
            this.data = data;
        }

        public string GenerateJwtToken(string userId, string username, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        public async Task<ResultModel<AuthResponseModel>> RegisterAsync(
            string fullName,
            string userName,
            string email,
            string password,
            string secret)
        {
            var existingUserName = await this.userManager.FindByNameAsync(userName);

            if (existingUserName != null)
            {
                return new ResultModel<AuthResponseModel>
                {
                    Errors = new string[] { string.Format(Errors.AlreadyRegisteredUserName, userName) },
                };
            }

            var user = new User()
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
            };

            var registerAtempt = await this.userManager.CreateAsync(user, password);

            if (!registerAtempt.Succeeded)
            {
                return new ResultModel<AuthResponseModel>
                {
                    Errors = registerAtempt.Errors.Select(x => x.Description),
                };
            }

            var token = this.GenerateJwtToken(user.Id, userName, secret);

            return new ResultModel<AuthResponseModel>
            {
                Result = new AuthResponseModel
                {
                    Token = token,
                    User = new UserDetailsServiceModel
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        UserName = user.UserName,
                        ImageUrl = user.ProfilePictureUrl,
                        Email = user.Email,
                    },
                },
                Success = true,
            };
        }

        public async Task<ResultModel<AuthResponseModel>> LoginAsync(string username, string password, string secret)
        {
            var user = await this.userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new ResultModel<AuthResponseModel>
                {
                    Errors = new string[] { Errors.InvalidLoginAttempt },
                };
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                return new ResultModel<AuthResponseModel>
                {
                    Errors = new string[] { Errors.InvalidLoginAttempt },
                };
            }

            var token = this.GenerateJwtToken(user.Id, user.UserName, secret);

            return new ResultModel<AuthResponseModel>
            {
                Result = new AuthResponseModel
                {
                    Token = token,
                    User = new UserDetailsServiceModel
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        UserName = user.UserName,
                        ImageUrl = user.ProfilePictureUrl,
                        Email = user.Email,
                    },
                },
                Success = true,
            };
        }
    }
}
