using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;

namespace ZiXing.SmsREG
{
    /// <summary>
    /// Utilities for the Library
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Retrieves the description from a enumeration value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumerationValue)
        where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }

        /// <summary>
        /// Sends a GET Request and returns the response
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GET(Uri uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.ToString());
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }


        /// <summary>
        /// Determines the country of the phone number
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static Country DetectCountry(string Number)
        {

            if (Number.Remove(3) == "380")
            {
                return Country.Ukriane;
            }

            if(Number.Remove(2) == "86")
            {
                return Country.China;
            }

            if (Number.Remove(1) == "7")
            {
                return Country.Russia;
            }

            return Country.Russia;
        }

    }
}
 