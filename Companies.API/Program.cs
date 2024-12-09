using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Companies.API.Extensions;
using Companies.Infrastructure.Data;
using Companies.Infrastructure.Repositories;
using Domain.Contracts;
using Services.Contracts;
using Companies.Services;
using Companies.Presemtation;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;


namespace Companies.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CompaniesContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CompaniesContext") ?? throw new InvalidOperationException("Connection string 'CompaniesContext' not found.")));

            builder.Services.AddControllers(configure =>
            {
                     configure.ReturnHttpNotAcceptable = true;

                //Global Filter
                //var policy = new AuthorizationPolicyBuilder()
                //                    .RequireAuthenticatedUser()
                //                    .RequireRole("Employee")
                //                    .Build();

                //configure.Filters.Add(new AuthorizeFilter(policy));


            })
                            // .AddXmlDataContractSerializerFormatters()
                            .AddNewtonsoftJson()
                            .AddApplicationPart(typeof(AssemblyReference).Assembly);
                            


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
           
            builder.Services.ConfigureServiceLayerServices();
            builder.Services.ConfigureRepositories();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var secretKey = builder.Configuration["secretkey"];
                    ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));

                    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
                    ArgumentNullException.ThrowIfNull(nameof(jwtSettings));

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

                    };
                });


            builder.Services.AddIdentityCore<ApplicationUser>(opt =>
                {
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 3;
                    opt.User.RequireUniqueEmail = true;
                })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CompaniesContext>()
                    .AddDefaultTokenProviders();


            //builder.Services.AddAuthentication(options =>
            //{
            //    options.AddPolicy("AdminPolicy", policy =>
            //       policy.RequireRole("Admin")
            //             .RequireClaim(ClaimTypes.NameIdentifier)
            //             .RequireClaim(ClaimTypes.Role));

            //    options.AddPolicy("EmployeePolicy", policy =>
            //        policy.RequireRole("Employee"));

            //});




            builder.Services.ConfigureCors();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                await app.SeedDataAsync();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
