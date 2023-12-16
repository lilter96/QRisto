using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using QRisto.Application.Configuration;
using QRisto.Application.Mappings;
using QRisto.Application.Services.Provider;
using QRisto.Application.Services.Reservation;
using QRisto.Application.Services.Service;
using QRisto.Application.Services.Table;
using QRisto.Application.Services.Token;
using QRisto.Application.Services.User;
using QRisto.Persistence;
using QRisto.Persistence.Entity.Auth;
using QRisto.Persistence.Repositories.Implementations;
using QRisto.Presentation.ClientApp;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var builder = WebApplication.CreateBuilder(args);

#region Database

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
        options.UseSqlServer(
            connectionString, x =>
                x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName).UseDateOnlyTimeOnly()));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

#endregion

#region Identity

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(
    options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 0;
    });

var jwtOptions = builder.Configuration
    .GetSection("JwtOptions")
    .Get<JwtOptions>();

builder.Services.AddSingleton(jwtOptions!);
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(
        options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = jwtOptions!.Issuer,

                ValidateAudience = false,
                ValidAudience = jwtOptions.Audience,

                ValidateLifetime = false,

                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
            };
        });

#endregion

#region Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc(
            "v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
        c.AddSecurityDefinition(
            "Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
        c.AddSecurityRequirement(
            new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
    });

#endregion

#region Services

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddTransient<IProviderService, ProviderService>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<ITableService, TableService>();
builder.Services.AddScoped<UnitOfWork>();

#endregion

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "clientapp/build"; });

builder.Services.AddCors(
    options =>
    {
        options.AddDefaultPolicy(
            policy => { policy.WithOrigins().AllowAnyOrigin(); });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.Use(
    async (context, next) =>
    {
        const string authKey = "Authorization";

        if (!context.Request.Headers.ContainsKey(authKey))
        {
            var cookieToken = context.Request.Cookies[AuthOptions.CookieName];

            if (cookieToken != null)
            {
                context.Request.Headers.Add(authKey, $"Bearer {cookieToken}");
            }
            else
            {
                if (context.Request.Query.TryGetValue(authKey, out var queryTokens) && queryTokens is [{ } queryToken])
                {
                    context.Request.Headers.Add(authKey, $"Bearer {queryToken}");
                }
            }
        }

        await next();
    });
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    const string spaPath = "/spaApp";
    app.MapControllerRoute(
        "default",
        "api/{controller}/{action=Index}/{id?}");
    app.MapWhen(
        context =>
            context.Request.Path.StartsWithSegments(spaPath) ||
            context.Request.Path == "/",
        client => { client.UseSpa(spa => { spa.UseProxyToSpaDevelopmentServer("https://localhost:4444"); }); });
}
else
{
#pragma warning disable ASP0014
    app.UseEndpoints(
        endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller}/{action=Index}/{id?}");
        });
#pragma warning restore ASP0014

    var rootPath = string.Empty;
    app.Map(
        new PathString(rootPath), client =>
        {
            client.UseSpaStaticFiles();
            client.UseSpa(
                spa =>
                {
                    spa.Options.SourcePath = "clientapp";
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        OnPrepareResponse = ctx =>
                        {
                            var headers = ctx.Context.Response.GetTypedHeaders();
                            headers.CacheControl = new CacheControlHeaderValue
                            {
                                NoCache = true,
                                NoStore = true,
                                MustRevalidate = true
                            };
                        }
                    };
                });
        });
}

app.Run();