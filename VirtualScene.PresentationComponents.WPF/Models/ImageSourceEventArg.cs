using System;
using System.Windows.Media;

namespace VirtualScene.PresentationComponents.WPF.Models
{
    /// <summary>
    /// Event argument for an updated image
    /// </summary>
    public class ImageSourceEventArg : EventArgs
    {
        /// <summary>
        /// Updated image source
        /// </summary>
        public ImageSource ImageSource { get; set; }

        /// <summary>
        /// Creates a new instance of the event argument for an updated image
        /// </summary>
        /// <param name="imageSource"></param>
        public ImageSourceEventArg(ImageSource imageSource)
        {
            ImageSource = imageSource;
        }
    }
}