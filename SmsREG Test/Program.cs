using System;
using System.Threading;

namespace SmsREG_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get a number from SMS-REG
            ZiXing.SmsREG.Objects.Number Number = ZiXing.SmsREG.API.Activation.GetNumber("<API KEY>", ZiXing.SmsREG.Service.Telegram, ZiXing.SmsREG.Country.Russia);

            while(true)
            {
                if (Number.Phone == null)
                {
                    Console.WriteLine("Number not ready, checking in 5 seconds");
                    Thread.Sleep(5000);
                    Number.Phone = ZiXing.SmsREG.API.Activation.GetPhoneNumber(Number);
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine($"ID: {Number.ID}");
            Console.WriteLine($"Number: {Number.Phone}");

            // Mark the number as ready
            ZiXing.SmsREG.API.Activation.SetReady(Number);

            // Wait for the code to be returned
            while(true)
            {
                try
                {
                    var Code = ZiXing.SmsREG.API.Activation.CheckCode(Number);
                    Console.WriteLine($"Code: {Code}");
                    break;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Press return to quit ...");
            Console.ReadLine();
        }
    }
}
