using System.ComponentModel.DataAnnotations;

namespace UserService.Interfaces.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [StringLength(256, MinimumLength = 0, ErrorMessage = "Maximum 256 characters")]
        public string UserName { get; set; } = default!;
        public IList<UserDataModel> Data { get; set; } = default!;
    }
}
