using ASP.NETCoreRestAPI.Data;
using ASP.NETCoreRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NETCoreRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await userRepository.GetUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var result = await userRepository.GetUser(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                var emp = await userRepository.GetUserByEmail(user.UserID);

                if (emp != null)
                {
                    ModelState.AddModelError("UserID", "User UserID already in use");
                    return BadRequest(ModelState);
                }

                var createdUser = await userRepository.AddUser(user);

                return CreatedAtAction(nameof(GetUser),
                    new { id = createdUser.ID }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new user record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            try
            {
                if (id != user.ID)
                    return BadRequest("User ID mismatch");

                var userToUpdate = await userRepository.GetUser(id);

                if (userToUpdate == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }

                return await userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating employee record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var userToDelete = await userRepository.GetUser(id);

                if (userToDelete == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }

                await userRepository.DeleteUser(id);

                return Ok($"User with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }
    }
}
