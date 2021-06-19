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
    /// Interaction logic for CreateProductPage.xaml
    /// </summary>
    public partial class CreateProductPage : Page
    {
        private CreateProductViewModel Context { get; set;}
        public CreateProductPage(Product product,User user,bool isEdit=false)
        {
            InitializeComponent();
            Context = new CreateProductViewModel(product,user,isEdit);
            DataContext = Context;
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
           Context.AddImage();
        }

        private void AddCountry(object sender, RoutedEventArgs e)
        {
            Context.AddCountry();
        }

        private void DeleteCountry(object sender, RoutedEventArgs e)
        {
            if (ListCountry.SelectedItem is ProductCountry productCountry) 
            {
                Context.RemoveCountry(productCountry);
            };
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            Context.SaveChanges();
            this.NavigationService.GoBack();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
