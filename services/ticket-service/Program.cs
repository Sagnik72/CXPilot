using Microsoft.EntityFrameworkCore;
using ticket_service.Configurations;
using ticket_service.Data;
using ticket_service.Services;

var builder = WebApplication.CreateBuilder(args);

var useInMemoryDatabase = builder.Configuration.GetValue<bool>("Database:UseInMemory");
if (useInMemoryDatabase)
{
    builder.Services.AddDbContext<TicketDbContext>(options =>
        options.UseInMemoryDatabase("CXPilotTicketDb"));
}
else
{
    builder.Services.AddDbContext<TicketDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
builder.Services.Configure<AiServiceOptions>(
    builder.Configuration.GetSection(AiServiceOptions.SectionName));

var aiOptions = builder.Configuration
    .GetSection(AiServiceOptions.SectionName)
    .Get<AiServiceOptions>() ?? throw new InvalidOperationException("AiService configuration is missing.");

builder.Services.AddHttpClient<IAiAnalysisClient, AiAnalysisClient>(client =>
{
    client.BaseAddress = new Uri(aiOptions.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(aiOptions.TimeoutSeconds);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
