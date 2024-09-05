using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Models.Models;

namespace TaskManagementSystem.Models.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByIdAsync(string id);
}
