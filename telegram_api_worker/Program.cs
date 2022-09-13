using TelegramApiWorker;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<BotService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
