using AdunTech.NPoco2Mysql;
using AdunTech.NPoco2SqlServer;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISqlServerDb, SqlServerDb>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServerTestDB");
    return new SqlServerDb(connectionString);
});

builder.Services.AddScoped<IMySqlDb, MySqlDb>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySqlTestDB");
    return new MySqlDb(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
  
app.MapControllers();

app.Run();
