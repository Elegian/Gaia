using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Gaia.ViewModel
{
    public class MainViewModel
    {
        #region Properties
        public TreeView treeView;
        public TabControl tabControl;
        #endregion

        #region ProcessDirectory
        private TreeViewItem ProcessDirectory(string targetDirectory)
        {
            TreeViewItem dir = new TreeViewItem();
            Regex rx = new Regex(@".*(\\)");
            Match match = rx.Match(targetDirectory);
            string dirName = targetDirectory;
            dirName = dirName.Remove(0, match.Value.Length);
            dir.Header = dirName;
            dir.Tag = dirName;

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                TreeViewItem file = new TreeViewItem();
                match = rx.Match(fileName);
                string filename = fileName;
                filename = filename.Remove(0, match.Value.Length);
                file.Header = filename;
                file.Tag = filename;
                file.FontWeight = FontWeights.Normal;
                dir.Items.Add(file);
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessDirectory(subdirectory);
            }
            return dir;
        }
        #endregion

        
        #region ctor
        public MainViewModel()
        {
            
        }
        #endregion

        #region Public Methods

        #region currentProject
        public void currentProject()
        {
            treeView.Items.Add(ProcessDirectory(Directory.GetCurrentDirectory()));
        }
        #endregion

        public void openFile()
        {
            try
            {

                if (treeView.SelectedItem != null)
                {
                    TreeViewItem a = treeView.SelectedItem as TreeViewItem;
                    string name = a.Header.ToString();
                    bool notShown = true;
                    foreach (TabItem t in tabControl.Items)
                    {
                        if(t.Header.ToString() == name)
                        {
                            t.IsSelected = true;
                            notShown = false;
                            break;
                        }
                    }
                    if (notShown)
                    {
                        TabItem ti = new TabItem();
                        ti.Header = name;
                        tabControl.Items.Add(ti);
                        ti.IsSelected = true;
                    }
                }
            }
            catch(Exception ex)
            {
                // TODO Logg
            }
        }

        #endregion

    }
}
