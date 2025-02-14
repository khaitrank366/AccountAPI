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
        private static int totalAmount = 500000;
        private static Boolean isLogin = false;

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

            totalAmount = 500000;
            isLogin = true;
            return Ok(new { Result = "Đăng nhập thành công!", TotalAmount = totalAmount });
        }

        [HttpPost("dice")]
        public IActionResult dice([FromBody] DiceRequest request)
        {
            if (!isLogin)
            {
                return BadRequest("Vui lòng đăng nhập trước khi chơi.");
            }
            if (request.BetAmount <= 0)
            {
                return BadRequest("Số tiền cược phải lớn hơn 0.");
            }
            if (request.BetAmount > totalAmount)
            {
                return BadRequest("Số tiền cược phải nhỏ hơn số tiền hiện có.");
            }

            if (request.BetType != "even" && request.BetType != "odd")
            {
                return BadRequest("Loại cược phải là 'chẵn' hoặc 'lẻ'.");
            }

            Random random = new Random();
            int randomNumber = random.Next(1, 9);

            string result = (randomNumber % 2 == 0) ? "even" : "odd";

            if ((request.BetType == "even" && result == "even") || (request.BetType == "odd" && result == "odd"))
            {
                totalAmount += request.BetAmount;
            }
            else
            {
                totalAmount -= request.BetAmount;
            }
            return Ok(new { Result = randomNumber, TotalAmount = totalAmount });

        }
    }    
}