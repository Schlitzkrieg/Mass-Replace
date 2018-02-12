using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Mass_Replace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string stateFile = Directory.GetCurrentDirectory().ToString() + @"\config.csv";
        List<RowModel> rowListing = new List<RowModel>();

        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(stateFile))
            {
                LoadState();
            }
            else
            {
                rowListing.Add(new RowModel() { FindString = string.Empty, ReplaceString = string.Empty });
            }
            FindReplaceGrid.ItemsSource = rowListing;
        }

        private void DataGridCell_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int cellIndex = FindReplaceGrid.SelectedIndex;
            if (cellIndex == FindReplaceGrid.Items.Count)
            {
                rowListing.Add(new RowModel() { FindString = string.Empty, ReplaceString = string.Empty });
            }
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(RowModel m in rowListing)
            {
                string newFind = m.ReplaceString;
                string newReplace = m.FindString;
                m.ReplaceString = newReplace;
                m.FindString = newFind;
            }
            FindReplaceGrid.Items.Refresh();
        }

        private void ReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            string mainText = MainTextArea.Text;
            if(mainText.Length == 0)
            {
                MessageBox.Show("No text to replace");
                return;
            }
            
            foreach(RowModel m in rowListing)
            {
                if (CaseCheckBox.IsChecked.Value == false)
                {
                    mainText = Regex.Replace(mainText, m.FindString, m.ReplaceString, RegexOptions.IgnoreCase);
                }
                else
                {
                    mainText = mainText.Replace(m.FindString, m.ReplaceString);
                }
                MainTextArea.Text = mainText;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            SaveState();
            System.Windows.Application.Current.Shutdown();
        }

        private void SaveState()
        {
            //if the file exists, clear it out
            if (File.Exists(stateFile))
            {
                File.WriteAllText(stateFile, string.Empty);
            }
            else//else, create it!
            {
                using (var config = File.Create(stateFile))
                {
                    config.Close();
                }
            }

            using (StreamWriter file = new StreamWriter(stateFile))
            {
                foreach (RowModel m in rowListing)
                {
                    file.WriteLine(m.FindString + "," + m.ReplaceString);
                }
            }
        }

        private void LoadState()
        {
            try
            {
                foreach (string line in File.ReadLines(stateFile))
                {
                    string[] vals = line.Split(',');
                    rowListing.Add(new RowModel() { FindString = vals[0], ReplaceString = vals[1]});
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
