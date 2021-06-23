using ProductApplication.viewModels;
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

namespace ProductApplication.views
{
    /// <summary>
    /// Interaction logic for ListProductPage.xaml
    /// </summary>
    public partial class ListProductPage : Page
    {
        private ListProductViewModel Context { get; set; }
        public ListProductPage(User user)
        {
            InitializeComponent();
            Context = new ListProductViewModel(user);
            DataContext = Context;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if(productGrid.SelectedItem is Product product)
            {
                this.NavigationService.Navigate(new CreateProductPage(product, App.dbContext.Users.Find(Context.Id),true));
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if(productGrid.SelectedItem is Product product)
            {
                var message = MessageBox.Show("Вы хотите удалить продукт ?", "Предупреждение", MessageBoxButton.OKCancel);
                if (message == MessageBoxResult.OK)
                {
                    Context.Delete(product);
                }
                else
                {
                    return;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Context.Refresh();
        }

        private void kg_Checked(object sender, RoutedEventArgs e)
        {
            Context.Kg = true;
            Context.Sort();
        }

        private void kg_Unchecked(object sender, RoutedEventArgs e)
        {
            Context.Kg = false;
            Context.Sort();
        }

        private void sht_Checked(object sender, RoutedEventArgs e)
        {
            Context.Sht = true;
            Context.Sort();
        }

        private void sht_Unchecked(object sender, RoutedEventArgs e)
        {
            Context.Sht = false;
            Context.Sort();
        }

        private void l_Checked(object sender, RoutedEventArgs e)
        {
            Context.L = true;
            Context.Sort();
        }

        private void l_Unchecked(object sender, RoutedEventArgs e)
        {
            Context.L = false;
            Context.Sort();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            l.IsChecked = false;
            sht.IsChecked = false;
            kg.IsChecked = false;
            ThisMounth.IsChecked = false;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var x = sender as TextBox;
            x.Text = string.Empty;
            x.GotFocus -= TextBox_GotFocus;
        }
     
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateProductPage(new Product(),App.dbContext.Users.Find(Context.Id)));
        }

        private void ThisMounth_Checked(object sender, RoutedEventArgs e)
        {
            Context.ThisMounth = true;
            Context.Sort();
        }

        private void ThisMounth_Unchecked(object sender, RoutedEventArgs e)
        {
            Context.ThisMounth = false;
            Context.Sort();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Context.Sort();
        }
    }
}
