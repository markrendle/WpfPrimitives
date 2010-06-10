using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel
{
    public static class PropertyChangedEventHandlerExtensions
    {
        /// <summary>
        /// Raises the specified <see cref="PropertyChangedEventHandler"/>, and handles the null check.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="propertyName">Name of the property being changed.</param>
        /// <returns><c>true</c> if any delegates were attached and the event was raised; otherwise, <c>false</c>.</returns>
        public static bool Raise(this PropertyChangedEventHandler handler, object sender, string propertyName)
        {
            if (handler == null) return false;

            handler(sender, new PropertyChangedEventArgs(propertyName));

            return true;
        }
    }
}
