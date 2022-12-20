using CcsApi.ApiEndpoints;
using CcsApi.Authentication;
using CcsApi.Authentication.Interfaces;
using CcsApi.Authentication.Models;
using CcsApi.Models;
using CcsApi.Repositories;
using CcsApi.Repositories.Interfaces;
using CcsApi.Services;
using CcsApi.Services.Interfaces;
using CcsApi.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();
builder.Services.AddScoped<IValidator<Order>, OrderValidator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
// require authorization at all calls by default
builder.Services.AddAuthorization(cfg =>
{
    cfg.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// register authentication endpoint
app.MapPost("/ccs/login", [AllowAnonymous] async ([FromBody] User user, [FromServices] IUserService userService, [FromServices] ITokenService tokenService) =>
{
    if (userService.IsValidLogin(user))
    {
        var token = tokenService.GenerateToken(user, builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:Audience"]);      
        return Results.Ok(token);
    }
    else
        return Results.Unauthorized();
});

// register API endpoints
ProductApi.RegisterProductApi(app);
CustomerApi.RegisterCustomerApi(app);
OrderApi.RegisterOrderApi(app);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

