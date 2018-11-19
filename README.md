# SMS-REG .NET Library

A simple wrapper for the SMS-REG API, Written for the .NET Library, an example usage of this Library can be found in "SmsREG Test" Project within this solution


## Example Code
```c#
static void Main(string[] args)
        {
            // Get a number from SMS-REG
            var Number = ZiXing.SmsREG.API.Activation.GetNumber("<API KEY>", ZiXing.SmsREG.Service.Telegram, ZiXing.SmsREG.Country.Russia);
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
```

## License 

**DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE** Version 2, December 2004 


 Copyright (C) 2018 Netkas <203818872@qq.com> 


*Everyone is permitted to copy and distribute verbatim or modified copies of this license document, and changing it is allowed as long  as the name is changed.* 

**DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION** 

  0. You just DO WHAT THE FUCK YOU WANT TO.