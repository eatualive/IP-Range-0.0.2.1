using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IP_Range
{
    public partial class windowContainer : Window
    {
        public windowContainer(Window owner)
        {
            this.Owner = owner;
            this.Height = this.Owner.ActualHeight * 0.8;
            this.Width = this.Owner.ActualWidth * 0.3;

            InitializeComponent();

            this.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Focus();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "") DialogResult = true;
            else
            {
                var da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new TimeSpan(0, 0, 0, 0, 200);
                da.AutoReverse = true;
                Name.BeginAnimation(TextBox.OpacityProperty, da);
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }
    }
}
