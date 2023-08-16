using Microsoft.EntityFrameworkCore;
using ToDo.Host.Extensions;
using ToDo.Host.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddCors();
builder.Services.AddDbContext<ToDoDbContext>(options =>
{
    options.UseInMemoryDatabase("ToDo");
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHttpClient("GitHub",
        client => client.BaseAddress = new Uri("https://github.com"));
builder.Services.AddHttpClient("GitHubApi",
        client => client.BaseAddress = new Uri("https://api.github.com"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c =>
{
    c.AllowAnyOrigin();
});
app.UseAuthorization();

app.MapApi();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");


app.Run();