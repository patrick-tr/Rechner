using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class BasicCalculation
    {
        public enum Opperands
        {
            None = 0,
            Plus,
            Minus,
            Divide,
            Multiply
        };

        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public Opperands Opperand { get; set; }
        public double Result
        {

            get
            {
                switch (this.Opperand)
                {
                    case Opperands.None:
                        return 0;
                    case Opperands.Plus:
                        return Value1 + Value2;
                    case Opperands.Minus:
                        return Value1 - Value2;
                    case Opperands.Multiply:
                        return Value1 * Value2;
                    case Opperands.Divide:
                        return Value1 / Value2;
                }

                return 0;
            }
            private set
            {
                if (value > double.MaxValue || value < double.MinValue)
                    throw new Exception("Value to big for datatype double");

                this.Result = value;
            }
        }

        public BasicCalculation(double value1, double value2, Opperands opperand)
        {
            this.Value1 = value1;
            this.Value2 = value2;
            this.Opperand = opperand;
        }

        public double GetResult()
        {
            return this.Result;
        }
    }
}
