using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace System.Windows
{
    public static class FrameworkElementExtensions
    {
        public static IEnumerable<FrameworkElement> GetAllAncestors(this FrameworkElement element)
        {
            while ((element = VisualTreeHelper.GetParent(element) as FrameworkElement) != null)
            {
                yield return element;
            }
        }
    }
}
