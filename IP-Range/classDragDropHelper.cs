using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IP_Range
{
    public class classDragDropHelper
    {
        public static readonly DependencyProperty IsDragOverProperty = DependencyProperty.RegisterAttached("IsDragOver", typeof(bool), typeof(classDragDropHelper), new PropertyMetadata(default(bool)));

        public static void SetIsDragOver(DependencyObject element, bool value)
        {
            element.SetValue(IsDragOverProperty, value);
        }

        public static bool GetIsDragOver(DependencyObject element)
        {
            return (bool)element.GetValue(IsDragOverProperty);
        }
    }
}
