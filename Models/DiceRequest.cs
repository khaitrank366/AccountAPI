using System.ComponentModel.DataAnnotations;

namespace LoginApi.Models
{
    public class DiceRequest
    {
        [Required]
        public string BetType { get; set; } = string.Empty;
        [Required]
        public int BetAmount { get; set; } = 0;
    }
}
