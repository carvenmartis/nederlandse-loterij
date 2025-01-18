namespace NederlandseLoterij.API.Models
{
    /// <summary>
    /// Represents the response from a scratch card operation.
    /// </summary>
    public class ScratchResponse
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The prize won, if any.
        /// </summary>
        public string? Prize { get; set; }

        /// <summary>
        /// A message providing additional information about the operation.
        /// </summary>
        public required string Message { get; set; }
    }
}