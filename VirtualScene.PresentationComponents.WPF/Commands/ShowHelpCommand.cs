using System.ComponentModel;
using VirtualScene.PresentationComponents.WPF.Views;

namespace VirtualScene.PresentationComponents.WPF.Commands
{
    /// <summary>
    /// The command shows the help window
    /// </summary>
    public class ShowHelpCommand : CommandBase
    {
        private HelpView _helpView;

        /// <summary>
        /// Show the help window
        /// </summary>
        protected override void Execute()
        {
            if (_helpView == null)
                _helpView = new HelpView();
            if (_helpView.IsVisible)
                _helpView.Focus();
            else
                _helpView.Show();
            _helpView.Closing += HelpViewOnClosing;
        }

        private void HelpViewOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _helpView.Hide();
            cancelEventArgs.Cancel = true;
        }
    }
}