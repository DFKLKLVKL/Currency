using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace currencies
{
    class Program
    {
        static async Task Main()
        {
            int cr,n;
            double rate = 0;
            Console.WriteLine("выбери валюту");
            Console.WriteLine("1 - доллары");
            Console.WriteLine("2 - евро");
            Console.WriteLine("3 - юани");
            Console.WriteLine("4 - беллоруские рубли");
            Console.WriteLine("5 - гривны");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("введи сумму в рублях");
            cr = Convert.ToInt32(Console.ReadLine());
            switch (n)
            {
                case 1:
                    {
                        rate = await  GetExchangeRate("USD");
                        double result = cr * rate;
                        Console.WriteLine(cr + " рублей это " + result + " долларов");
                        break;
                    }
                case 2:
                    {
                        rate = await  GetExchangeRate("EUR");
                        double result = cr * rate;
                        Console.WriteLine(cr + " рублей это " + result + " евро");
                        break;
                    }
                case 3:
                    {
                        rate = await  GetExchangeRate("CNY");
                        double result = cr * rate;
                        Console.WriteLine(cr + " рублей это " + result + " юаней");
                        break;
                    }
                case 4:
                    {
                        rate = await  GetExchangeRate("BYN");
                        double result = cr * rate;
                        Console.WriteLine(cr + " рублей это " + result + " б.рублей");
                        break;
                    }
                case 5:
                    {
                        rate = await  GetExchangeRate("UAH");
                        double result = cr * rate;
                        Console.WriteLine(cr + " рублей это " + result + " гривен");
                        break;
                    }
                default:
                    Console.WriteLine("такого варианта нет");
                    break;
            }


        }
        static async Task<double> GetExchangeRate(string toCurrency)
        {
            string url = $"https://api.exchangerate-api.com/v4/latest/RUB";
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    JObject data = JObject.Parse(json);
                    double rate = (double)data["rates"][toCurrency];
                    return rate;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении курса: {ex.Message}");
                    return 0;
                }
            }
        }
    }
}
