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
                    if (ci.Content.ToString() == "Desktop")
                    {
                        DesktopWindow dw = new DesktopWindow(P, g);
                        dw.Show();
                        P++;
                    }
                    else if (ci.Content.ToString() == "Text")
                    {
                        TextWindow tw = new TextWindow(P, g);
                        tw.Show();
                        P++;
                    }
                    else if (ci.Content.ToString() == "Mobile")
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
                MessageBox.Show(Scrabble2018.Properties.Skin.NeedFriends);
            }
        }


        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow ab = new AboutWindow();
            ab.ShowDialog();
        }

        /*private void English_Click(object sender, RoutedEventArgs e)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            CultureInfo _info = CultureInfo.CreateSpecificCulture("en-US");
            //English
            Thread.CurrentThread.CurrentCulture = _info;
            Thread.CurrentThread.CurrentUICulture = _info;
        }
        private void Japanese_Click(object sender, RoutedEventArgs e)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            CultureInfo _info = CultureInfo.CreateSpecificCulture("ja_JP");
            //Japanese
            Thread.CurrentThread.CurrentCulture = _info;
            Thread.CurrentThread.CurrentUICulture = _info;
        }*/

        public void Lang_Click(object sender, RoutedEventArgs e)
        {
            Scrabble2018.View.Language ln = new Language();
            ln.Show();
            this.Close();
        }
    }
}
