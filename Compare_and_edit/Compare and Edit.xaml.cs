using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Button = System.Windows.Controls.Button;

namespace Compare_and_edit
{
    /// <summary>
    /// Interaktionslogik für Compare_and_Edit.xaml
    /// </summary>
    public partial class Compare_and_Edit : Window
    {
        public Compare_and_Edit()
        {
            InitializeComponent();
            InitializeDatagrid();
        }
        private void InitializeDatagrid()
        {
            DataGridTextColumn textColumn1 = new DataGridTextColumn();
            textColumn1.Header = "Filename";
            textColumn1.Binding = new Binding("Filename");
            Foldercontent.Columns.Add(textColumn1);
            DataGridTextColumn textColumn2 = new DataGridTextColumn();
            textColumn2.Header = "Date";
            textColumn2.Binding = new Binding("Date");
            Foldercontent.Columns.Add(textColumn2);
            DataGridTextColumn textColumn3 = new DataGridTextColumn();
            textColumn3.Header = "FileSize";
            textColumn3.Binding = new Binding("FileSize");
            Foldercontent.Columns.Add(textColumn3);
            DataGridTextColumn textColumn4 = new DataGridTextColumn();
            textColumn4.Header = "Identical";
            textColumn4.Binding = new Binding("Identical");
            Foldercontent.Columns.Add(textColumn4);
        }

        public void OpenExplorer(object sender, RoutedEventArgs args)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            string myFolder = ((Button)sender).Tag.ToString();
            MessageBox.Show(myFolder);
            if (result.ToString() != string.Empty)
            {
                if (myFolder == "Folder1")
                {
                    txtPath1.Text = openFileDlg.SelectedPath;
                }
                if (myFolder == "Folder2")
                {
                    txtPath2.Text = openFileDlg.SelectedPath;
                }
            }

            //string varPath = openFileDlg.SelectedPath;
            //string[] fileFullNames = Directory.GetFiles(varPath);

            //for (int i = 0; i < fileFullNames.Length; i++)
            //{
            //    Foldercontent.(fileFullNames[i].Substring(varPath.Length, fileFullNames[i].Length - varPath.Length), Directory.GetCreationTime(fileFullNames[i]).ToString());
            //}
            //MessageBox.Show(Foldercontent.Columns.Count.ToString());

            //Process.Start("explorer.exe", @"c:\folder");
        }
    }
}
