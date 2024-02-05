using Telegram_Bot;

class Program
{
    static async Task Main(string[] args)
    {
        string token = "6927253004:AAFwitF0MaBS2RAABgqWxIFDaBmg7rTDB8M";
        Admin.Create(new Admin()
        {
            chatId = 5569322762
        });
        BotHandler handle = new BotHandler(token);

        try
        {
            await handle.BotHandle();
        }
        catch
        {
            await handle.BotHandle();
        }
    }
}