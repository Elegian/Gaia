using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gaia
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.MainViewModel mvm;
        public MainWindow()
        {
            InitializeComponent();
            mvm = new ViewModel.MainViewModel();
            this.DataContext = mvm;
            mvm.treeView = this.treeView;
            mvm.tabControl = this.tabControl;
        }

        private void ribbonbar_Loaded(object sender, RoutedEventArgs e)
        {
            //here we get rid off the quickaccesstoolbar
            Grid child = VisualTreeHelper.GetChild((DependencyObject)sender, 0) as Grid;
            if (child != null)
            {
                child.RowDefinitions[0].Height = new GridLength(0);
            }
        }

        private void treeView_Loaded(object sender, RoutedEventArgs e)
        {
            mvm.currentProject();
        }

        private void treeView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mvm.openFile();
        }
    }
}
