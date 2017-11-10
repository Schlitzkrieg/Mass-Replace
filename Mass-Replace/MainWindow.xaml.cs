using System;
using System.Collections.Generic;
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
        DataTable mainTable;

        public MainWindow()
        {
            InitializeComponent();
            mainTable = new DataTable();
            mainTable.Columns.Add("Find", typeof(string));
            mainTable.Columns.Add("Replace", typeof(string));
            FindReplaceGrid.ItemsSource = mainTable.AsDataView();
        }

        private void DataGridCell_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int cellIndex = FindReplaceGrid.SelectedIndex;
            if (cellIndex == mainTable.Rows.Count)
            {
                mainTable.Rows.Add();
            }
        }

        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
