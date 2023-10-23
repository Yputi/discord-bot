using app.Services;
using app.Settings;
using app;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRefitServices();
builder.Services.Configure<DiscordSettings>(options => builder.Configuration.GetSection(nameof(DiscordSettings)).Bind(options));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DiscordService>();
builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<RunnerService>();
builder.Services.AddSingleton<GuildJoinService>();
builder.Services.AddHostedService(provider => provider.GetService<RunnerService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.GetService<DiscordService>().Start();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();