namespace Ananke {
    public interface IControlModule {
        string Identifier { get; }

        string Activate();
        void ResponseReceived(string response);
    }
}
