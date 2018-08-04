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
    public partial class windowHost : Window
    {
        public windowHost(Window owner, classHost host = null)
        {
            this.Owner = owner;
            this.Height = this.Owner.ActualHeight * 0.8;
            this.Width = this.Owner.ActualWidth * 0.3;

            InitializeComponent();

            this.WindowStyle = WindowStyle.SingleBorderWindow;

            if (host != null)
            {
                IP_address.Text = host.IP;
                DNS_Name.Text = host.DNS_Name;
                Description.Text = host.Description;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IP_address.Focus();
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
            if (IP_address.Text == "" && DNS_Name.Text == "" && Description.Text == "")
            {
                var da = new DoubleAnimation();
                da.From = 1;
                da.To = 0;
                da.Duration = new TimeSpan(0, 0, 0, 0, 200);
                da.AutoReverse = true;
                IP_address.BeginAnimation(TextBox.OpacityProperty, da);
                DNS_Name.BeginAnimation(TextBox.OpacityProperty, da);
                Description.BeginAnimation(TextBox.OpacityProperty, da);
            }
            else DialogResult = true;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }
    }
}
