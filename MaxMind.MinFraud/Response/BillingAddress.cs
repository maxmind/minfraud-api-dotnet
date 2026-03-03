namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Information about the billing address.
    /// </summary>
    public sealed record BillingAddress : Address
    {
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public sealed override string ToString() => base.ToString();
    }
}