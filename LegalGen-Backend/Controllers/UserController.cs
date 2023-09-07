using Microsoft.AspNetCore.Mvc;
using LegalGen_Backend.DBContext;
using Task_Management_System.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LegalGen_Backend.Models;
using Microsoft.AspNetCore.Identity;
using NuGet.Common;

namespace LegalGen_Backend.Controllers
{
    [Route("LegelGen/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // POST: api/UserRegisterModels
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel userRegisterModel)
        {
            if (!ModelState.IsValid)
            {   
                return BadRequest(ModelState);
            }

            User user = new User()
            {
                UserName = userRegisterModel.Email,
                Email = userRegisterModel.Email,
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                ContactDetails = userRegisterModel.ContactDetails,
                Organization = userRegisterModel.Organization,
            };
                var result = await _userManager.CreateAsync(user, userRegisterModel.Password);
            

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return Ok();

            }
            else
            {
                return BadRequest("User already Exist");
            }

        }
            
        [HttpPut("update-profile")]
        // PUT: LegelGen/User/Update/{id}
        public async Task<IActionResult> UpdateUser([FromBody] UserRegisterModel userUpdateModel)
        {
            // Get the current user
            try
            {       
                // Find the user by email
                var currentUser = await _userManager.FindByEmailAsync(userUpdateModel.Email);

                if (currentUser == null)
                {
                    return NotFound("User not found");
                }

                // Update user properties
                currentUser.FirstName = userUpdateModel.FirstName;
                currentUser.LastName = userUpdateModel.LastName;
                currentUser.ContactDetails = userUpdateModel.ContactDetails;
                currentUser.Organization = userUpdateModel.Organization;

                // Update the user in the database
                var result = await _userManager.UpdateAsync(currentUser);

                if (result.Succeeded)
                {
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    var errors = result.Errors.Select(error => error.Description);
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // You can also customize the error response message
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userLoginModel.Email);

            if (user == null)
            {   
                return Unauthorized("Invalid credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginModel.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(user);
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }

        [HttpGet("getUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Return the user data
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // You can also customize the error response message
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePassword updatePasswordDto)
        {
            // Get the current user
            var user = await _userManager.FindByNameAsync(updatePasswordDto.Email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the old password is valid
            var passwordCheck = await _userManager.CheckPasswordAsync(user, updatePasswordDto.OldPassword);

            if (!passwordCheck)
            {
                return BadRequest("Invalid old password.");
            }

            // Change the user's password
            var result = await _userManager.ChangePasswordAsync(user, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Failed to update password.");
            }
        }


    }
}
