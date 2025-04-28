using Fansoft.PocArc.Api.Data;
using Fansoft.PocArc.Api.Data.Repository;
using Fansoft.PocArc.Api.Domain.Entities;
using Fansoft.PocArc.Api.Domain.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Authentication:Authority"]  ?? 
                            throw new InvalidOperationException();
        options.Audience = builder.Configuration["Authentication:Audience"]  ?? 
                           throw new InvalidOperationException();
        options.RequireHttpsMetadata = bool.Parse(builder.Configuration["Authentication:RequireHttpsMetadata"] ?? 
                                                  throw new InvalidOperationException());
    });

builder.Services.AddAuthorization();

// Repository
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend API", Version = "v1" });

    // Autenticação via Bearer token no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer.\r\n\r\nDigite 'Bearer' [espaço] e depois seu token.\r\n\r\nExemplo: \"Bearer eyJhbGciOiJI...\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Auto migration segura (sem tentar criar banco!)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        // Abre conexão manualmente só para validar que o banco CustomersDb já existe
        await dbContext.Database.OpenConnectionAsync();
        await dbContext.Database.MigrateAsync();
        await dbContext.Database.CloseConnectionAsync();

        app.Logger.LogInformation("✅ Database migration applied successfully.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "❌ An error occurred while applying database migrations.");
        // throw; // Se preferir parar o app em caso de erro crítico
    }
}

// Swagger (apenas se for Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoints
app.MapPost("/customers", async (Customer customer, ICustomerRepository repo) =>
{
    var created = await repo.AddAsync(customer);
    return Results.Created($"/customers/{created.Id}", created);
}).RequireAuthorization();

app.MapGet("/customers", async (ICustomerRepository repo, int page = 1, int pageSize = 10, string? search = null) =>
{
    if (page <= 0) page = 1;
    if (pageSize <= 0 || pageSize > 100) pageSize = 10;

    var (customers, totalCount) = await repo.GetAllPagedAsync(page, pageSize, search ?? string.Empty);

    var response = new
    {
        totalItems = totalCount,
        page,
        pageSize,
        totalPages = (int)Math.Ceiling((double)totalCount / pageSize),
        items = customers
    };

    return Results.Ok(response);
}).RequireAuthorization();

app.MapGet("/customers/{id}", async (Guid id, ICustomerRepository repo) =>
{
    var customer = await repo.GetByIdAsync(id);
    return customer != null ? Results.Ok(customer) : Results.NotFound();
}).RequireAuthorization();

app.MapPut("/customers/{id}", async (Guid id, Customer updated, ICustomerRepository repo) =>
{
    var customer = await repo.GetByIdAsync(id);
    if (customer == null) return Results.NotFound();
    
    updated.Id = id;
    await repo.UpdateAsync(updated);
    return Results.Ok(updated);
}).RequireAuthorization();

app.MapDelete("/customers/{id}", async (Guid id, ICustomerRepository repo) =>
{
    await repo.DeleteAsync(id);
    return Results.NoContent();
}).RequireAuthorization();

await app.RunAsync();