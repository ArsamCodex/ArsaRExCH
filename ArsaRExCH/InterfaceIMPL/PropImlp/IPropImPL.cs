using ArsaRExCH.Data;
using ArsaRExCH.DTOs;
using ArsaRExCH.Interface.PropInterface;
using ArsaRExCH.Model.Prop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using NBitcoin.Secp256k1;
using Newtonsoft.Json;

namespace ArsaRExCH.InterfaceIMPL.PropImlp
{
    public class IPropImPL(IDbContextFactory<ApplicationDbContext> context, ILogger<IPropImPL> logger
        , HttpClient httpClient
        ) : IProp
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContext = context;
        private readonly ILogger<IPropImPL> _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        private readonly HttpClient _httpClient = httpClient;

        public async Task DeleteObjectById<T>(object id) where T : class
        {
            if (id == null)
            {
                _logger.LogError("The ID parameter is null.");
                throw new ArgumentNullException(nameof(id), "The ID parameter cannot be null.");
            }

            try
            {
                using var scope = _dbContext.CreateDbContext();
                var entity = await scope.Set<T>().FindAsync(id);

                if (entity == null)
                {
                    _logger.LogWarning("Entity of type {EntityType} with ID {Id} not found.", typeof(T).Name, id);
                    throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
                }

                scope.Set<T>().Remove(entity);
                await scope.SaveChangesAsync();
                _logger.LogInformation("Successfully deleted entity of type {EntityType} with ID {Id} from the database.", typeof(T).Name, id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database update error occurred while deleting entity.");
                throw new InvalidOperationException("An error occurred while deleting from the database.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting entity.");
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }
        public async Task<T> SaveObject<T>(T entity) where T : class
        {
            if (entity == null)
            {
                _logger.LogError("The entity object is null.");
                throw new ArgumentNullException(nameof(entity), "The entity object cannot be null.");
            }

            try
            {
                // Create a new DbContext scope
                using var scope = _dbContext.CreateDbContext();

                // Add the entity to the DbSet
                await scope.Set<T>().AddAsync(entity);

                // Save changes to the database
                await scope.SaveChangesAsync();

                // Log successful save
                _logger.LogInformation("Successfully saved {EntityType} to the database.", typeof(T).Name);

                return entity; // Return the saved entity
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database update error occurred while saving {EntityType}.", typeof(T).Name);
                throw new InvalidOperationException($"An error occurred while saving {typeof(T).Name} to the database.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while saving {EntityType}.", typeof(T).Name);
                throw new Exception($"An unexpected error occurred while saving {typeof(T).Name}.", ex);
            }
        }
        public async Task EditObject<T>(object id, T updatedEntity) where T : class
        {
            // Check for null parameters
            if (id == null)
            {
                _logger.LogError("The ID parameter is null.");
                throw new ArgumentNullException(nameof(id), "The ID parameter cannot be null.");
            }

            if (updatedEntity == null)
            {
                _logger.LogError("The updated entity object is null.");
                throw new ArgumentNullException(nameof(updatedEntity), "The updated entity object cannot be null.");
            }

            try
            {
                // Create a DbContext scope for editing
                using var scope = _dbContext.CreateDbContext();

                // Fetch the existing entity of type T by ID
                var existingEntity = await scope.Set<T>().FindAsync(id);

                // If the entity is not found, log and throw a not found exception
                if (existingEntity == null)
                {
                    _logger.LogWarning("Entity of type {EntityType} with ID {Id} not found for editing.", typeof(T).Name, id);
                    throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
                }

                // Update the properties of the existing entity with the new values
                scope.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);

                // Save the changes to the database
                await scope.SaveChangesAsync();

                // Log successful operation
                _logger.LogInformation("Successfully edited entity of type {EntityType} with ID {Id}.", typeof(T).Name, id);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exception and log it
                _logger.LogError(ex, "A database update error occurred while editing the entity.");
                throw new InvalidOperationException("An error occurred while editing the entity in the database.", ex);
            }
            catch (Exception ex)
            {
                // Catch all other exceptions
                _logger.LogError(ex, "An unexpected error occurred while editing the entity.");
                throw new Exception("An unexpected error occurred while editing the entity.", ex);
            }
        }
        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            try
            {
                // Create a DbContext scope
                using var scope = _dbContext.CreateDbContext();

                // Fetch all entities of type T from the database
                var entities = await scope.Set<T>().ToListAsync();

                // Check if no entities were found and log a warning
                if (entities.Count == 0)
                {
                    _logger.LogWarning("No {EntityType} entities found in the database.", typeof(T).Name);
                }

                // Return the entities as IEnumerable<T>
                return entities;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database update error occurred while retrieving {EntityType} entities.", typeof(T).Name);
                throw new InvalidOperationException($"An error occurred while retrieving {typeof(T).Name} entities from the database.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving {EntityType} entities.", typeof(T).Name);
                throw new Exception($"An unexpected error occurred while retrieving {typeof(T).Name} entities.", ex);
            }
        }

        public async Task<BinancePrepetualPriceDTO> GetBTCPerpetualPriceAsync()
        {
            try
            {
                // Sending HTTP request to Binance API to get the price of BTCUSDT perpetual
                var response = await _httpClient.GetStringAsync("https://fapi.binance.com/fapi/v1/ticker/price?symbol=BTCUSDT");

                // Deserialize the response into the DTO object
                var priceData = JsonConvert.DeserializeObject<BinancePrepetualPriceDTO>(response);

                // Return the price data object
                return priceData;
            }
            catch (Exception ex)
            {
                // Handle the exception (logging, etc.)
                Console.WriteLine($"Error fetching price: {ex.Message}");
                return null;
            }
        }



    }
}
