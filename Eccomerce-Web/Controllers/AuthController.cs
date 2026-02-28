using Eccomerce_Web.Data;
using Eccomerce_Web.Models;
using Eccomerce_Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eccomerce_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {




        private readonly DataContext _db ;
        private readonly IJWTService _IJWTService;

        public AuthController(DataContext db, IJWTService JW)
        {
            _db = db;
            _IJWTService = JW;

        }



        [HttpGet("Get-User")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _db.Users.ToListAsync();

            // if (user == null)
            // {
            //     return BadRequest("Invalid user."); why? just return blank array 
            // }

            return Ok(user);
        }


        [HttpGet("Get-User-byid/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _db.UserProfiles.FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {


                ApiResponse<bool> ResNotFounds = new()
                {
                    Data = false,
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found"

                };





                return NotFound(ResNotFounds);
            }




            ApiResponse<UserProfile> ResNotFound = new()
            {
                Data = user,
                Status = StatusCodes.Status404NotFound,
                Message = "User not found"
            };


            return Ok("success");
        }


        [HttpDelete("Delete-User-byid/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {

                ApiResponse<bool> ResNotFound = new()
                {
                    Data = false,
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found"

                };


                return NotFound(ResNotFound);
            }

            _db.Remove(user);
            await _db.SaveChangesAsync();
            return Ok("success");
        }
    }
}
