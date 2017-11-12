using System.Collections.Generic;
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
        List<RowModel> rowListing = new List<RowModel>();

        public MainWindow()
        {
            InitializeComponent();
            rowListing.Add(new RowModel() { FindString = string.Empty, ReplaceString = string.Empty });
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
            System.Windows.Application.Current.Shutdown();
        }
    }
}
