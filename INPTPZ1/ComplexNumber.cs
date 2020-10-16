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

            public static readonly ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };

            public ComplexNumber Multiply(ComplexNumber inputNumber)
            {
                // Steps:
                // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
                ComplexNumber thisInstance = this;
                return new ComplexNumber()
                {
                    Real = thisInstance.Real * inputNumber.Real - thisInstance.Imaginary * inputNumber.Imaginary,
                    Imaginary = thisInstance.Real * inputNumber.Imaginary + thisInstance.Imaginary * inputNumber.Real
                };
            }

            public ComplexNumber Add(ComplexNumber InputNumber)
            {
                ComplexNumber thisInstance = this;
                return new ComplexNumber()
                {
                    Real = thisInstance.Real + InputNumber.Real,
                    Imaginary = thisInstance.Imaginary + InputNumber.Imaginary
                };
            }

            public ComplexNumber Subtract(ComplexNumber inputNumber)
            {
                ComplexNumber thisInstance = this;
                return new ComplexNumber()
                {
                    Real = thisInstance.Real - inputNumber.Real,
                    Imaginary = thisInstance.Imaginary - inputNumber.Imaginary
                };
            }

            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }

            internal ComplexNumber Divide(ComplexNumber inputNumber)
            {
                // Steps:
                // (aRe + aIm*i) / (bRe + bIm*i)
                // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
                //  bRe*bRe - bIm*bIm*i*i
                ComplexNumber upperFractionPart = Multiply(new ComplexNumber() { Real = inputNumber.Real, Imaginary = -inputNumber.Imaginary });
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
