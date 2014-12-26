using System.ComponentModel;
using VirtualScene.ApplicationPresentationComponents.WPF.Views;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.ApplicationPresentationComponents.WPF.Commands
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
        /// <param name="parameter"></param>
        protected override void ExecuteAction(object parameter)
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