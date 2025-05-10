using SignalrDemo.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

var policyName = "defaultCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, builder =>
    {
        builder.WithOrigins("http://localhost:4200") // the Angular app url
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}).AddAzureSignalR(options =>
{
    options.ConnectionString = builder.Configuration["Azure:SignalR:ConnectionString"];
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(policyName);
app.MapHub<SignalrDemoHub>("signalrdemohub");

app.Run();
