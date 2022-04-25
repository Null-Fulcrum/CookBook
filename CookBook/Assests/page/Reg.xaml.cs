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
using CookBook.Assests.DB;

namespace CookBook.Assests.page
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        string temp;
        public Reg()
        {
            InitializeComponent();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text == "" || TbNickname.Text == "" || TbPassword.Password == "" || TbLogin.Text == "Login" || TbNickname.Text == "Nikname")
            {
                TbInvalid.Visibility = Visibility.Visible;
                TbLogin.BorderThickness = new Thickness(1);
                TbPassword.BorderThickness = new Thickness(1);
                TbNickname.BorderThickness = new Thickness(1);

            }
            else
            {
                User user = new User();
                user.Login = TbLogin.Text;
                user.Password = TbPassword.Password;
                user.Nickname = TbNickname.Text;
                AppData.Context.User.Add(user);
                AppData.Context.SaveChanges();
                ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new Auth();
            }
        
        }

        private void TbNickname_GotFocus(object sender, RoutedEventArgs e)
        {
            var texbox = sender as TextBox;
            switch (texbox.Name)
            {
                case "TbNickname":
                    if (texbox.Text == "Nikname")
                    {
                        texbox.Text = "";
                    }
                    break;
                case "TbLogin":
                    if (texbox.Text == "Login")
                    {
                        texbox.Text = "";
                    }
                    break;
             
            }
        }

        private void TbNickname_LostFocus(object sender, RoutedEventArgs e)
        {
            
            var textbox = sender as TextBox;
            switch (textbox.Name)
            {
                case "TbNickname":
                    if (textbox.Text == "")
                    {
                        textbox.Text = "Nikname";
                    }
                    break;
                case "TbLogin":
                    if (textbox.Text == "")
                    {
                        textbox.Text = "Login";
                    }
                    break;

            }

        }
    }
}
