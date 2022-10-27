using Telegram.Bot;
using Telegram.Bot.Types;

using HttpClient = Asd.HttpLib.HttpClient;

namespace TelegramApiWorker;

internal class BotService : IHostedService {
  private readonly TelegramBotClient _client;
  private readonly HttpClient _httpClient = new("http://deeppavlov/");
  internal async Task Handle(ITelegramBotClient client, Update update, CancellationToken token) {
    if (update.Message is not Message message) {
      return;
    }
    var answer = _httpClient.Post<string>("ask");
    if (!answer) {
      await client.SendTextMessageAsync(message.Chat.Id, "Что то сломаласё.");
    }
    await client.SendTextMessageAsync(message.Chat.Id, answer);
  }
  internal Task HandleError(ITelegramBotClient client, Exception exception, CancellationToken token) {
    return Task.CompletedTask;
  }
  public Task StartAsync(CancellationToken cancellationToken) {
    _client.StartReceiving(updateHandler: Handle, pollingErrorHandler: HandleError);

    return Task.CompletedTask;
  }

  public async Task StopAsync(CancellationToken cancellationToken) {
    await _client.CloseAsync();
  }

  public BotService(IConfiguration configuration) {
    _client = new(configuration["TelegramToken"]);
  }
}