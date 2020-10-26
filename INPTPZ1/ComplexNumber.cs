namespace INPTPZ1
{

    namespace Mathematics
    {
        public class ComplexNumber
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }

            public override bool Equals(object inputObject)
            {
                if (inputObject is ComplexNumber inputComplexNumber)
                {
                    return inputComplexNumber.Real == Real && inputComplexNumber.Imaginary == Imaginary;
                }
                return base.Equals(inputObject);
            }

            public override int GetHashCode()
            {
                int hashCode = -837395861;
                hashCode = hashCode * -1521134295 + Real.GetHashCode();
                hashCode = hashCode * -1521134295 + Imaginary.GetHashCode();
                return hashCode;
            }

            public static readonly ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };

            public ComplexNumber Multiply(ComplexNumber multiplier)
            {
                ComplexNumber currentInstance = this;
                return new ComplexNumber()
                {
                    Real = currentInstance.Real * multiplier.Real - currentInstance.Imaginary * multiplier.Imaginary,
                    Imaginary = currentInstance.Real * multiplier.Imaginary + currentInstance.Imaginary * multiplier.Real
                };
            }

            public ComplexNumber Add(ComplexNumber adder)
            {
                ComplexNumber currentInstance = this;
                return new ComplexNumber()
                {
                    Real = currentInstance.Real + adder.Real,
                    Imaginary = currentInstance.Imaginary + adder.Imaginary
                };
            }

            public ComplexNumber Subtract(ComplexNumber reducer)
            {
                ComplexNumber currentInstance = this;
                return new ComplexNumber()
                {
                    Real = currentInstance.Real - reducer.Real,
                    Imaginary = currentInstance.Imaginary - reducer.Imaginary
                };
            }

            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }

            public ComplexNumber Divide(ComplexNumber inputNumber)
            {
                ComplexNumber upperFractionPart = Multiply(new ComplexNumber() { 
                    Real = inputNumber.Real,
                    Imaginary = inputNumber.Imaginary * -1 
                });

                double lowerFractionPart = (inputNumber.Real * inputNumber.Real) + (inputNumber.Imaginary * inputNumber.Imaginary);

                return new ComplexNumber()
                {
                    Real = upperFractionPart.Real / lowerFractionPart,
                    Imaginary = upperFractionPart.Imaginary / lowerFractionPart
                };
            }
        }
    }
}
