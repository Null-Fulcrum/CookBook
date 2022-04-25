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
using CookBook.Assests.Classes;

namespace CookBook.Assests.page
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new Reg();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            var user = AppData.Context.User.Where(i => i.Login == TbLogin.Text && i.Password == TbPassword.Password).FirstOrDefault();
            if (user != null)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new RecipeList();
            }
            else
            {
                TbInvalid.Visibility = Visibility.Visible;
                TbLogin.BorderThickness = new Thickness(1);
                TbPassword.BorderThickness = new Thickness(1);
            }
            
        }

      
    }
}
