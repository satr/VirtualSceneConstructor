using System;
using System.IO;
using System.Windows.Controls;
using VirtualScene.PresentationComponents.WPF.Commands;

namespace VirtualScene.ApplicationPresentationComponents.WPF.ViewModels
{
    /// <summary>
    /// The view model for the HelpView
    /// </summary>
    public class HelpViewModel
    {
        const string HelpFileName = "Help.htm";

        /// <summary>
        /// Creates a new instance of the view model for the HelpView
        /// </summary>
        /// <param name="browser">The web browser control to display the help</param>
        /// <param name="closeViewAction">The action closing the view</param>
        public HelpViewModel(WebBrowser browser, Action closeViewAction)
        {
            browser.Source = GetHelpSource();
            HideHelpCommand = new DelegateCommand(closeViewAction);
        }

        /// <summary>
        /// The comman closing the help view
        /// </summary>
        public DelegateCommand HideHelpCommand { get; set; }

        /// <summary>
        /// Gets the help source for the web-browser control
        /// </summary>
        public Uri GetHelpSource()
        {
            var helpFileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), HelpFileName));
            return helpFileInfo.Exists ? new Uri(String.Format("file:///{0}", helpFileInfo.FullName)) : null;
        }
    }
}