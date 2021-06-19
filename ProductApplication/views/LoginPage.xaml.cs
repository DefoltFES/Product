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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private LoginViewModel Context { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            Context= new LoginViewModel();
            DataContext = Context;
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var x = sender as TextBox;
            x.Text = string.Empty;
            x.GotFocus -= TextBox_GotFocus;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Context.isExist())
            {
                this.NavigationService.Navigate(new ListProductPage(App.dbContext.Users.Where(x=>x.Login==Context.Login).First()));
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
        }
    }
}
