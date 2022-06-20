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

    public class RowType
    {
        public string Filename { get; set; }
        public string Date { get; set; }
        public string FileSize { get; set; }
        public bool Identical { get; set; }
    }
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
        //Creates the right columns in the Datagrid 
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

        //https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
        static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
        //Opens the the Explorer, so the user can choose the folder for folder1
        public void Folder1Clickedtxt(object sender, RoutedEventArgs args)
        {
            //Datagird clear
            Foldercontent.Items.Clear();
            Foldercontent.Items.Refresh();
            //Opens explorer 
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            openFileDlg.ShowDialog();
            //Fills the Textbox with path and gives the path to the updateDatagrid function
            txtPath1.Text = openFileDlg.SelectedPath;
            updateDatagrid(openFileDlg.SelectedPath);

        }
        //Opens the the Explorer, so the user can choose the folder for folder2
        public void Folder2Clickedtxt(object sender, RoutedEventArgs args)
        {
            //Datagird clear
            Foldercontent.Items.Clear();
            Foldercontent.Items.Refresh();
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            openFileDlg.ShowDialog();
            txtPath2.Text = openFileDlg.SelectedPath;
            updateDatagrid(openFileDlg.SelectedPath);
        }
        //Displays the content of Folder1
        public void Folder1Clicked(object sender, RoutedEventArgs args) {
            //Datagird clear
            Foldercontent.Items.Clear();
            Foldercontent.Items.Refresh();

            updateDatagrid(txtPath1.Text);


        }
        //Displays the content of Folder2
        public void Folder2Clicked(object sender, RoutedEventArgs args)
        {
            //Datagird clear
            Foldercontent.Items.Clear();
            Foldercontent.Items.Refresh();
            updateDatagrid(txtPath2.Text);
        }
       
        //Fills the Datagrid with the content of the current selected folder
        public void updateDatagrid(string path)
        {
           
            // get the file attributes for file or directory
                FileAttributes attr = File.GetAttributes(path); //Fehlermeldung System.ArgumentException: "Der Pfad hat ein ungültiges Format." lösen


            //if no Folder is found return
            if (!attr.HasFlag(FileAttributes.Directory))
                return;
            //fill Datagrid with content of the selcted folder
            string [] dirContent = Directory.GetFileSystemEntries(path);
            foreach (string entry in dirContent)
            {
                FileAttributes attri = File.GetAttributes(entry);

                long length = 0;
                if (!attri.HasFlag(FileAttributes.Directory))
                {
                    length = new System.IO.FileInfo(entry).Length;
                }
                string SizeOut = SizeSuffix(length);
                var lastModified = System.IO.File.GetLastWriteTime(entry);
                string NameOnly = System.IO.Path.GetFileName(entry);

                Foldercontent.Items.Add(new RowType() { Filename = NameOnly, Date = lastModified.ToString("dd/MM/yy HH:mm:ss"), FileSize = SizeOut, Identical = false });
            }
        }
        //checks if files are the identical 
        public void IsIdentical(string path1, string path2)
        {
            //Datagrid clear
            Foldercontent.Items.Clear();
            Foldercontent.Items.Refresh();
            // get the file attributes for file or directory
            FileAttributes attr1 = File.GetAttributes(path1);
            FileAttributes attr2 = File.GetAttributes(path2);
            //if no Folder is found return
            if (!attr1.HasFlag(FileAttributes.Directory))
                return;
            //if no Folder is found return
            if (!attr2.HasFlag(FileAttributes.Directory))
                return;



        }
    }
}
