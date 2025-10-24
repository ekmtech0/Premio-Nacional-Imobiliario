using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public abstract class User
    {
        [Key]
        public virtual Guid Id { get; set; }
        public virtual string Nome { get; set; } = null!;
        
        public string Role { get; set; } = null!;
    }
}
