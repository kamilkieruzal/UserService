using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [StringLength(256, MinimumLength = 0, ErrorMessage = "Maximum 256 characters")]
        public string UserName { get; set; } = default!;
    }
}
