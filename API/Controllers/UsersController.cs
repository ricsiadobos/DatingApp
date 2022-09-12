using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class UsersController : BaseController
{
    private readonly DataContext _context;


    public UsersController(DataContext context)
    {
        _context = context;
    }

    //Mindig Aszinkron metódust használunk, hogy a szálakat ne foglaljuk le.
    [AllowAnonymous]    //engedély mellőzése
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users= await _context.Users.ToListAsync();
        return users;

    }
    
    //api/users/3
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user;
    }
}

