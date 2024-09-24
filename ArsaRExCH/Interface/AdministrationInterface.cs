﻿using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface AdministrationInterface
    {
        Task AddBannCountries(BanedCountries banedCountries);
        Task<List<BanedCountries>> GetAllBannedCountriesInDatabase();
        Task<BanedCountries> RemoveBannedCuntries();
        Task<BanedCountries> EditBannedCountries();
        
    }
}
