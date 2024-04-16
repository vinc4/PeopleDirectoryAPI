using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeopleDirectory.Application.Bounderies;
using PeopleDirectory.Intergration.Entities;
using PeopleDirectory.Persistence.Database;
using PeopleDirectory.Application;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using PeopleDirectoryAPI.MappingProfiles;
using PeopleDirectory.Intergration.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DbContextSeeder>();


//add services
builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<INotificationService, EmailNotificationService>();


builder.Services.AddIdentity<PeopleDirectory.Intergration.Entities.User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("PeopleDirectory");
});

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowAnyOrigin();
    });
});

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ControllerReqToServiceReqProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);



var app = builder.Build();
app.UseCors("AllowAllOrigins");


using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbContextSeeder>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await seeder.SeedRolesAsync(roleManager);
    await seeder.SeedAsync();
    await seeder.SeedClientsAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
