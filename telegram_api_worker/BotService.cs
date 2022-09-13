using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramApiWorker;

internal class BotService : IHostedService {
  private readonly TelegramBotClient _client;
  internal async Task Handle(ITelegramBotClient client, Update update, CancellationToken token) {
    if (update.Message is not Message message) {
      return;
    }
    await client.SendTextMessageAsync(message.Chat.Id, "Hello world!!!");
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