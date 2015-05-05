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
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Address = address;
            Address2 = address2;
            City = city;
            Region = region;
            Country = country;
            Postal = postal;
            PhoneNumber = phoneNumber;
            PhoneCountryCode = phoneCountryCode;
        }
    }
}