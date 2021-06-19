using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace ProductApplication.viewModels
{
    class CreateProductViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Product product;
        private User User { get; set; }

        private bool IsEdit { get; set; }

        public string Name { get => product.Name;set
            {
                product.Name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public string Comment
        {
            get => product.Comment;
            set
            {
                product.Comment = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Comment"));
            }
        }
        public string DateAdd
        {
            get => product.DataAdd;
            set
            {
                product.DataAdd = value;
               
            }
        }

        public byte[] Image
        {
            get => product.Photo;
            set
            {
                product.Photo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Image"));
            }
        }
        public TypeUnit TypeUnit { get=>product.TypeUnit; 
            set 
            {
                product.TypeUnit = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TypeUnit"));
            } 
        }
        public List<TypeUnit> TypeUnits { get; set;}
        private ObservableCollection<ProductCountry> countryProducts;
        public ObservableCollection<ProductCountry> CountryProducts { get=> countryProducts; 
            set 
            {
                countryProducts = value;
                OnPropertyRaised("CountryProducts");
            }
        }
        public List<Country> Countrys { get; set; }
        public CreateProductViewModel(Product product,User user,bool isEdit)
        {
            this.product = product;
            IsEdit = isEdit;
            DateAdd = DateTime.Today.ToString("dd/MM/yyyy");
            TypeUnits = App.dbContext.TypeUnits.ToList();
            Countrys=App.dbContext.Countries.ToList();
            User= user;
            CountryProducts = new ObservableCollection<ProductCountry>(product.ProductCountry);
            if (CountryProducts.Count == 0)
            {
                CountryProducts.Add(new ProductCountry());
            }
            
        }
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image File (*.jpg;*.png)|*.jpg;*.png",
                CheckPathExists = true,
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo file = new FileInfo(openFileDialog.FileName);
                if (file.Length > 153600)
                {
                    MessageBox.Show("Нельзя добавлять изображения больше 150 кбайт");
                    return;
                }
                else
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    Image = BitmapSourceToByteArray(new BitmapImage(fileUri));
                }
            }
        }

        private byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        public void AddCountry()
        {
            CountryProducts.Add(new ProductCountry());
        }

        public void RemoveCountry(ProductCountry productCountry)
        {
            CountryProducts.Remove(productCountry);
        }

        public void SaveChanges()
        {
            if (IsEdit == false)
            {
                foreach (var item in CountryProducts)
                {
                    product.ProductCountry.Add(item);
                }
                User.Product.Add(this.product);
                App.dbContext.Products.Add(this.product);
                App.dbContext.SaveChanges();
            }
            if(IsEdit == true)
            {
                product.ProductCountry.Clear();
                foreach (var item in CountryProducts)
                {
                    product.ProductCountry.Add(item);
                }
                App.dbContext.Products.Attach(product);
                App.dbContext.Entry(product).State = EntityState.Modified;
                App.dbContext.SaveChanges();
            }
        }
    }
}

