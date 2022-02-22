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
using System.Windows.Shapes;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListEquipment.xaml
    /// </summary>
    public partial class ListEquipment : Window
    {
        public ListEquipment()
        {
            InitializeComponent();
            lvProduct.ItemsSource = ClassHelper.Class1.Context.Product.ToList();
        }
    }
}
