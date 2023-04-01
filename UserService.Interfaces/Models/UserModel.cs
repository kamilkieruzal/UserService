namespace UserService.Interfaces.Models
{
    public class UserModel
    {
        public string Name { get; set; } = default!;
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
