using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CpApi.DataBaseContext;
using CpApi.Interfaces;
using CpApi.Model;
using CpApi.Requests;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;

namespace CpApi.Service
{
    public class UserLoginService : IUsersLoginsService
    {
        private readonly ContextDb _context;

        public UserLoginService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var logins = await _context.Logins.ToListAsync();
            var users = await _context.Users.ToListAsync();

            return new OkObjectResult(new
            {
                data = new { users = users },
                status = true
            });
        }

        public async Task<IActionResult> CreateNewUserAndLoginAsync(CreateNewUserAndLogin newUser)
        {
            var user = new Users()
            {
                Name = newUser.Name,
                AboutMe = newUser.AboutMe,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var login = new Logins()
            {
                User_id = user.id_User,
                Email = newUser.Email,
                Password = newUser.Password,
            };

            await _context.Logins.AddAsync(login);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<IActionResult> AuthorizationAsync(string email, string pass)
        {
            var login = await _context.Logins.FirstOrDefaultAsync(a => a.Email == email && a.Password == pass);
            if (login is null)
            {
                return new NotFoundObjectResult(new { status = false, MessageContent = "Такого пользователя нет" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(a => a.id_User == login.User_id);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new(ClaimsIdentity.DefaultRoleClaimType, user.Admin ? "ADMIN" : "USER")
                };
                var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                
                // eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoi0JjQvNC40LvRjCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVTRVIiLCJuYmYiOjE3MzcxNDM5MzcsImV4cCI6MTczNzE0NTczNywiaXNzIjoiQVBJU2VydmVyIiwiYXVkIjoiQmxhem9yQXBwIn0.3h7tibNkInxpCLF3BUdhZCYgybFFBN-NsJxCznkpa-I
                
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: "APIServer",
                        audience: "BlazorApp",
                        notBefore: now,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(30)),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("UgZd07mr8o5gvtFUUUGcjT4e8q08mEuB")), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return new OkObjectResult(new { status = true, token = encodedJwt });
            }

            return new BadRequestObjectResult(new { status = false });
        }

        public async Task<IActionResult> DeleteUserAsync(int Id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.id_User == Id);
            var login = await _context.Logins.FirstOrDefaultAsync(a => a.User_id == Id);

            if (user is null)
            {
                return new NotFoundObjectResult(new { status = false, MessageContent = "Пользователь не найден" });
            }

            try 
            {
                _context.Remove(login);
                _context.Remove(user);
            }
            catch { return new NotFoundObjectResult(new { status = false, MessageContent = "Логин пользователя не найден" }); }
            
            await _context.SaveChangesAsync();
            return new OkObjectResult(new { status = true });
        }

        public async Task<IActionResult> EditUserAsync(Users user, string email, string pass)
        {
            var edituser = await _context.Users.FirstOrDefaultAsync(a => a.id_User == user.id_User);
            if (edituser is null)
            {
                return new NotFoundObjectResult(new { status = false, MessageContent = "Пользователь не найден" });
            }
            var login = await _context.Logins.FirstOrDefaultAsync(a => a.User_id == user.id_User);

            edituser.Name = user.Name;
            edituser.AboutMe = user.AboutMe;
            edituser.Admin = user.Admin;
            login.User_id = user.id_User;
            login.Email = email;
            login.Password = pass;

            await _context.SaveChangesAsync();
            return new OkObjectResult(new { status = true, edituser, login });

        }

        public async Task<IActionResult> GetUsersAsync()
        {
            var usersWithLogins = from user in _context.Users
                                  join login in _context.Logins on user.id_User equals login.User_id
                                  select new
                                  {
                                      user.id_User,
                                      user.Name,
                                      user.AboutMe,
                                      login.Email,
                                      login.Password
                                  };

            return new OkObjectResult(usersWithLogins.ToList());
        }
    }
}
