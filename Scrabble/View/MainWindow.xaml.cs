using System.Windows;
using System.Windows.Controls;
using Scrabble.View;
using Scrabble.Model;
using Scrabble.Controller;
using System.Resources;
using System.Globalization;
using System.Threading;
using System;
using Scrabble2018.View;
using System.Windows.Media.Imaging;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace Scrabble
{
    /*
     * Author: https://github.com/poyea
     * Repo: https://github.com/poyea/scrabble
     * April 2019
     * 
     */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Logo_Change(); <-- trying to figure out how to change the logo when the language changes. Still a work in progress -Linh
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int cnt = 0;
            foreach (ComboBox c in Interfaces.Children)
            {
                ComboBoxItem ci = c.SelectedItem as ComboBoxItem;
                if (ci != null && ci.ToString() != "") cnt++;
            }
            if (cnt >= 2)
            {
                GameState.GSInstance.Initialise(cnt);
                int P = 0;
                Game g = new Game(); // Controller
                foreach (ComboBox c in Interfaces.Children)
                {
                    ComboBoxItem ci = c.SelectedItem as ComboBoxItem;
                    if (ci == null) continue;
                    if (ci.Content.ToString() == Scrabble2018.Properties.Skin.DesktopInterface)
                    {
                        DesktopWindow dw = new DesktopWindow(P, g);
                        dw.Show();
                        P++;
                    }
                    else if (ci.Content.ToString() == Scrabble2018.Properties.Skin.TextInterface)
                    {
                        TextWindow tw = new TextWindow(P, g);
                        tw.Show();
                        P++;
                    }
                    else if (ci.Content.ToString() == Scrabble2018.Properties.Skin.MobileInterface)
                    {
                        MobileWindow mw = new MobileWindow(P, g);
                        mw.Show();
                        P++;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(Scrabble2018.Properties.Skin.NeedFriends); //Who doesn't need friends? :)

            }
        }


        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow ab = new AboutWindow();
            ab.ShowDialog();
        }

        public void Lang_Click(object sender, RoutedEventArgs e)
        {
            Scrabble2018.View.Language ln = new Language();
            ln.Show();
            this.Close();
        }
        /*public void Logo_Change() <-- It's so hard :< I don't know why. I want to change the pictures :(
        {

            if (CultureInfo.CurrentUICulture.Name == "ja-JP")
            {
                Logo.Source = new BitmapImage(new Uri("/Image/banner_ja-JP.png", UriKind.Relative));
            }
            else if(CultureInfo.CurrentUICulture.Name == "ko-KR")
            {
                Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/Scrabble;component/Image/banner_ko-KR.png"));

            }
            else if (CultureInfo.CurrentUICulture.Name == "zh-CN")
            {
                Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/Scrabble;component/Image/banner_zh-CN.png"));

            }
            else
            {
                Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/Scrabble;component/Image/banner.png"));

            }

        }*/
    }
}
