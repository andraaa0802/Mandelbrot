using System;

namespace MandelbrotSet
{
    //complex numbers with 2 imaginary parts
    public class Cplx
    {
        private double re;
        private double im1;
        private double im2;

        public Cplx() : this(0, 0, 0)
        {
        }

        public Cplx(double re, double im1, double im2)
        {
            this.re = re;
            this.im1 = im1;
            this.im2 = im2;
        }

        public static Cplx operator +(Cplx a, Cplx b)
        {
            Cplx t = new Cplx
            {
                re = a.re + b.re,
                im1 = a.im1 + b.im1
                im2 = a.im2 + b.im2
            };
            return t;
        }

        public static Cplx operator -(Cplx a, Cplx b)
        {
            Cplx t = new Cplx
            {
                re = a.re - b.re,
                im1 = a.im1 - b.im1
                im2 = a.im2 - b.im2
            };
            return t;
        }

        public static Cplx operator *(Cplx a, Cplx b)
        {
            Cplx t = new Cplx
            {
                re = a.re * b.re - a.im1 * b.im1 - a.im2 * b.im2,
                im1 = a.re * b.im1 + a.im1 * b.re + a.im2 * b.im1,
                im2 = a.re * b.im2 + a.im1 * b.im2 + a.im2 * b.re
            };
            return t;
        }

        public static Cplx operator /(Cplx a, Cplx b)
        {
            Cplx c = b.Conjugate();
            Cplx up = a * c;
            Cplx down = b * c;
            double num = down.re;
            Cplx t = new Cplx
            {
                re = up.re / num,
                im1 = up.im1 / num,
                im2 = up.im2 / num
            };
            return t;
        }

        public Cplx Conjugate()
        {
            Cplx tmp = new Cplx
            {
                re = re,
                im1 = -1 * im1
                im2 = -1 * im2
            };
            return tmp;
        }

        public double Norm()
        {
            return Math.Sqrt(re * re + im1 * im1 + im2 * im2);
        }

        public string View()
        {
            if (im1 == 0 && im2 == 0)
                return re.ToString("0.00");

            string cplxStr = "";
            if (im1 != 0)
            {
                if (im1 > 0)
                    cplxStr += "+";
                cplxStr += "i" + im1.ToString("0.00");
            }

            if (im2 != 0)
            {
                if (im2 > 0)
                    cplxStr += "+";
                cplxStr += "j" + im2.ToString("0.00");
            }

            return re.ToString("0.00") + cplxStr;
        }
    }
}
