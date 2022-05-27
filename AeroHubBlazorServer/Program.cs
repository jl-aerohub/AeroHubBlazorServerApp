using AeroHubBlazorServer.Controllers;
using AeroHubBlazorServer.Data;
using AeroHubBlazorServer.DbContexts;
using AeroHubBlazorServer.Interfaces;
using Azure.Storage.Blobs;
using FileHandlingServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add the Entity Framework Core DBContext
builder.Services.AddDbContext<MFINContext>(_ =>
{
    _.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "MFINDBConnectionString"));
});

builder.Services.AddAzureClients(_ =>
{
    _.AddBlobServiceClient(
        builder.Configuration["AzureStorageConnectionString"]);
});

builder.Services.AddScoped<IStorageInterface, AzureBlobStorageService>(
    provider => new AzureBlobStorageService(provider.GetRequiredService<BlobServiceClient>(), builder.Configuration["ContainerName"]));

builder.Services.AddScoped<MetaDatasController>();
builder.Services.AddScoped<AzureStorageHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Files")),
    RequestPath = "/Files"
});

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();
