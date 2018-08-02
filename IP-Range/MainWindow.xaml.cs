using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static IP_Range.classTools;

namespace IP_Range
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tvContainers.ItemsSource = Containers;
            b
        }



        //MenuItem events
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem;
            windowContainer wc = new windowContainer(this);

            switch (mi.Name)
            {
                case ("AddContainer"):
                    wc.ShowDialog();
                    if (wc.DialogResult == true)
                    {
                        Containers.Add(new classContainer(wc.Name.Text, true));
                    }
                    break;

                case ("AddSubContainer"):
                    wc.ShowDialog();
                    if (wc.DialogResult == true)
                    {
                        classContainer container = tvContainers.SelectedItem as classContainer;
                        container.Children.Add(new classContainer(wc.Name.Text, true));
                        container.IsExpanded = true;
                    }
                    break;

                case ("RemoveContainer"):

                    break;
            }
        }


        public ObservableCollection<classContainer> FindParentCollectionForContainer(ObservableCollection<classContainer> containers, classContainer container)
        {
            ObservableCollection<classContainer> p = null;
            foreach (var c in containers)
            {
                if (c == container)
                    return containers;
                else
                {
                    p = FindParentCollectionForContainer(c.Children, container);
                    if (p != null)
                        break;
                }
            }
            return p;
        }

        public TreeViewItem FindTreeViewItemFromObject(ItemContainerGenerator icg, object obj)
        {
            TreeViewItem tvi = (TreeViewItem)icg.ContainerFromItem(obj);
            if (tvi != null)
                return tvi;

            for (int i = 0; i < icg.Items.Count; i++)
            {
                tvi = FindTreeViewItemFromObject(((TreeViewItem)icg.ContainerFromIndex(i)).ItemContainerGenerator, obj);
                if (tvi != null)
                    break;
            }

            return tvi;
        }

        //Move Container
        private void tvContainers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.Up && Keyboard.Modifiers == ModifierKeys.Alt)
            {
                var parentcollection = FindParentCollectionForContainer(Containers, tvContainers.SelectedItem as classContainer);
                if (parentcollection == null) return;
                var i = parentcollection.IndexOf(tvContainers.SelectedItem as classContainer);
                if (i > 0) parentcollection.Move(i, i - 1);

                var tvi = FindTreeViewItemFromObject(((TreeView)sender).ItemContainerGenerator, ((TreeView)sender).SelectedItem);
                if (tvi != null) tvi.BringIntoView();
            }
            else if (e.SystemKey == Key.Down && Keyboard.Modifiers == ModifierKeys.Alt)
            {
                var parentcollection = FindParentCollectionForContainer(Containers, tvContainers.SelectedItem as classContainer);
                if (parentcollection == null) return;
                var i = parentcollection.IndexOf(tvContainers.SelectedItem as classContainer);
                if (i < parentcollection.Count - 1 && i >= 0) parentcollection.Move(i, i + 1);

                var tvi = FindTreeViewItemFromObject(((TreeView)sender).ItemContainerGenerator, ((TreeView)sender).SelectedItem);
                if (tvi != null) tvi.BringIntoView();
            }
        }

        //Select TreeViewItem by RightMouse_Click
        private void Bd_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var brd = sender as Border;
            TreeViewItem tvi = FindVisualParent(brd) as TreeViewItem;
            tvi.IsSelected = true;
        }

        private object FindVisualParent(DependencyObject child)
        {
            object parent = VisualTreeHelper.GetParent(child);

            if (parent != null)
            {
                if (parent is TreeViewItem)
                    return parent as TreeViewItem;
                else
                    return FindVisualParent(parent as DependencyObject);
            }
            else
                return null;
        }

        // Window management

        //Close window
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Minimize window
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Maximize-minimize window
        private void WindowFullScreen(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        //Thicknes control on fullscreen
        private void Window_StateChanged(object sender, EventArgs e)
        {
            MainGrid.Margin = (this.WindowState != WindowState.Maximized) ? new Thickness(0, 0, 0, 0) : new Thickness(7, 7, 7, 7);
        }
    }
}
