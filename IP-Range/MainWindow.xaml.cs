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
        }

        //MenuItem Events
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem;
            windowContainer wc = new windowContainer(this);
            classContainer container = tvContainers.SelectedItem as classContainer;

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
                        container.Children.Add(new classContainer(wc.Name.Text, true));
                        container.IsExpanded = true;
                    }
                    break;

                case ("RemoveContainer"):
                    var result = MessageBox.Show("Are you really wont to remove container?", "", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        var parentcollection = FindParentCollectionForContainer(Containers, container);
                        if (parentcollection != null) parentcollection.Remove(container);
                    }
                    break;

                case ("EditContainer"):

                    windowContainer wcEdit = new windowContainer(this, container);
                    wcEdit.ShowDialog();
                    if (wcEdit.DialogResult == true)
                    {
                        container.Name = wcEdit.Name.Text;
                    }
                    break;
            }
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

        //Find Collection<classContainer> From classContainer
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

        //Find TreeViewItem From Border
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

        //Find TreeViewItem From classContainer
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

        //Select TreeViewItem by RightMouse_Click
        private void Bd_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var brd = sender as Border;
            TreeViewItem tvi = FindVisualParent(brd) as TreeViewItem;
            tvi.IsSelected = true;
        }

        #region Drag and Drop

        Point startmouseposition;

        //Events for border highlight
        private void DropBorder_OnDragEnter(object sender, DragEventArgs e)
        {
            classDragDropHelper.SetIsDragOver((DependencyObject)sender, true);
        }
        private void DropBorder_OnPreviewDragLeave(object sender, DragEventArgs e)
        {
            classDragDropHelper.SetIsDragOver((DependencyObject)sender, false);
        }
        
        //Set Start Position For Delta Before Drag
        private void tvContainers_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startmouseposition = e.GetPosition(sender as TreeView);
        }

        private void tvContainers_MouseMove(object sender, MouseEventArgs e)
        {
            TreeView tv = sender as TreeView;

            if (e.LeftButton == MouseButtonState.Pressed &&
                e.GetPosition(sender as TreeView).X < tv.ActualWidth - SystemParameters.VerticalScrollBarWidth &&
                Math.Abs(e.GetPosition(sender as TreeView).X - startmouseposition.X) < 10 &
                Math.Abs(e.GetPosition(sender as TreeView).Y - startmouseposition.Y) < 10 &
                (Math.Abs(e.GetPosition(sender as TreeView).X - startmouseposition.X) > 5 ||
                Math.Abs(e.GetPosition(sender as TreeView).Y - startmouseposition.Y) > 5))
            {
                TreeViewItem tvi = (TreeViewItem)FindVisualParent(e.OriginalSource as DependencyObject);

                if (tvi != null && tvi.DataContext != null)
                {
                    DataObject dragData = new DataObject((classContainer)tvi.DataContext);
                    DragDrop.DoDragDrop(tv, dragData, DragDropEffects.Move);
                }
            }
        }

        private void tvContainers_DragEnterOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(classContainer)))
            {
                TreeViewItem tvidestination = (TreeViewItem)FindVisualParent(e.OriginalSource as DependencyObject);
                classContainer objdestination = (tvidestination != null && tvidestination.DataContext.GetType() == typeof(classContainer)) ? tvidestination.DataContext as classContainer : null;
                classContainer objsource = (classContainer)e.Data.GetData(typeof(classContainer));

                e.Effects = DragDropEffects.Move;

                if (objsource == objdestination) e.Effects = DragDropEffects.None;
                else if (objsource != null & objdestination != null)
                {
                    ObservableCollection<classContainer> selfparentcollection = FindParentCollectionForContainer(objsource.Children, objdestination);
                    if (selfparentcollection != null) e.Effects = DragDropEffects.None;

                    ObservableCollection<classContainer> sourceparentcollection = FindParentCollectionForContainer(objsource.Children, objdestination);
                    if (sourceparentcollection == objdestination.Children) e.Effects = DragDropEffects.None;
                }
                else if (objsource != null & objdestination == null)
                {
                    ObservableCollection<classContainer> sourceparentcollection = FindParentCollectionForContainer(Containers, objsource);
                    if (sourceparentcollection == Containers) e.Effects = DragDropEffects.None;
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void tvContainers_Drop(object sender, DragEventArgs e)
        {

        }

        #endregion

        #region Window management

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

        #endregion
    }
}
