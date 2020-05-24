using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Todo: Aufklappbare Sidebar einbauen für erweiterte Funktionen wie x² oder Konstanwert für Pi.

        private double _Result { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void BtnClicked(object sender, RoutedEventArgs e)
        {
            //Todo: Eventuell die Auswertung der Buttons anders lösen
            //weil nicht sehr schön

            Button btn = (Button)sender;

            int num;

            if (int.TryParse(btn.Content.ToString(), out num))
            {
                AddNumberToDisplay(num);
            }
            else if (btn.Content.ToString() == "+" || btn.Content.ToString() == "-" ||
                btn.Content.ToString() == "x" || btn.Content.ToString() == "/")
            {
                AddOperatorToDisplay(btn.Content.ToString());
            }
            else if(btn.Content.ToString() == "=")
            {
                string msg = CalculateResult();

                if (msg == String.Empty)
                    DisplayResult();
                else
                    DisplayResult(msg);
            }
            else if(btn.Content.ToString() == "C")
            {
                ClearDisplay();
            }
            else if(btn.Content.ToString() == "CE")
            {
                ClearLastEntry();
            }
            else if(btn.Content.ToString() == ",")
            {
                AddComma();
            }
            else if(btn.Content.ToString() == "%")
            {
                //Todo: Funktionalität für Prozent einfügen
            }
        }

        private void AddComma()
        {
            //Todo: Checken ob komma an erster Stelle dan soll führende 0 
            //eingefügt werden

            CheckLblIfNull();

            this.lblDisplay.Content += ",";
        }

        private void AddNumberToDisplay(int num)
        {
            if (this.lblDisplay == null)
                return;

            this.lblDisplay.Content += num.ToString(); 
        }

        private void AddOperatorToDisplay(string op)
        {
            //Todo: Checken if Zahl in Display funktioniert nicht!

            if (this.lblDisplay == null && this.lblDisplay.Content.ToString().Length == 0)
                return;

            this.lblDisplay.Content = this.lblDisplay.Content + " " + op + " ";
        }

        private void ClearDisplay()
        {
            if (CheckLblIfNull())
                return;

            this.lblDisplay.Content = String.Empty;
        }

        private void ClearLastEntry()
        {
            if (CheckLblIfNull())
                return;

            string str = this.lblDisplay.Content.ToString();

            if (str[str.Length - 1] == ' ')
                this.lblDisplay.Content = str.Remove(str.Length - 3);
            else
                this.lblDisplay.Content = str.Remove(str.Length - 1);
        }

        private bool CheckLblIfNull()
        {
            if (this.lblDisplay == null || this.lblDisplay.Content == null)
                return true;

            return false;
        }

        private string CalculateResult()
        {
            //Todo: Mehrere Rechenvorgänge in einem ermöglichen (Punkt vor Strich beachten sowie Klammern!!)

            if (CheckLblIfNull())
                return String.Empty; ;

            string[] formula = this.lblDisplay.Content.ToString().Split(' ');

            BasicCalculation.Opperands op = BasicCalculation.Opperands.None;

            switch(formula[1])
            {
                case "+":
                    op = BasicCalculation.Opperands.Plus;
                    break;
                case "-":
                    op = BasicCalculation.Opperands.Minus;
                    break;
                case "/":
                    op = BasicCalculation.Opperands.Divide;
                    break;
                case "x":
                    op = BasicCalculation.Opperands.Multiply;
                    break;
            }

            if (op == BasicCalculation.Opperands.Divide)
            {
                if (double.Parse(formula[0]) == 0 || double.Parse(formula[2]) == 0)
                {
                    return "Div / 0";
                }
            }

            BasicCalculation basicCalculation = new BasicCalculation(double.Parse(formula[0]),
                double.Parse(formula[2]), op);

            this._Result = basicCalculation.GetResult();

            return string.Empty;
        }

        private void DisplayResult()
        {
            ClearDisplay();
            this.lblDisplay.Content = this._Result;
        }

        private void DisplayResult(string msg)
        {
            ClearDisplay();
            this.lblDisplay.Content = msg;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            //Todo: Keybord Eingabe fertig stellen (Shift und Num Pad!)

            if (!Keyboard.IsKeyDown(Key.LeftShift) || !Keyboard.IsKeyDown(Key.RightShift))
            {
                if (e.Key == Key.Return)
                {
                    string msg = CalculateResult();

                    if (msg == String.Empty)
                        DisplayResult();
                    else
                        DisplayResult(msg);
                }
                else if (e.Key == Key.Delete)
                {
                    ClearDisplay();
                }
                else if (e.Key == Key.Back)
                {
                    ClearLastEntry();
                }
                else if (e.Key == Key.Decimal)
                {
                    AddComma();
                }
                else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
                {
                    AddNumberToDisplay(int.Parse(new KeyConverter().ConvertToString(e.Key)));
                }
                else if (e.Key == Key.Add || e.Key == Key.OemPlus)
                {
                    AddOperatorToDisplay("+");
                }
                else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
                {
                    AddOperatorToDisplay("-");
                }
                else if (e.Key == Key.Divide)
                {
                    AddOperatorToDisplay("/");
                }
                else if (e.Key == Key.Multiply)
                {
                    AddOperatorToDisplay("x");
                }
            }
            else
            {

            }
        }
    }
}
