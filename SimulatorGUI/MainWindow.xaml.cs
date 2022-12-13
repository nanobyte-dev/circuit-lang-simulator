using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CircuitLangSimulator;
using CircuitLangSimulator.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.XPath;

namespace SimulatorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Program program;
        Module mainModule;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "CircuitLang file|*.cl";
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                using (var source = File.OpenRead(dialog.FileName))
                {
                    ICharStream stream = CharStreams.fromStream(source);
                    ITokenSource lexer = new CircuitLangLexer(stream);
                    ITokenStream tokens = new CommonTokenStream(lexer);
                    CircuitLangParser parser = new CircuitLangParser(tokens);
                    parser.BuildParseTree = true;
                    IParseTree tree = parser.program();

                    var visitor = new CircuitLangModelGenerator();
                    program = (Program)visitor.Visit(tree);

                    mainModule = program.Modules.FirstOrDefault(x => x.IsMain);
                    if (mainModule == null)
                    {
                        MessageBox.Show("Program doesn't have a main module!");
                        return;
                    }

                    ListInputs.Children.Clear();
                    foreach (VariableReference input in mainModule.Inputs)
                    {
                        var control = new CircuitInput(new System.Collections.BitArray(input.Index))
                        {
                            Name = input.VariableName
                        };
                        control.PropertyChanged += InputValueChanged;
                        ListInputs.Children.Add(control);
                    }

                    ListOutputs.Children.Clear();
                    foreach (VariableReference output in mainModule.Outputs)
                    {
                        var control = new CircuitOutput(new System.Collections.BitArray(output.Index))
                        {
                            Name = output.VariableName
                        };
                        ListOutputs.Children.Add(control);
                    }
                }
            }
        }

        private void InputValueChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }
    }
}
