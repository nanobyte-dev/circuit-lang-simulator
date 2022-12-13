using Antlr4.Runtime.Sharpen;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SimulatorGUI
{
    /// <summary>
    /// Interaction logic for CircuitInput.xaml
    /// </summary>
    public partial class CircuitInput : UserControl, INotifyPropertyChanged
    {
        public BitArray Value { get; set; }

        public CircuitInput(BitArray initialValue)
        {
            InitializeComponent();

            Value = initialValue;
            for (int i = Value.Length - 1; i >= 0; i--)
            {
                var button = new ToggleButton()
                {
                    Tag = i,
                    IsChecked = Value.Get(i)
                };
                button.Click += Button_Click;

                toggleContainer.Children.Add(button);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            Value.Set((int)button.Tag, button.IsChecked.HasValue && button.IsChecked.Value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
        }
    }
}
