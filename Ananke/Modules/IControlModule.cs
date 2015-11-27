namespace Ananke.Modules {
    /// <summary>
    ///     Interface that indicates a class should be treated as a C&amp;C module
    /// </summary>
    public interface IControlModule {
        /// <summary>
        ///     Unique identifier used for communication between bots and master
        /// </summary>
        string Identifier { get; }

        /// <summary>
        ///     Called when the activate button is pressed
        /// </summary>
        /// <returns>Message to send to bots</returns>
        string Activate();

        /// <summary>
        ///     Called when a response is received from a bot
        /// </summary>
        /// <param name="response"></param>
        void ResponseReceived(string response);
    }
}
