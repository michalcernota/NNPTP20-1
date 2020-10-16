using System.Collections.Generic;

namespace INPTPZ1
{
    namespace Mathematics
    {
        class Poly
        {
            public List<ComplexNumber> Coefficients { get; set; }

            public Poly() => Coefficients = new List<ComplexNumber>();

            public Poly Derivate()
            {
                Poly poly = new Poly();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    poly.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
                }

                return poly;
            }

            public ComplexNumber Evaluate(ComplexNumber inputNumber)
            {
                ComplexNumber result = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber currentCoefficient = Coefficients[i];
                    ComplexNumber powerResult = inputNumber;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                        {
                            powerResult = powerResult.Multiply(inputNumber);
                        }

                        currentCoefficient = currentCoefficient.Multiply(powerResult);
                    }

                    result = result.Add(currentCoefficient);
                }

                return result;
            }

            public override string ToString()
            {
                string result = "";
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    result += Coefficients[i];
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            result += "x";
                        }
                    }
                    result += " + ";
                }
                return result;
            }
        }
    }
}
