using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseController
{
    private readonly DataContext _context;

    public AccountController(DataContext context)
    {
        this._context = context;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.UserName)) return BadRequest("Username is taken!");
        
        using var hmac = new HMACSHA512(); //selejtezi az osztályt használat után IDisposable miatt
        
        var user = new AppUser
        {
            UserName = registerDto.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key

        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    // helper method
    private async Task<bool> UserExists(string userName)
    {
        return await _context.Users.AnyAsync(x => x.UserName == userName.ToLower());
    }
}