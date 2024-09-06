using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Models.Models;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int status { get; set; } = 1;
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
    public string JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime ExpiryDate { get; set; }
    public User User { get; set; }

}
