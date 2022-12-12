using Scrabble;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Scrabble2018.View
{
    /// <summary>
    /// Interaction logic for Language.xaml
    /// </summary>
    public partial class Language : Window
    {
        public Language()
        {
            InitializeComponent();
        }

        private void English_Click(object sender, RoutedEventArgs e)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            CultureInfo _info = CultureInfo.CreateSpecificCulture("en-US");
            //English
            Thread.CurrentThread.CurrentCulture = _info;
            Thread.CurrentThread.CurrentUICulture = _info;

            ///Image/banner.png
            MainWindow ab = new MainWindow();
            ab.Show();
            this.Close();
        }
        private void Japanese_Click(object sender, RoutedEventArgs e)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            CultureInfo _info = CultureInfo.CreateSpecificCulture("ja_JP");
            //Japanese
            Thread.CurrentThread.CurrentCulture = _info;
            Thread.CurrentThread.CurrentUICulture = _info;

            MainWindow ab = new MainWindow();
            ab.Show();
            this.Close();        
        }

        private void Korean_Click(object sender, RoutedEventArgs e)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            CultureInfo _info = CultureInfo.CreateSpecificCulture("ko-KR");
            //Korean
            Thread.CurrentThread.CurrentCulture = _info;
            Thread.CurrentThread.CurrentUICulture = _info;


            MainWindow ab = new MainWindow();
            ab.Show();
            this.Close();
        }

        private void Chinese_Click(object sender, RoutedEventArgs e)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            CultureInfo _info = CultureInfo.CreateSpecificCulture("zh_CN");
            //Chinese
            Thread.CurrentThread.CurrentCulture = _info;
            Thread.CurrentThread.CurrentUICulture = _info;

            MainWindow ab = new MainWindow();
            ab.Show();
            this.Close();
        }
    }
}
