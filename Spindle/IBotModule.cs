namespace Spindle {
    public interface IBotModule {
        string Identifier { get; }

        string CommandReceived(string command);
    }
}
