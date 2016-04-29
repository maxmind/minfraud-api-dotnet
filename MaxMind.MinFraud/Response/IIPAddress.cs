namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// An interface for IP address risk classes.
    /// </summary>
    public interface IIPAddress
    {
        /// <summary>
        /// The risk associated with the IP address. The value ranges from 0.01
        /// to 99. A higher score indicates a higher risk.
        /// </summary>
        double? Risk { get; }
    }
}