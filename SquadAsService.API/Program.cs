using Scalar.AspNetCore;
using Fiker.Api;
using Fiker.Application.Extensions;
using Fiker.Infrastructure.Extensions;
using Fiker.Presentation.MiddleWare;
using Fiker.Presistance.Extensions;
using Fiker.Presistance.Seeding;
using Hangfire;

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
app.UseHangfireDashboard("/hangFireDashboard");
SeedingData.Invoke(app.Services.CreateScope().ServiceProvider).Wait();

app.Run();