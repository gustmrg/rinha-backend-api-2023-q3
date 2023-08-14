using RinhaDeBackEnd.API.Data;
using RinhaDeBackEnd.API.Helpers;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Init();
}*/

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();