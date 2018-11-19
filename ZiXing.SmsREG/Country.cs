using System.ComponentModel;

namespace ZiXing.SmsREG
{
    /// <summary>
    /// The supported countries
    /// </summary>
    public enum Country
    {
        /// <summary>
        /// Any free mobile phone number
        /// </summary>
        [Description("all")]
        All = 0,

        /// <summary>
        /// Number of Russian operators
        /// </summary>
        [Description("ru")]
        Russia = 1,

        /// <summary>
        /// Number of Ukrainian operators
        /// </summary>
        [Description("ua")]
        Ukriane = 2,

        /// <summary>
        /// Number of Kazakhstani operators
        /// </summary>
        [Description("kz")]
        Kazakhstan = 3,

        /// <summary>
        /// The number of Chinese operators
        /// </summary>
        [Description("cn")]
        China = 4
    }
}
