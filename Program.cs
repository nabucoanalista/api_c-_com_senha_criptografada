using mysqlAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository>(factory => {
    var connectionString = builder.Configuration.GetSection("DatabaseSettings").GetValue<string>("ConnectionString");
    return new UserRepository(connectionString);
});
builder.Services.AddScoped<ITestRepository>(factory => {
    var connectionString = builder.Configuration.GetSection("DatabaseSettings").GetValue<string>("ConnectionString");
    return new TestRepository(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
