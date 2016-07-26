using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_ListElements
{
    public partial class MainWindow : Window
    {
        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem itemOld = e.OldValue as TreeViewItem;
            TreeViewItem itemNew = e.NewValue as TreeViewItem;
            if (e.OldValue != null && e.NewValue != null)
            {
                txtOld.Text = itemOld.Header.ToString();
                txtNew.Text = itemNew.Header.ToString();
            }
        }

        private void TreeViewItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem tvItem = sender as TreeViewItem;
            if (tvItem != null)
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    tvItem.IsExpanded = true;
                    Expand(tvItem);
                }
            }
        }

        private void Expand(TreeViewItem item)
        {
            foreach (TreeViewItem node in item.Items)
            {
                node.IsExpanded = true;
                if (node.Items.Count > 0) Expand(node);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem del = treeView1.SelectedItem as TreeViewItem;
            if (del != null)
            {
                TreeViewItem parent = del.Parent as TreeViewItem;
                if (parent != null)
                    parent.Items.Remove(del);
                else
                    treeView1.Items.Remove(del);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (treeView1.Items.Count == 0 || chkBox1.IsChecked == true)
            {
                treeView1.Items.Add(new TreeViewItem { Header = txtNewItem.Text });
                return;
            }
            TreeViewItem selectedItem = (TreeViewItem)treeView1.SelectedItem;
            if (selectedItem != null)
                selectedItem.Items.Add(new TreeViewItem { Header = txtNewItem.Text });
        }
    }
}
