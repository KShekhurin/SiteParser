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
using System.Net;
using System.IO;
using System.Xml;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace SimpleParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, IParser> parsers = new Dictionary<string, IParser>(); 
        string selectedChannel = null;
        public MainWindow()
        {
            InitializeComponent();

            parsers.Add("Bash", new ParserBashRandom());
            parsers.Add("ITHappens", new ParserITHappensRandom());
            parsers.Add("Zadolbali", new ParserZadolbaliRandom());
        }

        async private void GetRandomePhrase(object sender, RoutedEventArgs e)
        {
            if (selectedChannel != null)
            {
                BtnGetRandomPhrase.IsEnabled = false;
                informLabel.Visibility = Visibility.Visible;
                await Task.Run(() => GetPhrase());
                BtnGetRandomPhrase.IsEnabled = true;
                informLabel.Visibility = Visibility.Hidden;
            }
        }

        void GetPhrase()
        {
            IParser parser = new ParserBashRandom();
            Dispatcher.Invoke(() => parser = parsers[selectedChannel]);
            while (true)
            {
                Info info = parser.Parse().Result;

                string text = "";
                Dispatcher.Invoke(() => text = TbContaner.Text);

                if (text != info.text)
                {
                    Dispatcher.Invoke(() => TbContaner.Text = info.text);
                    break;
                } 
            }
        }

        private void SaveThisPhrase(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Text file | *.txt";

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, TbContaner.Text);
            }
        }

        private void ChangeChannel(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            selectedChannel = selectedItem.Content.ToString();
        }
    }
}
