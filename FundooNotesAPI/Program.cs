using Bussiness_Layer.InterfaceBL;
using Bussiness_Layer.ServiceBL;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.ContextClass;
using Repository_Layer.User_Interface;
using Repository_Layer.User_Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FundooNotesDB")));

builder.Services.AddTransient<IUserRL, UserRL>();
builder.Services.AddTransient<IUserBL, UserBL>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
