﻿using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class Seed
{
    public static async Task SeedUsers(DataContext context)
    {

        if (await context.Users.AnyAsync()) return;  //if there's existing user, won't add it again 

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();

            user.UserName = user.UserName.ToLower();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pas$$w0rd"));

            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);


        }
        await context.SaveChangesAsync();
    }
}
