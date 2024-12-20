using ArsaRExCH.Components;
using ArsaRExCH.Components.Account;
using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.InterfaceIMPL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ArsaRExCH.StaticsHelper;
using MudBlazor.Services;
using ArsaRExCH.Interface.PropInterface;
using ArsaRExCH.InterfaceIMPL.PropImlp;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddHostedService<PropTradeOrderCheker>();
//builder.Services.AddHostedService<DaailyMessageChedcker>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<WalletInterface<double>, WalletInterfaceIMPL>();
builder.Services.AddTransient<BetInterface, BetInterfaceIMPL>();
builder.Services.AddTransient<PriceInterface, PrriceInterfaceIMPL>();
builder.Services.AddScoped<UserIpInterface, UserInterfaceIMPL>();
builder.Services.AddScoped<AdministrationInterface, AdministrationInterfaceIMPL>();
builder.Services.AddScoped<AirDropInterface, AirDropInterfaceIMP>();
builder.Services.AddScoped<PostNadReplyInterface, PostNadReplyInterfaceIMPL>();
builder.Services.AddScoped<IBitcoinPool, IBitcoinPoolIMPL>();
builder.Services.AddScoped<ILiveChat, ILiveChatIMPL>();
builder.Services.AddScoped<ITrade, ItradeIMPL>();
builder.Services.AddScoped<IProp, IPropImPL>();
builder.Services.AddScoped<IPair, IPairIMPL>();



/*Renove swagger form this project relocate to api */



builder.Services.AddHttpContextAccessor();
//builder.Services.AddHttpClient();

builder.Services.AddHttpClient("BinanceClient", client =>
{
    client.BaseAddress = new Uri("https://api.binance.com/");
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

builder.Services.AddSingleton<ArsaRExCH.Interface.IEmailSender<ApplicationUser>, ArsaRExCH.Interface.EmailSender2>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddSingleton<DbContextFactory>();
builder.Services.AddMudServices();
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7258/") // Change to your frontend's URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});





/*
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ArsaRECH Private API",
        Version = "v0.1",
        Description = "Private API - ArsarExCH ",
        Contact = new OpenApiContact
        {
            Name = " Support",
            Email = "Arminttwat@gmail.com",
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { } // Empty array, if you don't use scopes
        }
    });
});*/
builder.Logging.AddConsole();



var app = builder.Build();
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    //   await InitializeRolesAndAssignAdmin(services);
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Ensure roles exist
    var roles = new[] { "Admin", "User", "Moderator" }; // Define your roles here
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}*/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
/*
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ARsarExCH Public API");
}); */
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ArsaRExCH.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();
app.MapControllers();
app.UseRouting();
app.UseAuthentication();  // Ensure this is called before UseAuthorization
app.UseAuthorization();
app.UseAntiforgery();



app.Run();


/*
async Task InitializeRolesAndAssignAdmin(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Ensure "Admin" role exists
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Find a user to assign the Admin role to
    var user = await userManager.FindByEmailAsync("admin@example.com");
    if (user != null && !(await userManager.IsInRoleAsync(user, "Admin")))
    {
        await userManager.AddToRoleAsync(user, "Admin");
    }
}*/














/*
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   // options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JwtIssuer"],
                ValidAudience = builder.Configuration["JwtAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]))
            };
        }).AddIdentityCookies();
*/