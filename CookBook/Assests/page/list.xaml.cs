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
    /// Логика взаимодействия для list.xaml
    /// </summary>
    public partial class list : Page
    {
        string type = "All recipes";
        int skip = 0;
        int page = 1;
        decimal allpage;
        public list()
        {
            InitializeComponent();
            filter();
            var buttonlist = ButtonStackPanel.Children.OfType<Button>().ToList();
            var clist =  countlist.Children.OfType<Label>().ToList();
            for (int i = 0; i < buttonlist.Count; i++)
            {
                string tmp = Convert.ToString(buttonlist[i].Content);
                if (tmp == "All recipes")
                {
                    clist[i].Content = Convert.ToString(AppData.Context.Recipe.Count());
                }
                else
                {
                    clist[i].Content = Convert.ToString(AppData.Context.Recipe.Where(j => j.Type.Title == tmp).Count());
                }
            }
            
        }
        public void filter() 
        {
            var list = AppData.Context.Recipe.ToList();
            currentpage.Text = Convert.ToString(page);
            if (type == "All recipes")
            {
                list = AppData.Context.Recipe.Where(i=> i.Title.Contains(search.Text)).ToList();
            }
            else
            {
                list = AppData.Context.Recipe.Where(i => i.Type.Title == type && i.Title.Contains(search.Text)).ToList();
            }
            allpage = Math.Ceiling(Convert.ToDecimal(list.Count) / 6);
            allpages.Text = Convert.ToString(allpage);
            list = list.Skip(skip).Take(6).ToList();
            listview.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        
           
            switch ((sender as Button).Tag)
            {
                case "prev":
                    if (page > 1)
                    {
                        skip -= 6;
                        page -= 1;
                    }
        
                    break;
                case "next":
                    if (page < allpage)
                    {
                        skip += 6;
                        page += 1;
                    }

                    break;
                default:
                    var buttons = ButtonStackPanel.Children.OfType<Button>().ToList();
                    buttons.ForEach(btn =>
                    btn.Foreground = Brushes.Gray
                    );
                    (sender as Button).Foreground = Brushes.White;
                    type = Convert.ToString((sender as Button).Content);
                    break;
            }
            filter();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (sender as TextBox);
            if (textbox.Text == "Search for recipie...")
            {
                textbox.Text = "";
            }

        }

        private void listview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
       
            var send = sender as ListView;
            if (send.SelectedItem != null)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new RecipeContent(send.SelectedItem as Recipe);
            }


        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox.Text == "")
            {
                textbox.Text = "Search for recipie...";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new AddRecipe();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }
    }
}
