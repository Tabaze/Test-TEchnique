using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApp.Context;
using testApp.Models;

namespace testApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private TestContext _context;
        public UserController(TestContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public async Task<ResponceMessage> GetUsersList()
        {
            ResponceMessage responce = new ResponceMessage();
            try
            {
                responce.dataResult = (await _context.Set<User>().ToListAsync());
                responce.isSuccess = true;
                responce.Message = "Success";
            }
            catch (Exception ex)
            {
                responce.isSuccess = false;
                responce.Message = ex.Message;
            }
            return responce;
        }

        [HttpGet("signIn")]
        public async Task<ResponceMessage> SignIn(string username, string password)
        {
            ResponceMessage responce = new ResponceMessage();
            try
            {
                responce.dataResult = await _context.Set<User>().Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefaultAsync();
                
                if (responce.dataResult != null)
                {
                    responce.isSuccess = true;
                    responce.Message = "Success";
                }
                else
                {
                    responce.isSuccess = false;
                    responce.Message = "not found";
                }
            }
            catch (Exception ex)
            {
                responce.isSuccess = false;
                responce.Message = ex.Message;
            }
            return responce;
        }
    }
}
