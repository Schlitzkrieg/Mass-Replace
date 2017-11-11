using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (CaseCheckBox.IsChecked.Value == false)
                mainText = mainText.ToUpper();


            foreach(RowModel m in rowListing)
            {
                if (CaseCheckBox.IsChecked.Value == false)
                {
                    m.FindString = m.FindString.ToUpper();
                    m.ReplaceString = m.ReplaceString.ToUpper();

                }
                mainText = mainText.Replace(m.FindString, m.ReplaceString);
                MainTextArea.Text = mainText;
            }
            
        }
    }
}
