using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs
{
    public class UserDTO
    {
        [StringLength(256, MinimumLength = 1, ErrorMessage = "Maximum 256 characters")]
        public string Name { get; set; } = default!;
        public int UserId { get; set; }
    }
}
