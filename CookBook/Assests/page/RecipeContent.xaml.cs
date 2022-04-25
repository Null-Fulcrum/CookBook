using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    /// Логика взаимодействия для RecipeContent.xaml
    /// </summary>
    public partial class RecipeContent : Page
    {
        List<string> recipes = new List<string>();
        List<string> instructions = new List<string>();
        Recipe currentrecipe = new Recipe();
        
        public RecipeContent(Recipe recipe)
        {
            InitializeComponent();
            currentrecipe = recipe;
            if (recipe.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(recipe.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    image.Source = bitmapImage;

                }
            }
            TbTitle.Text = Convert.ToString(recipe.Title);
            string[] tmpIngridient = recipe.Ingridients.Split( new char[] { ';' },StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in tmpIngridient)
            {
                recipes.Add(item);
            }
            string[] tmpInstruction = recipe.Intructions.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in tmpInstruction)
            {
                instructions.Add(item);
            }

            for (int i = 0; i < recipes.Count(); i++)
            {
                AddIngridient(recipes[i]);
            }
            for (int i = 0; i < tmpInstruction.Count(); i++)
            {
                AddInstruction(tmpInstruction[i], i);
            }

        }
        public void AddIngridient(string recipie)
        {
            ingridientlist.Children.OfType<StackPanel>().ToList().Clear();
            var ingridient = recipie.Split(':');
            TextBlock text = new TextBlock();
            text.Text = ingridient[0];
            text.Foreground = Brushes.Gray;
            text.FontSize = 10;
            text.Width = 60;
            text.TextWrapping = TextWrapping.Wrap;
            TextBlock text1 = new TextBlock();
            text1.Text = ingridient[1];
            text1.Foreground = Brushes.Gray;
            text1.FontSize = 10;
            text1.Width = 180;
            text1.TextWrapping = TextWrapping.Wrap;
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Margin = new Thickness(0, 0, 0, 10);
            stack.Children.Add(text);
            stack.Children.Add(text1);
            ingridientlist.Children.Add(stack);
        }
        public void AddInstruction(string instruction,int num)
        {
            num = num + 1;
            instructionlist.Children.OfType<StackPanel>().ToList().Clear();
            TextBlock text = new TextBlock();
            text.Text = Convert.ToString(num) + ".";
            text.Foreground = Brushes.Gray;
            text.FontSize = 10;
            text.Width = 30;
            text.TextWrapping = TextWrapping.Wrap;
            TextBlock text1 = new TextBlock();
            text1.Text = instruction;
            text1.Foreground = Brushes.Gray;
            text1.FontSize = 10;
            text1.Width = 350;
            text1.TextWrapping = TextWrapping.Wrap;
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Margin = new Thickness(0, 0, 0, 10);
            stack.Children.Add(text);
            stack.Children.Add(text1);
            instructionlist.Children.Add(stack);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            switch (btn.Content)
            {
                case "Back":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new RecipeList();
                    break;
                case "Delete":
                    AppData.Context.Recipe.Remove(currentrecipe as Recipe);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new RecipeList();
                    break;
                case "Edit":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new ChangeRecipe(currentrecipe as Recipe);
                    break;
                default:
                    break;
            }
        }
    }
}
