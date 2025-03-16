using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement el in MainRoot.Children)
                if (el is Button btn)
                    btn.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)((Button)e.OriginalSource).Content;

            if (OutputWindow.Text == "Error" || OutputWindow.Text == "Infinity" || OutputWindow.Text == "NaN")
            {
                OutputWindow.Text = "0";
            }
            if (str == "AC")
            {
                OutputWindow.Text = "0";
            }
            else if (str == "C")
            {
                if (OutputWindow.Text.Length == 1)
                {
                    OutputWindow.Text = "0";
                }
                else if (OutputWindow.Text.Length > 0)
                {
                    OutputWindow.Text = OutputWindow.Text.Substring(0, OutputWindow.Text.Length - 1);
                }
            }
            else if (str == "=")
            {
                try
                {
                    object result = new DataTable().Compute(OutputWindow.Text, null);

                    string value = Convert.ToDouble(result).ToString(System.Globalization.CultureInfo.InvariantCulture);

                    OutputWindow.Text = value;
                }
                catch (Exception)
                {
                    OutputWindow.Text = "Error";
                }
            }
            else if ((OutputWindow.Text[OutputWindow.Text.Length - 1] == '.' ||
                     OutputWindow.Text[OutputWindow.Text.Length - 1] == '+' ||
                     OutputWindow.Text[OutputWindow.Text.Length - 1] == '-' ||
                     OutputWindow.Text[OutputWindow.Text.Length - 1] == '*' ||
                     OutputWindow.Text[OutputWindow.Text.Length - 1] == '/') &&
                     (str == "." || str == "+" || str == "-" || str == "*" || str == "/"))
            {
                OutputWindow.Text = OutputWindow.Text.Substring(0, OutputWindow.Text.Length - 1) + str;
            }
            else
            {
                if (OutputWindow.Text == "0" && str != "." && str != "+" && str != "-" && str != "*" && str != "/")
                {
                    OutputWindow.Text = str;
                }
                else
                {
                    OutputWindow.Text += str;
                }
            }
            }
        }
    }
