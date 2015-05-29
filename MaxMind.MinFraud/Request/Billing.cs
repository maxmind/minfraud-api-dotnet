namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The billing information for the transaction being sent to the
    /// web service.
    /// </summary>
    public class Billing : Location
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName">The first name of the end user as provided in their billing information.</param>
        /// <param name="lastName">The last name of the end user as provided in their billing information.</param>
        /// <param name="company">The company of the end user as provided in their billing information.</param>
        /// <param name="address">The first line of the user’s billing address.</param>
        /// <param name="address2">The second line of the user’s billing address.</param>
        /// <param name="city">The city of the user’s billing address.</param>
        /// <param name="region">The <see href="http://en.wikipedia.org/wiki/ISO_3166-2">ISO 3166-2</see> 
        /// subdivision code for the user’s billing address.</param>
        /// <param name="country">The two character <see href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO
        /// 3166-1 alpha-2</see> country code of the user’s billing address.</param>
        /// <param name="postal">The postal code of the user’s billing address.</param>
        /// <param name="phoneNumber">The phone number without the country code for the user’s billing address.</param>
        /// <param name="phoneCountryCode">The country code for phone number associated with the user’s billing address.</param>
        public Billing(
            string firstName = null,
            string lastName = null,
            string company = null,
            string address = null,
            string address2 = null,
            string city = null,
            string region = null,
            string country = null,
            string postal = null,
            string phoneNumber = null,
            string phoneCountryCode = null
            ) : base(
                firstName: firstName,
                lastName: lastName,
                company: company,
                address: address,
                address2: address2,
                city: city,
                region: region,
                country: country,
                postal: postal,
                phoneNumber: phoneNumber,
                phoneCountryCode: phoneCountryCode
                )
        {
        }
    }
}