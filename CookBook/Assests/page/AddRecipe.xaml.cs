using System;
using System.Collections.Generic;
using System.IO;
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
using CookBook.Assests.Classes;
using CookBook.Assests.DB;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace CookBook.Assests.page
{
    /// <summary>
    /// Логика взаимодействия для AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Page
    {
        string pathPhoto = "";
        string recipie = "Qty.:Product";
        string instruction = "describe the cooking procces";
        List<string> types = new List<string>();
        List<string> ingridients = new List<string>();
        List<string> instructions = new List<string>();
       
   
        public AddRecipe()
        {
            InitializeComponent();
            var typelist = AppData.Context.Type.ToList();
            types.Add("Choose type");
            for (int i = 0; i < typelist.Count; i++)
            {
                types.Add(typelist[i].Title);
            }
            combobox.ItemsSource = types.ToList();
            combobox.SelectedIndex = 0;
            
            AddEditIngridients(recipie);
            AddEditInstructions(instruction);
        }
        //Добавление поля рецепта
        public void AddEditIngridients(string recipie)
        {

            var ingridient = recipie.Split(':');
            TextBox text = new TextBox();
            text.Text = ingridient[0];
            text.Background = null;
            text.BorderBrush = Brushes.Gray;
            text.BorderThickness = new Thickness(0, 0, 0, 1);
            text.Foreground = Brushes.Gray;
            text.FontSize = 10;
            text.Tag = "quantity";
            text.Width = 50;
            text.GotFocus += Text_GotFocus;
            text.LostFocus += Text_LostFocus;
            text.Margin = new Thickness(0, 0, 10, 0);
            text.TextWrapping = TextWrapping.Wrap;
            TextBox text1 = new TextBox();
            text1.Text = ingridient[1];
            text1.Background = null;
            text1.GotFocus += Text_GotFocus;
            text1.LostFocus += Text_LostFocus;
            text1.BorderBrush = Brushes.Gray;
            text1.BorderThickness = new Thickness(0, 0, 0, 1);
            text1.Foreground = Brushes.Gray;
            text1.FontSize = 10;
            text1.Tag = "product";
            text1.Width = 140;
            text1.TextWrapping = TextWrapping.Wrap;
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.Margin = new Thickness(0, 0, 0, 10);
            stack.Children.Add(text);
            stack.Children.Add(text1);
            ingridientlist.Children.Add(stack);
        }
        //Добавление поля интрукции по приготовлению рецепта
        public void AddEditInstructions(string instruction)
        {
            TextBox text1 = new TextBox();
            text1.Text = instruction;
            text1.Background = null;
            text1.BorderBrush = Brushes.Gray;
            text1.BorderThickness = new Thickness(0, 0, 0, 1);
            text1.Foreground = Brushes.Gray;
            text1.FontSize = 10;
            text1.Width = 350;
            text1.Tag = "insrtuction";
            text1.GotFocus += Text_GotFocus;
            text1.LostFocus += Text_LostFocus;
            text1.TextWrapping = TextWrapping.Wrap;
            instructionlist.Children.Add(text1);

        }

        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (textbox.Tag)
            {
                case "quantity":
                    if (textbox.Text == "")
                    {
                        textbox.Text = "Qty.";
                    }
                    break;
                case "product":
                    if (textbox.Text == "")
                    {
                        textbox.Text = "Product";
                    }
                    break;
                case "insrtuction":
                    if (textbox.Text == "")
                    {
                        textbox.Text = "describe the cooking procces";
                    }
                    break;
                case "title":
                    if (textbox.Text == "")
                    {
                        textbox.Text = "Add title for the recipe";
                    }
                    break;
                default:
                    break;
            }
        }

        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (textbox.Tag)
            {
                case "quantity":
                    if (textbox.Text == "Qty.")
                    {
                        textbox.Text = "";
                    }
                    break;
                case "product":
                    if (textbox.Text == "Product")
                    {
                        textbox.Text = "";
                    }
                    break;
                case "insrtuction":
                    if (textbox.Text == "describe the cooking procces")
                    {
                        textbox.Text = "";
                    }
                    break;
                case "title":
                    if (textbox.Text == "Add title for the recipe")
                    {
                        textbox.Text = "";
                    }
                    break;
                default:
                    break;
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Content)
            {
                case "Add instruction":
                    AddEditInstructions(instruction);
                    break;
                case "Remove last instruction":
                    if (instructionlist.Children.Count > 1)
                    {
                        instructionlist.Children.Remove(instructionlist.Children.OfType<TextBox>().Last());
                    }

                    break;
                case "Add ingridient":
                    AddEditIngridients(recipie);
                    break;
                case "Remove last ingridient":
                    if (ingridientlist.Children.Count > 1)
                    {
                        ingridientlist.Children.Remove(ingridientlist.Children.OfType<StackPanel>().Last());
                    }
                    break;
                case "Upload Image":
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        image.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                        pathPhoto = openFileDialog.FileName;
                    }
                    break;
                case "Create":
                    var ingridientslist = ingridientlist.Children.OfType<StackPanel>().ToList();
                    var instructionslist = instructionlist.Children.OfType<TextBox>().ToList();
            
                    DB.Recipe newrecipe = new Recipe();
                    newrecipe.Title = titles.Text;
                    newrecipe.IDType = combobox.SelectedIndex;
                    for (int i = 0; i < ingridientslist.Count; i++)
                    {
                        var ingridient = ingridientslist[i].Children.OfType<TextBox>().ToList();
                        for (int j = 0; j < ingridient.Count - 1; j++)
                        {
                            newrecipe.Ingridients += ingridient[j].Text + ":" + ingridient[j + 1].Text + ";";
                        }
                    }
                    for (int i = 0; i < instructionslist.Count; i++)
                    {
                        newrecipe.Intructions += instructionslist[i].Text + ";";
                    }
                    if (pathPhoto != "")
                    {
                        newrecipe.Photo = File.ReadAllBytes(pathPhoto);
                    }
                    AppData.Context.Recipe.Add(newrecipe);
                    AppData.Context.SaveChanges();
                    ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new RecipeList();
                    break;
                case "Cancel":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).frame.Content = new RecipeList();
                    break;
                default:
                    break;
            }
    
        }
    }

}
