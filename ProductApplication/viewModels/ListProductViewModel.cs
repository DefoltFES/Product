using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApplication.viewModels
{
    class ListProductViewModel:INotifyPropertyChanged
    {
        public int Id { get; set; }
        private List<Product> filterList;
        private int filterCount;
        private int allcount;
        public List<Product> AllList { get; set; }
        public List<Product> FilterList 
        {   
            get=> filterList; 
            set
            {
                filterList = value;
                OnPropertyRaised("FilterList");
            }
        }
        private string searchTerm;
        public string SearchTerm { get=>searchTerm; 
            set 
            {
                searchTerm = value;
                OnPropertyRaised("SearchTerm");
            } 
        }
        public bool Kg { get; set; }
        public bool Sht {get; set;}
        public bool L {get; set;}
        
        public bool ThisMounth { get; set; }
        public int AllCount { get=>allcount; 
            set 
            {
                allcount = value;
                OnPropertyRaised("AllCount");
            } 
        }
        public int FilterCount { get=>filterCount; 
            set 
            {
                filterCount = value;
                OnPropertyRaised("FilterCount");
            } 
        }


        public ListProductViewModel(User user)
        {
            Id = user.Id;        
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Sort()
        {
            if (SearchTerm != "" && SearchTerm != null)
            {
                FilterList = AllList.Where(x => x.Name.ToLower().Contains(SearchTerm.ToLower()) || x.Comment.ToLower().Contains(SearchTerm.ToLower())).ToList();
            }if(SearchTerm=="" || SearchTerm == null)
            {
                FilterList = AllList;
            }

            if (L && !Sht && !Kg)
            {
                FilterList = FilterList.Where(x => x.TypeUnit.Name == "л").ToList();
            }
            if (Sht && !L && !Kg)
            {
                FilterList = FilterList.Where(x => x.TypeUnit.Name == "шт").ToList();
            }
            if (Kg && !L && !Sht)
            {
                FilterList = FilterList.Where(x => x.TypeUnit.Name == "кг").ToList();
            }
            if (L && Sht && !Kg)
            {
                FilterList = FilterList.Where(x => x.TypeUnit.Name == "л" || x.TypeUnit.Name == "шт").ToList();   
            }
            if (Kg && L && !Sht)
            {
                FilterList= FilterList.Where(x => x.TypeUnit.Name == "кг" || x.TypeUnit.Name == "л").ToList();
            }
            if (Kg && Sht && !L)
            {
                FilterList= FilterList.Where(x => x.TypeUnit.Name == "кг" || x.TypeUnit.Name == "шт").ToList();
            }
            if (L && Sht && Kg)
            {
                FilterList = FilterList.Where(x => x.TypeUnit.Name == "кг" || x.TypeUnit.Name == "шт" || x.TypeUnit.Name == "л").ToList();
            }
            if (ThisMounth)
            {
                FilterList = FilterList.Where(x => Convert.ToDateTime(x.DataAdd).Month == DateTime.Now.Month).ToList();
            }
            FilterCount = FilterList.Count;

        }

       

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public void Refresh()
        { 
          AllList = App.dbContext.Products.Where(x => x.UserId == Id).ToList();
          FilterList = AllList;
            L = false;
          Sht = false;
          Kg = false;
            AllCount = AllList.Count;
           FilterCount = FilterList.Count;
            
        }

        public void Delete(Product product)
        {
            FilterList.Remove(product);
            App.dbContext.ProductsCountries.RemoveRange(product.ProductCountry);
            App.dbContext.Products.Remove(product);
            App.dbContext.SaveChanges();
        }
    }
}
