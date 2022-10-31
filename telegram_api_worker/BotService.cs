using Telegram.Bot;
using Telegram.Bot.Types;

using HttpClient = Asd.HttpLib.HttpClient;

namespace TelegramApiWorker;

internal class BotService : IHostedService {
  private readonly TelegramBotClient _client;
  private readonly HttpClient _httpClient = new("http://deeppavlov/");
  private readonly ILogger<BotService> _logger;

  internal async Task Handle(ITelegramBotClient client, Update update, CancellationToken token) {
    if (update.Message is not Message message) {
      _logger.LogError("Message is not message.");
      return;
    }
    if (message.Text is not string text) {
      _logger.LogInformation("Received message without text.");
      await client.SendTextMessageAsync(message.Chat.Id, "Вопрос  долже быть задан в виде текстового сообщения.");
      return;
    }
    _logger.LogInformation("Received message: {}", text);
    var answer = _httpClient.Post<string>("ask", null, text);
    if (!answer) {
      await client.SendTextMessageAsync(message.Chat.Id, "Что то сломаласё.");
    }
    _logger.LogInformation("Answer text: {}", answer.Value);
    await client.SendTextMessageAsync(message.Chat.Id, answer);
  }
  internal async Task HandleError(ITelegramBotClient client, Exception exception, CancellationToken token) {
    _logger.LogError("{}", exception.ToString());
  }
  public async Task StartAsync(CancellationToken cancellationToken) {
    _client.StartReceiving(updateHandler: Handle, pollingErrorHandler: HandleError);
  }

  public async Task StopAsync(CancellationToken cancellationToken) {
    await _client.CloseAsync();
  }

  public BotService(IConfiguration configuration, ILogger<BotService> logger) {
    _client = new(configuration["TelegramToken"]);
    _logger = logger;
  }
}