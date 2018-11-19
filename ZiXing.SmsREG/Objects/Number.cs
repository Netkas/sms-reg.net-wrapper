namespace ZiXing.SmsREG.Objects
{
    /// <summary>
    /// SMS-REG Number Object
    /// </summary>
    public class Number
    {
        /// <summary>
        /// The API Key that's used with this object
        /// </summary>
        public string APIKey { get; }

        /// <summary>
        /// The ID for this object
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The phone number for this object
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The country that this number if from
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// The service that this number is used for
        /// </summary>
        public Service Service { get; set;  }

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="APIKey"></param>
        public Number(string APIKey)
        {
            this.APIKey = APIKey;
        }
    }
}
