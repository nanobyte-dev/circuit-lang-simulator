using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimulatorGUI
{
    /// <summary>
    /// Interaction logic for CircuitOutput.xaml
    /// </summary>
    public partial class CircuitOutput : UserControl
    {
        public BitArray Value { get; set; }

        public CircuitOutput(BitArray initialValue)
        {
            InitializeComponent();

            Value = initialValue;
            for (int i = Value.Count - 1; i >= 0; i--)
            {
                var button = new Ellipse()
                {
                    Tag = i,
                    Fill = Value.Get(i) ? Brushes.Green : Brushes.Red
                };
                toggleContainer.Children.Add(button);
            }
        }

        public void UpdateValues()
        {
            foreach (var child in toggleContainer.Children.Cast<Ellipse>())
            {
                child.Fill = Value.Get((int)child.Tag) ? Brushes.Green : Brushes.Red;
            }
        }
    }
}
