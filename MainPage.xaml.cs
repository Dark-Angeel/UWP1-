using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP1_
{
    public sealed partial class MainPage : Page
    {
        private TextBox operand1TextBox;
        private TextBox operand2TextBox;
        private RadioButton decimalRadioButton;
        private RadioButton hexRadioButton;
        private RadioButton octalRadioButton;
        private RadioButton binaryRadioButton;
        private TextBlock resultTextBlock;

        public MainPage()
        {
            this.InitializeComponent();
            CreateUI();
        }

        private void CreateUI()
        {
            operand1TextBox = new TextBox { PlaceholderText = "Unesite prvi broj", Margin = new Thickness(5) };
            operand2TextBox = new TextBox { PlaceholderText = "Unesite drugi broj", Margin = new Thickness(5) };

            StackPanel buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
            Button addButton = new Button { Content = "+", Margin = new Thickness(5) };
            Button subtractButton = new Button { Content = "-", Margin = new Thickness(5) };
            Button multiplyButton = new Button { Content = "*", Margin = new Thickness(5) };
            Button divideButton = new Button { Content = "/", Margin = new Thickness(5) };
            addButton.Click += DodajButton_Click;
            subtractButton.Click += OduzmiButton_Click;
            multiplyButton.Click += PomnožiButton_Click;
            divideButton.Click += PodijeliButton_Click;
            buttonPanel.Children.Add(addButton);
            buttonPanel.Children.Add(subtractButton);
            buttonPanel.Children.Add(multiplyButton);
            buttonPanel.Children.Add(divideButton);

            decimalRadioButton = new RadioButton { Content = "Decimalni", IsChecked = true };
            hexRadioButton = new RadioButton { Content = "Heksadecimalni" };
            octalRadioButton = new RadioButton { Content = "Oktalni" };
            binaryRadioButton = new RadioButton { Content = "Binarni" };

            resultTextBlock = new TextBlock { Margin = new Thickness(5) };

            StackPanel mainPanel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            mainPanel.Children.Add(operand1TextBox);
            mainPanel.Children.Add(operand2TextBox);
            mainPanel.Children.Add(buttonPanel);
            mainPanel.Children.Add(decimalRadioButton);
            mainPanel.Children.Add(hexRadioButton);
            mainPanel.Children.Add(octalRadioButton);
            mainPanel.Children.Add(binaryRadioButton);
            mainPanel.Children.Add(resultTextBlock);

            this.Content = mainPanel;
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            Izračunaj((x, y) => x + y);
        }

        private void OduzmiButton_Click(object sender, RoutedEventArgs e)
        {
            Izračunaj((x, y) => x - y);
        }

        private void PomnožiButton_Click(object sender, RoutedEventArgs e)
        {
            Izračunaj((x, y) => x * y);
        }

        private void PodijeliButton_Click(object sender, RoutedEventArgs e)
        {
            Izračunaj((x, y) => x / y);
        }

        private void Izračunaj(Func<int, int, int> operacija)
        {
            if (JeIspravanUnos(operand1TextBox.Text) && JeIspravanUnos(operand2TextBox.Text))
            {
                int operand1 = ParsirajUnos(operand1TextBox.Text);
                int operand2 = ParsirajUnos(operand2TextBox.Text);

                int rezultat = operacija(operand1, operand2);
                PrikaziRezultat(rezultat);
            }
            else
            {
                resultTextBlock.Text = "Neispravan unos!";
            }
        }

        private void PrikaziRezultat(int rezultat)
        {
            if (decimalRadioButton.IsChecked == true)
            {
                resultTextBlock.Text = rezultat.ToString();
            }
            else if (hexRadioButton.IsChecked == true)
            {
                resultTextBlock.Text = rezultat.ToString("X");
            }
            else if (octalRadioButton.IsChecked == true)
            {
                resultTextBlock.Text = Convert.ToString(rezultat, 8);
            }
            else //provjera
            {
                resultTextBlock.Text = Convert.ToString(rezultat, 2);
            }
        }

        private bool JeIspravanUnos(string unos)
        {
            // unesene znamke
            char[] prihvatljiveZnamenke;

            if (decimalRadioButton.IsChecked == true)
                prihvatljiveZnamenke = "0123456789".ToCharArray();
            else if (hexRadioButton.IsChecked == true)
                prihvatljiveZnamenke = "0123456789ABCDEFabcdef".ToCharArray();
            else if (octalRadioButton.IsChecked == true)
                prihvatljiveZnamenke = "01234567".ToCharArray();
            else //provjera
                prihvatljiveZnamenke = "01".ToCharArray();

            foreach (char znak in unos)
            {
                if (Array.IndexOf(prihvatljiveZnamenke, znak) == -1)
                    return false;
            }
            return true;
        }

        private int ParsirajUnos(string unos)
        {
            //pretvorba
            if (decimalRadioButton.IsChecked == true)
                return int.Parse(unos);
            else if (hexRadioButton.IsChecked == true)
                return Convert.ToInt32(unos, 16);
            else if (octalRadioButton.IsChecked == true)
                return Convert.ToInt32(unos, 8);
            else // provjera
                return Convert.ToInt32(unos, 2);
        }
    }
}
