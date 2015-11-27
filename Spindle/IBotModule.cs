namespace Spindle {
    /// <summary>
    ///     Interface that indicates a class should be treated as a bot module
    /// </summary>
    public interface IBotModule {
        /// <summary>
        ///     Unique identifier used for communication between bots and master
        /// </summary>
        string Identifier { get; }

        /// <summary>
        ///     Called when a command arrives for this module
        /// </summary>
        /// <param name="command">Received command</param>
        /// <returns>Text to reply to master</returns>
        string CommandReceived(string command);
    }
}
