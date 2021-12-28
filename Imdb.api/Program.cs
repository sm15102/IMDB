using Imdb.api;
using Imdb.api.Extensions;
using Imdb.api.Middleware;
using Imdb.Application;
using Imdb.Identity;
using Imdb.Persistence;
using Imdb.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwagger();
builder.Services.AddProvidsersServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.MigrateDbContext();
app.AddIdentityUsers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMDB API");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.UseCustomExceptionHandler();
app.UseCors("Open");

app.UseAuthorization();

app.MapControllers();

app.Run();
