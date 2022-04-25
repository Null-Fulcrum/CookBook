using System;
using System.Collections.Generic;
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

namespace CookBook.Assests.page
{
    /// <summary>
    /// Логика взаимодействия для RecipeList.xaml
    /// </summary>
    public partial class RecipeList : Page
    {
        public RecipeList()
        {
            InitializeComponent();
            subframe.Content =new list();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new Auth();
        }
    }
}
