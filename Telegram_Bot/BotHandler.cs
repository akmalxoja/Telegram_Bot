using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using TelegramBot;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using OfficeOpenXml.Drawing;

namespace Telegram_Bot
{
    public class BotHandler
    {
        public string botToken { get; set; }
        public int isCodeTrue = 1;
        public int InfoStatus = 0;
        public int InFoUpdate = 0;
        public int DeleteStatus = 0;
        public string phonename = "";
        public List<Phones> BANKAI = new List<Phones>();
        public bool isBookNamer = false;
        public bool IsHulkTrash = true;
        public BotHandler(string token)
        {
            botToken = token;
        }

        public async Task BotHandle()
        {
            var botClient = new TelegramBotClient(botToken);

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;

            long chatId = message.Chat.Id;

            CRUD.Create(new BotUser()
            {
                chatID = chatId,
                status = 0,
                phoneNumber = ""
            });

            if (isCodeTrue == 1)
            {
                if (message.Text != "get_code")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Code xato",
                        cancellationToken: cancellationToken);
                    return;
                }
                isCodeTrue = 2;
                await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Qabul qilindi!",
                        cancellationToken: cancellationToken);
            }
            if (message.Contact != null)
            {
                CRUD.Update(chatId, message.Contact.PhoneNumber.ToString());
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Iltimos sizga yuborilgan code ni kiriting!",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
                isCodeTrue = 1;
                return;
            }



            if (message.Text == "/start")
            {
                 
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                        {
                    KeyboardButton.WithRequestContact("Contact")
                })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: "Assalomu aleykum, Online telefon marketga xush kelibsiz\n" +
                     "Botdan to'liq foydalanish uchun telefon raqamingizni jo'nating",
                     replyMarkup: replyKeyboardMarkup,
                     cancellationToken: cancellationToken);
                    CRUD.ChangeStatusCode(chatId, 0);
                    return;
                }
                await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: "Assalomu aleykum, Botimizga xush kelibsiz bu bot orqali siz nimadir qila olishingiz mumkin",
                     cancellationToken: cancellationToken);
                CRUD.ChangeStatusCode(chatId, 0);
            }


            if (CRUD.IsPhoneNumberNull(chatId) == false)
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                    {
                    KeyboardButton.WithRequestContact("Contact")
                })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(
                     chatId: chatId,
                     text: "Botdan to'liq foydalanish uchun telefon raqamingizni jo'nating!",
                     replyMarkup: replyKeyboardMarkup,
                     cancellationToken: cancellationToken);
                CRUD.ChangeStatusCode(chatId, 0);
                return;
            }

            // change qilinmasin

            if (Admin.isAdmin(chatId) == true)
            {

                if (message.Text == "Model")
                {
                    CRUD.ChangeStatusCode(chatId, 1);
                }
                else if (message.Text == "Book")
                {
                    CRUD.ChangeStatusCode(chatId, 2);
                }
                else if (message.Text == "OrderStatus")
                {
                    CRUD.ChangeStatusCode(chatId, 3);
                }
                else if (message.Text == "PayType")
                {
                    CRUD.ChangeStatusCode(chatId, 4);
                }
                else if (message.Text == "Change Status")

                    CRUD.ChangeStatusCode(chatId, 5);
                else if (message.Text == "GetExelFormat")
                {
                    CRUD.ChangeStatusCode(chatId, 6);
                }
                else if (message.Text == "GetCustomerList")
                {
                    CRUD.ChangeStatusCode(chatId, 7);
                }
                else if (message.Text == "Add Admin")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Enter admin chatId",
                        cancellationToken: cancellationToken);
                    return;
                }
                else if (message.Text == "Back")
                {
                    if (CRUD.GetStatusCode(chatId) == 1)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 2)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 3)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 4)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 5)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 6)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                    else if (CRUD.GetStatusCode(chatId) == 7)
                    {
                        CRUD.ChangeStatusCode(chatId, 0);
                    }
                }
                long messageAsLong;
                if (long.TryParse(message.Text, out messageAsLong))
                {
                    Admin.Create(new Admin()
                    {
                        chatId = messageAsLong
                    });
                    Admin.Create(new Admin()
                    {
                        chatId = messageAsLong
                    });
                    await botClient.SendTextMessageAsync(chatId: chatId, text: "Succesfully added", cancellationToken: cancellationToken);
                }
                #region CREATE
                else if (message.Text == "CREATE")
                {
                    if (CRUD.GetStatusCode(chatId) == 1)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos modelini kiriting",
                            cancellationToken: cancellationToken
                            );
                        InfoStatus = 1;
                    }
                    else if (CRUD.GetStatusCode(chatId) == 2)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos yangi telefon nomini,modelini,narxini, yuboring yuboring",
                            cancellationToken: cancellationToken
                            );
                        InfoStatus = 2;
                    }
                    else if (CRUD.GetStatusCode(chatId) == 3)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos yangi order statusini yuboring",
                            cancellationToken: cancellationToken
                            );
                        InfoStatus = 3;
                    }
                    else if (CRUD.GetStatusCode(chatId) == 4)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos yangi payment turini yuboring",
                            cancellationToken: cancellationToken
                            );
                        InfoStatus = 4;
                    }
                    return;
                }
                #endregion

                else if (message.Text == "READ")
                {
                    if (CRUD.GetStatusCode(chatId) == 1)
                    {
                       
                    }
                    else if (CRUD.GetStatusCode(chatId) == 2)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: Phones.Read(),
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (CRUD.GetStatusCode(chatId) == 3)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: OrderStatus.Read(),
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (CRUD.GetStatusCode(chatId) == 4)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: PayType.Read(),
                            cancellationToken: cancellationToken
                            );
                    }


                }

                else if (message.Text == "UPDATE")
                {
                    if (CRUD.GetStatusCode(chatId) == 1)
                    {
                        InFoUpdate = 1;
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Update qilish uchun eski name, yangi name kiriting ",
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (CRUD.GetStatusCode(chatId) == 2)
                    {
                        InFoUpdate = 2;

                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Update last name, new name, new price",
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (CRUD.GetStatusCode(chatId) == 3)
                    {
                        InFoUpdate = 3;

                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Update qilish uchun name, new status ni kiriting",
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (CRUD.GetStatusCode(chatId) == 4)
                    {
                        InFoUpdate = 4;
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Update qilish uchun last name,new name ni kiriting",
                            cancellationToken: cancellationToken
                            );
                    }
                }

                else if (message.Text == "DELETE")
                {
                    if (CRUD.GetStatusCode(chatId) == 1)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos delete bo'luvchi modelni turini kiriting",
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (CRUD.GetStatusCode(chatId) == 2)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos delete bo'luvchi Brendni nomini kiriting",
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (CRUD.GetStatusCode(chatId) == 3)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos delete bo'luvchi order statusini kiriting",
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (CRUD.GetStatusCode(chatId) == 4)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Iltimos delete bo'luvchi Payment turini kiriting",
                            cancellationToken: cancellationToken
                            );
                    }
                    else if (message.Text == "UPDATE")
                    {
                        if (CRUD.GetStatusCode(chatId) == 1)
                        {
                            InFoUpdate = 1;
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Update kilish uchun name kiriting ",
                                cancellationToken: cancellationToken
                                );
                        }
                        else if (CRUD.GetStatusCode(chatId) == 2)
                        {
                            InFoUpdate = 2;

                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "update last name, new name, new price",
                                cancellationToken: cancellationToken
                                );
                        }
                        else if (CRUD.GetStatusCode(chatId) == 3)
                        {
                            InFoUpdate = 3;

                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "name, new status",
                                cancellationToken: cancellationToken
                                );
                        }
                        else if (CRUD.GetStatusCode(chatId) == 4)
                        {
                            InFoUpdate = 4;
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "last name,new name ",
                                cancellationToken: cancellationToken
                                );
                        }
                    }
                    if (message.Text != null)
                    {
                        switch (DeleteStatus)
                        {
                           
                            case 2:
                                DeleteStatus = 0;
                                if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE" || message.Text == "Back")
                                {
                                    return;
                                }
                                Phones.Delete(message.Text);
                                await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "Muvaffaqiyatli o'chirildi",
                                    cancellationToken: cancellationToken);
                                break;
                            case 3:
                                DeleteStatus = 0;
                                if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE" || message.Text == "Back")
                                {
                                    return;
                                }
                                OrderStatus.Delete(message.Text);
                                await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "Muvaffaqiyatli o'chirildi",
                                    cancellationToken: cancellationToken);
                                break;
                            case 4:
                                DeleteStatus = 0;
                                if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE" || message.Text == "Back")
                                {
                                    return;
                                }
                                PayType.Delete(message.Text);
                                await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "Muvaffaqiyatli o'chirildi",
                                    cancellationToken: cancellationToken);
                                break;
                            default:
                                DeleteStatus = 0;
                                break;
                        }
                    }
                    switch (InfoStatus)
                    {
                        
                        case 2:
                            InfoStatus = 0;
                            if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE")
                            {
                                return;
                            }
                            string[] phones = message.Text.Split(',', ' ');
                            Phones.Create(new Phones()
                            {
                                Brend = phones[0],
                                Model = phones[1],
                                price = int.Parse(phones[2]),
                               
                            });
                            await botClient.SendTextMessageAsync(
                              chatId: chatId,
                              text: "Muvaffaqiyatli yaratildi",
                              cancellationToken: cancellationToken);
                            break;
                        case 3:
                            InfoStatus = 0;
                            if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE")
                            {
                                return;
                            }
                            OrderStatus.Create(new OrderStatus()
                            {
                                status = message.Text
                            });
                            await botClient.SendTextMessageAsync(
                             chatId: chatId,
                             text: "Muvaffaqiyatli yaratildi",
                             cancellationToken: cancellationToken);
                            break;
                        case 4:
                            InfoStatus = 0;
                            if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE")
                            {
                                return;
                            }
                            PayType.Create(new PayType()
                            {
                                cash = message.Text,
                            });
                            await botClient.SendTextMessageAsync(
                             chatId: chatId,
                             text: "Muvaffaqiyatli yaratildi",
                             cancellationToken: cancellationToken);
                            break;
                        default:
                            InfoStatus = 0;
                            break;
                    }
                    switch (InFoUpdate)
                    {
                            
                        case 2:
                            InFoUpdate = 0;
                            if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE")
                            {
                                return;
                            }
                            string[] book = message.Text.Split(',');
                            Phones.Update(book[0], book[1], book[2], book[3], book[4]);

                            await botClient.SendTextMessageAsync(
                              chatId: chatId,
                              text: "Muvaffaqiyatli Update",
                              cancellationToken: cancellationToken);
                            break;
                        case 3:
                            InFoUpdate = 0;
                            if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE")
                            {
                                return;
                            }
                            string[] order = message.Text.Split(',');
                            OrderStatus.Update(order[0], order[1]);

                            await botClient.SendTextMessageAsync(
                             chatId: chatId,
                             text: "Muvaffaqiyatli Update",
                             cancellationToken: cancellationToken);
                            break;
                        case 4:
                            InFoUpdate = 0;
                            if (message.Text == "Phone" || message.Text == "READ" || message.Text == "CREATE" || message.Text == "UPDATE" || message.Text == "DELETE")
                            {
                                return;
                            }
                            string[] paytypes = message.Text.Split(",");
                            PayType.Update(paytypes[0], paytypes[1]);
                            await botClient.SendTextMessageAsync(
                             chatId: chatId,
                             text: "Muvaffaqiyatli Update",
                             cancellationToken: cancellationToken);
                            break;
                        default:
                            InFoUpdate = 0;
                            break;
                    }
                }
                switch (CRUD.GetStatusCode(chatId))
                {
                    case 1:
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                        {
                            new[]
                            {

                           
                            new KeyboardButton("Phone"),
                            new KeyboardButton("OrderStatus"),
                            new KeyboardButton("PayType"),
                            },
                            new[]
                            {
                                new KeyboardButton("Change Status"),
                                new KeyboardButton("GetExelFormat"),
                                new KeyboardButton("GetCustomerList"),
                                new KeyboardButton("Add Admin")
                            },
                        })
                        {
                            ResizeKeyboard = true
                        };

                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Bosh menu",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                        break;
                    case 2:
                        await CreateFunction(botClient, update, cancellationToken);
                        return;
                    case 3:
                        await CreateFunction(botClient, update, cancellationToken);
                        return;
                    case 4:
                        await CreateFunction(botClient, update, cancellationToken);
                        return;
                    case 5:
                        await CreateFunction(botClient, update, cancellationToken);
                        return;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        FileMake.MijozRoyxatPdf(CRUD.ReadForPDF(), @"C:\Users\VICTUS\Desktop\DotNet\Telegram_Bot\Data");
                        var stream2 = System.IO.File.OpenRead(@"C:\Users\VICTUS\Desktop\DotNet\Telegram_Bot\Data\MijozlarRoyhati.pdf");
                        Message message2 = await botClient.SendDocumentAsync(
                            chatId: chatId,
                            document: InputFile.FromStream(stream: stream2, fileName: "MijozlarRoyhati.pdf"),
                            caption: "Mijozlar Ro'yhati");
                        break;
                    default:
                        break;
                }


            }
            //admin panel tugadi

            else
            {
                if (CRUD.GetStatusCode(chatId) == 0)
                {
                    try
                    {

                        List<List<KeyboardButton>> categories = new List<List<KeyboardButton>>();
                        List<KeyboardButton> currentRow = new List<KeyboardButton>();

                       

                        if (currentRow.Count > 0)
                        {
                            categories.Add(new List<KeyboardButton>(currentRow));
                        }


                        ReplyKeyboardMarkup rep = new ReplyKeyboardMarkup(categories.Select(row => row.ToArray()).ToArray());
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Tanlang",
                            replyMarkup: rep,
                            cancellationToken: cancellationToken);
                        CRUD.ChangeStatusCode(chatId, 1);
                        return;
                    }
                    catch
                    {

                    }
                }
                if (message.Text != null && IsHulkTrash == true)
                {
                    int i = 0;
                    List<Phones>? s = DeserializeSerialize<Phones>.GetAll(@"C:\Users\VICTUS\Desktop\DotNet\Telegram_Bot\Books.json");
                    string st = "";
                    BANKAI.Clear();
                    foreach (Phones p in s)
                    {
                        if (message.Text.Contains(p.Brend))
                        {
                            if (p != null)
                            {
                                if (Phones.GetRead(p) != null)
                                {
                                    try
                                    {
                                        BANKAI.Add(p);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("EXCEPTION");
                                    }
                                    st += $"{i + 1}  + {Phones.GetRead(p)}";
                                }
                                i++;
                            }
                        }
                    }
                    if (st != "")
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: st.ToString(),
                            cancellationToken: cancellationToken);

                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Kerakli telefon id sini yuboring!",
                            cancellationToken: cancellationToken);
                        isBookNamer = true;
                        IsHulkTrash = false;
                        return;
                    }
                    await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Afsuski topilmadi!",
                            cancellationToken: cancellationToken);
                    return;
                }
                if (isBookNamer == true)
                {
                    if (BANKAI.Count >= Convert.ToInt16(message.Text) && 1 <= Convert.ToInt16(message.Text))
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: Phones.GetRead(BANKAI[Convert.ToInt16(message.Text) - 1]).ToString(),
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Index out of range");
                    }
                    IsHulkTrash = true;
                }

            }
        }
        //use panel

        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
        }

        public async Task CreateFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup4 = new(new[]
                      {
                            new[]
                            {

                            new KeyboardButton("CREATE"),
                             new KeyboardButton("READ"),
                            new KeyboardButton("UPDATE"),
                            new KeyboardButton("DELETE"),
                            },
                           new[]
                           {
                               new KeyboardButton("Back")
                           }
                        })
            {
                ResizeKeyboard = true
            };
            Message resp = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: update.Message.Text,
                replyMarkup: replyKeyboardMarkup4,
                cancellationToken: cancellationToken);

        }
    }

}