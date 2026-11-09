using AutoMapper;
using GameLibraryAPI.Dto;
using GameLibraryAPI.Dto.Gebruiker;
using GameLibraryAPI.Helper;
using GameLibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameLibraryAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly SignInManager<Gebruiker> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public GebruikerController(UserManager<Gebruiker> userManager,
            SignInManager<Gebruiker> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // Registeren als nieuwe gebruiker
        [HttpPost("register")]
        public async Task<IActionResult> Register(GebruikerRegistratieDto request)
        {
            string rol = string.Empty;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var gebruiker = await _userManager.FindByEmailAsync(request.Email);
            if (gebruiker == null)
            {
                var user = new Gebruiker
                {
                    UserName = request.Name,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    EmailConfirmed = true,
                };

                // BELANGRIJK
                if (_userManager.Users.Any())
                {
                    rol = "Lifecoach";
                }
                else
                {
                    rol = "Coach";
                }

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, rol);
                    return Ok();
                }
                else
                {
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError("message", error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("message", "Gebruiker is aanwezig is database.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(GebruikerInlogDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("message", "Deze gebruiker bestaat niet.");
                return BadRequest(ModelState);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("message", "Het emailadres is nog niet bevestigd.");
                return BadRequest(ModelState);
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
            {
                ModelState.AddModelError("message", "Verkeerde logincombinatie!");
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("message", "Account geblokkeerd!!");
                return BadRequest(ModelState);
            }


            if (result.Succeeded)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles != null)
                {
                    foreach (var userRole in userRoles)
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = Token.GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            ModelState.AddModelError("message", "Ongeldige loginpoging");
            return Unauthorized(ModelState);
        }

        [HttpGet("GetAlleUsersMetRollen")]
        public async Task<IActionResult> GetAlleUsersMetRollen()
        {
            var users = await _userManager.Users.ToListAsync();

            var userWithRolesList = new List<GebruikerMetRollenDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userWithRolesList.Add(new GebruikerMetRollenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles
                });
            }

            return Ok(userWithRolesList);
        }

        [HttpPost("GrantPermission")]
        public async Task<IActionResult> GrantPermission(GrantPermissionDto gpm)
        {
            Gebruiker? gb = await _userManager.FindByEmailAsync(gpm.Email);
            IdentityRole? rol = await _roleManager.FindByNameAsync(gpm.RolNaam);


            if (gb != null && rol != null)
            {
                var huidigeRoles = await _userManager.GetRolesAsync(gb);

                if (huidigeRoles.Any())
                {
                    var huidigeRole = huidigeRoles.First();
                    var removeResult = await _userManager.RemoveFromRoleAsync(gb, huidigeRole);
                    if (!removeResult.Succeeded)
                    {
                        if (removeResult.Errors.Count() > 0)
                        {
                            foreach (var error in removeResult.Errors)
                                ModelState.AddModelError("message", error.Description);
                        }
                        return BadRequest(ModelState);
                    }
                }

                IdentityResult res = await _userManager.AddToRoleAsync(gb, rol.Name);

                if (res.Succeeded)
                    return Ok();
                else
                {
                    foreach (IdentityError error in res.Errors)
                        ModelState.AddModelError("", error.Description);

                    return BadRequest(ModelState);
                }
            }
            else
                ModelState.AddModelError("", "De gebruiker bestaat niet.");

            ModelState.AddModelError("message", "Onbekende fout");
            return Unauthorized(ModelState);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Gebruiker? gebruiker = await _userManager.FindByIdAsync(id);
            if (gebruiker != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(gebruiker);
                if (result.Succeeded)
                    return Ok("De gebruiker is succesvol verwijderd.");
                else
                {
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError("message", error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ResetUserPassword(string userId, string newPassword)
        {
            // Stap 1: Zoek de gebruiker op basis van userId
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Gebruiker niet gevonden");
            }

            // Stap 2: Genereer een reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Stap 3: Reset het wachtwoord met het gegenereerde token
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            // Stap 4: Controleer of het resetten gelukt is
            if (resetPasswordResult.Succeeded)
            {
                return Ok("Wachtwoord succesvol aangepast");
            }

            // Als er fouten zijn, stuur deze terug
            foreach (var error in resetPasswordResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return BadRequest(ModelState);
        }

    }
}
