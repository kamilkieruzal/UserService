using System.ComponentModel.DataAnnotations;

namespace UserService.Interfaces.Models
{
    public class UserDataModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(256, MinimumLength = 0, ErrorMessage = "Maximum 256 characters")]
        public string Name { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public UserModel User { get; set; } = default!;
    }
}
