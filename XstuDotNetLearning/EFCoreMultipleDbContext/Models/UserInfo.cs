using System.ComponentModel.DataAnnotations;

namespace EFCoreMultipleDbContext.Models
{
    public class UserInfo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public DateTime? CreationTime { get; set; } = DateTime.Now;
    }
}
