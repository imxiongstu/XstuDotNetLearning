using System.ComponentModel.DataAnnotations;

namespace EFCoreMultipleDbContext.Models
{
    public class ApplicationInfo
    {
        [Key]
        public Guid? Id { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
    }
}
