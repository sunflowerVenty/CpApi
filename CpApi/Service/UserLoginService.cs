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
            try
            {
                var emailcheck = await _context.Logins.FirstOrDefaultAsync(a => a.Email == newUser.Email);
                if (emailcheck == null)
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
                else
                {
                    return new BadRequestObjectResult(new
                    {
                        status = false
                    });
                }
            }
            catch (Exception ex) 
            {
                return new BadRequestObjectResult(new
                {
                    status = false
                });
            }
            
        }


        public async Task<IActionResult> AuthorizationAsync([FromBody] AuthUser authuser)
        {
            var login = await _context.Logins.FirstOrDefaultAsync(a => a.Email == authuser.Email && a.Password == authuser.Password );
            if (login is null)
            {
                return new UnauthorizedObjectResult(new { status = false });
            }

            var user = await _context.Users.FirstOrDefaultAsync(a => a.id_User == login.User_id);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new("id_User", Convert.ToString(user.id_User)),
                    new("Name", user.Name),
                    new("AboutMe", user.AboutMe),
                    new("isAdmin", Convert.ToString(user.Admin))
                };
                var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
              
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: "APIServer",
                        audience: "BlazorApp",
                        notBefore: now,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(10)),
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

        public async Task<IActionResult> EditUserAsync([FromBody] UserInfo userInfo)
        {
            var checkemail = await _context.Logins.FirstOrDefaultAsync(s => s.Email == userInfo.Email && s.User_id != userInfo.id_User);

            if (checkemail is null)
            {
                var edituser = await _context.Users.FirstOrDefaultAsync(a => a.id_User == userInfo.id_User);
                if (edituser is null)
                {
                    return new NotFoundObjectResult(new { status = false, MessageContent = "Пользователь не найден" });
                }
                var login = await _context.Logins.FirstOrDefaultAsync(a => a.User_id == userInfo.id_User);

                edituser.Name = userInfo.Name;
                edituser.AboutMe = userInfo.AboutMe;
                login.User_id = userInfo.id_User;
                login.Email = userInfo.Email;
                login.Password = userInfo.Password;

                await _context.SaveChangesAsync();
                return new OkObjectResult(new { status = true, edituser, login });
            }
            else
            {
                return new BadRequestObjectResult(new { status = false });
            }
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

        public Task<IActionResult> EditUserAsync(Users user, string email, string pass)
        {
            throw new NotImplementedException();
        }
    }
}
