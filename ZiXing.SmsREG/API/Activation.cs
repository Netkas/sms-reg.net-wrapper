using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using ZiXing.SmsREG.Objects;

namespace ZiXing.SmsREG.API
{
    /// <summary>
    /// The Activation API
    /// </summary>
    public class Activation
    {
        /// <summary>
        /// Gets a number from SMS-REG
        /// </summary>
        /// <param name="APIKey"></param>
        /// <param name="Service"></param>
        /// <param name="Country"></param>
        /// <returns></returns>
        public static Number GetNumber(string APIKey, Service Service, Country Country = Country.All)
        {
            Uri RequestURL = new Uri($"http://api.sms-reg.com/getNum.php?country={Utilities.GetDescription(Country)}&service={Utilities.GetDescription(Service)}&apikey={APIKey}");
            var Response = JObject.Parse(Utilities.GET(RequestURL));
            var NumberObj = new Number(APIKey);

            if((string)Response["response"] == "1")
            {
                NumberObj.ID = (string)Response["tzid"];
            }
            else
            {
                throw new Exception((string)Response["error_msg"]);
            }
            
            NumberObj.Service = Service;
            NumberObj.Phone = GetPhoneNumber(NumberObj);

            if (Country == Country.All)
            {
                NumberObj.Country = Utilities.DetectCountry(NumberObj.Phone);
            }
            else
            {
                NumberObj.Country = Country;
            }
            
            return NumberObj;
        }

        /// <summary>
        /// Requests the phone number from the server
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string GetPhoneNumber(Number Number)
        {
            Uri RequestURL = new Uri($"http://api.sms-reg.com/getOperations.php?apikey={Number.APIKey}");
            var GetResponse = Utilities.GET(RequestURL);
            Console.WriteLine(GetResponse);

            try
            {
                var Response = JObject.Parse(GetResponse);
                if ((string)Response["response"] == "0")
                {
                    throw new Exception((string)Response["error_msg"]);
                }
                else
                {
                    throw new Exception($"Unknown Response: {GetResponse}");
                }
            }
            catch
            {
                Debug.Print("Array Response");
            }
            
            foreach (JObject ArrayItem in JArray.Parse(GetResponse))
            {
                if((string)ArrayItem["tzid"] == Number.ID)
                {
                    return (string)ArrayItem["phone"];
                }
            }

            throw new Exception("Number not found");
        }

        /// <summary>
        /// Marks the number as ready, this will listen for incoming SMS messages
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static bool SetReady(Number Number)
        {
            Uri RequestURL = new Uri($"http://api.sms-reg.com/setReady.php?tzid={Number.ID}&apikey={Number.APIKey}");
            var Response = JObject.Parse(Utilities.GET(RequestURL));

            if ((string)Response["response"] == "1")
            {
                return true;
            }
            else
            {
                throw new Exception((string)Response["error_msg"]);
            }
        }

        /// <summary>
        /// Checks if a SMS message was recieved, if success the code will be returned as a string
        /// For any other reason, an exception will be thrown
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string CheckCode(Number Number)
        {
            Uri RequestURL = new Uri($"http://api.sms-reg.com/getState.php?tzid={Number.ID}&apikey={Number.APIKey}");
            var GetResponse = Utilities.GET(RequestURL);

            var Response = JObject.Parse(GetResponse);
            if ((string)Response["response"] == "TZ_NUM_ANSWER")
            {
                return (string)Response["msg"];
            }
            else
            {
                throw new Exception($"Not Ready: {(string)Response["response"]}");
            }
        }
    }
}
