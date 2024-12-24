using Scalar.AspNetCore;
using SquadAsService.Api;
using SquadAsService.Application.Extensions;
using SquadAsService.Infrastructure.Extensions;
using SquadAsService.Presentation.MiddleWare;
using SquadAsService.Presistance.Extensions;
using SquadAsService.Presistance.Seeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresistance(builder.Configuration);

builder.Services.DependencyInjectionService(builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
    {
        o.Theme = ScalarTheme.Mars;
        o.Title = "SquadAsService API";
        o.WithDownloadButton(true);
        o.DefaultHttpClient = new(ScalarTarget.JavaScript, ScalarClient.Fetch);
        o.Authentication = new()
        {
            PreferredSecurityScheme = "Bearer",
        };
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseCors(cores => cores.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
}
else
{
    app.UseCors(cors => cors.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.MapControllers();

SeedingData.Invoke(app.Services.CreateScope().ServiceProvider).Wait();

app.Run();