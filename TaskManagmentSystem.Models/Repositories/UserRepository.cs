using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Interfaces;
using TaskManagementSystem.Models.Models;

namespace TaskManagmentSystem.Business.Repositories;

public class UserRepository : BaseRepository<User> , IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<User> GetByIdAsync(string id)
    {
        return await _context.Users.FindAsync(id);
    }
}
