using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace WpfDragDrop.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        private void ItemsControl_Drop(object sender, DragEventArgs e)
        {
            var control = sender as ItemsControl;
            var source = control.ItemsSource as IList;
            if (source != null)
            {
                source.Add(e.Data.GetData(typeof(Model)));
            }
        }

        private void ItemsControl_DragEnter(object sender, DragEventArgs e)
        {
//            if (e.Data.GetDataPresent(typeof(Model)))
//            {
//                e.Effects = DragDropEffects.All;
//            }
        }

        private void ItemsControl_DragOver(object sender, DragEventArgs e)
        {
//            if (e.Data.GetDataPresent(typeof(Model)))
//            {
//                e.Effects = DragDropEffects.All;
//            }
        }
    }
}
