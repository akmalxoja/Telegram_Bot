﻿using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace TelegramBot
{
    public static class CRUD
    {
        public static string filePath = @"C:\Users\VICTUS\Desktop\DotNet\Telegram_Bot\Database.json";

        public static void Create(BotUser chat)
        {
            List<BotUser> chats = GetAllChats();
            if (chats.Any(c => c.chatID == chat.chatID))
            {
                return;
            }
            chats.Add(chat);
            SaveChats(chats);
        }


        public static string Read(long chatId)
        {
            List<BotUser> chats = GetAllChats();
            var chat = chats.Find(c => c.chatID == chatId);

            if (chat != null)
            {
                return $"{chat.chatID}:{chat.phoneNumber}";
            }
            else
            {
                return $"{chat.chatID}:{chat.phoneNumber}";
            }
        }

        public static string ReadForPDF()
        {
            StringBuilder sb = new StringBuilder();
            List<BotUser> chats = GetAllChats();
            foreach (BotUser chat in chats)
            {
                sb.Append($"{chat.chatID} => {chat.phoneNumber}\n-------------------\n");
            }
            return sb.ToString();
        }
        public static bool IsPhoneNumberNull(long chatId)
        {
            List<BotUser> chats = GetAllChats();
            var chat = chats.Find(c => c.chatID == chatId);

            if (chat != null && chat.phoneNumber != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Update(long chatId, string newPhoneNumber)
        {
            try
            {
                List<BotUser> users = GetAllChats();

                if (users != null)
                {
                    int index = users.FindIndex(u => u.chatID == chatId);


                    if (index != -1)
                    {
                        users[index].phoneNumber = newPhoneNumber;

                        SaveChats(users);
                    }
                }
            }
            catch
            {

            }

        }

        public static void Delete(long chatId)
        {
            List<BotUser> chats = GetAllChats();
            var chatToRemove = chats.Find(c => c.chatID == chatId);

            if (chatToRemove != null)
            {
                chats.Remove(chatToRemove);
                SaveChats(chats);
            }
        }

        private static List<BotUser> GetAllChats()
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<BotUser>>(json) ?? new List<BotUser>();
            }
            else
            {
                return new List<BotUser>();
            }
        }
        public static int GetStatusCode(long chatId)
        {

            List<BotUser> users = GetAllChats();
            BotUser? chatToRemove = users.Find(c => c.chatID == chatId);

            return chatToRemove.status;
        }

        public static void ChangeStatusCode(long chatId, int statusCode)
        {

            List<BotUser> users = GetAllChats();
            int index = users.FindIndex(u => u.chatID == chatId);
            if (index != -1)
            {
                users[index].status = statusCode;
            }
            SaveChats(users);
        }

        public static List<BotUser> GetAll()
        {
            return GetAllChats();
        }

        private static void SaveChats(List<BotUser> chats)
        {
            string json = JsonSerializer.Serialize(chats);
            System.IO.File.WriteAllText(filePath, json);
        }
    }
    public class BotUser
    {
        public long chatID { get; set; }

        public int status { get; set; }
        public string? phoneNumber { get; set; }
    }
}
// WOOOOOOOOOOOOOOOOOOOOOOOW Shelby Sila
