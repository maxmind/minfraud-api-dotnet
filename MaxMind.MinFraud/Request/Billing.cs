namespace MaxMind.MinFraud.Request
{
    public class Billing : Location
    {
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