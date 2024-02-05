using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram_Bot
{
    public class Phones
    {
        public string Brend { get; set; }
        public string Model { get; set; }
        public long price { get; set; }
  

        public static string path = @"C:\Users\VICTUS\Desktop\DotNet\Telegram_Bot\Phones.json";

        public static void Create(Phones bk)
        {
            List<Phones> phones = DeserializeSerialize<Phones>.GetAll(path);
            if (phones.Any(c => c.Brend == bk.Brend))
            {
                return;
            }
            phones.Add(bk);
            DeserializeSerialize<Phones>.Save(phones, path);
        }
        public static string Read()
        {
            StringBuilder builder = new StringBuilder();
            List<Phones> books = DeserializeSerialize<Phones>.GetAll(path);
            foreach (Phones c in books)
            {
                builder.Append(
                    $"Phone Brend: {c.Brend}\n" +
                    $"Phone model: {c.Model}\n" +
                    $"price: {c.price}");
            }
            return builder.ToString();
        }

        public static string GetRead(Phones c)
        {
            return (
                     $"Phone Brend: {c.Brend}\n" +
                    $"Phone model: {c.Model}\n" +
                    $"price: {c.price}");

        }

        public static void Update(string last_name, string new_price, string new_name, string new_author, string new_category_name)
        {
            try
            {
                List<Phones> phones = DeserializeSerialize<Phones>.GetAll(path);
                if (phones != null)
                {
                    int index = phones.FindIndex(name => name.Brend == last_name);
                    if (index != -1)
                    {
                        phones[index].Brend = new_name;
                        phones[index].price = Convert.ToUInt16(new_price);
                        DeserializeSerialize<Phones>.Save(phones, path);
                    }
                }
            }
            catch { }
        }

        public static void Delete(string del_name)
        {
            try
            {
                List<Phones> phones = DeserializeSerialize<Phones>.GetAll(path);
                var catToRemove = phones.Find(ct => ct.Model == del_name);

                if (catToRemove != null)
                {
                    phones.Remove(catToRemove);
                    DeserializeSerialize<Phones>.Save(phones, path);
                }
            }
            catch { }
        }
    }
}
