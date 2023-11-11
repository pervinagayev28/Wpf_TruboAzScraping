






using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
namespace Scraping
{
  
    public partial class MainWindow : Window
    {
        public dynamic books { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            //books = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText("BooksDataBase.json"));
            books = ScrapingClass.Call();
            DataContext = this;
        }
    }
}
