namespace Ananke.Modules {
    /// <summary>
    ///     Interface that indicated a module has the ability to show a form.<br/>
    ///     Should also implement <seealso cref="IControlModule"/> to be useful
    /// </summary>
    public interface IGraphicalControlModule {
        /// <summary>
        ///     Called when the form needs to be shown
        /// </summary>
        void ShowForm();
    }
}
