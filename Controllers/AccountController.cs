using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Danh sách lưu trữ tài khoản trên server
        private static List<Account> accounts = new List<Account>();

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok(accounts);
        }

        // Đăng ký tài khoản
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] Account newAccount)
        {
            // Kiểm tra tài khoản đã tồn tại chưa
            if (accounts.Any(a => a.Username == newAccount.Username))
            {
                return BadRequest("Tài khoản đã tồn tại!");
            }

            // Tạo tài khoản mới với mật khẩu mặc định là "1"
            newAccount.Password = "1";
            accounts.Add(newAccount);

            return Ok("Đăng ký thành công!");
        }

        // Đăng nhập tài khoản
        [HttpPost("login")]
        public IActionResult Login([FromBody] Account loginRequest)
        {
            var account = accounts.FirstOrDefault(a => a.Username == loginRequest.Username);

            if (account == null)
            {
                return NotFound("Tài khoản không tồn tại!");
            }

            if (account.Password != loginRequest.Password)
            {
                return Unauthorized("Mật khẩu không chính xác!");
            }

            return Ok("Đăng nhập thành công!");
        }
    }    
}