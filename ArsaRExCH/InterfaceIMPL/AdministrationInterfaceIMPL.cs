﻿using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.InterfaceIMPL
{
    public class AdministrationInterfaceIMPL : AdministrationInterface
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BetInterfaceIMPL> _logger;
        private readonly PriceInterface _priceInterface;
        private readonly ApplicationDbContext _dbContext;
        public AdministrationInterfaceIMPL(IConfiguration configuration, ApplicationDbContext context, ILogger<BetInterfaceIMPL> logger,
                         SignInManager<ApplicationUser> signInManager, PriceInterface priceInterface, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _signInManager = signInManager;
            _priceInterface = priceInterface;
            _dbContext = dbContext;
        }
        public async Task AddBannCountries(BanedCountries banedCountries)
        {
            try
            {
                await _context.AddAsync(banedCountries);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can adjust this to your logging mechanism)
                Console.WriteLine($"Error adding banned country: {ex.Message}");
                throw new Exception("An unexpected error occurred while saving the bet.", ex);
            }
        }

        public async Task<List<ApplicationUser>> AllUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Some Problem");
            }
        }

        public async Task<List<BanedCountries>> GetAllBannedCountriesInDatabase()
        {
            try
            {
                return await _context.BanedCountris.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error to get List of Baanned countries", ex);
            }
        }

        public async Task<bool> RemoveBannedCuntries(int banbId)
        {
            try
            {
                var bannedCountry = await _context.BanedCountris.FindAsync(banbId);
                if (bannedCountry != null)
                {
                    _context.BanedCountris.Remove(bannedCountry);
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing country: {ex.Message}");
                return false;
            }
        }
    }
    }

