using System;
using System.Drawing;
using System.Windows.Forms;

namespace MandelbrotSet
{
    public partial class Mandelbrot : Form
    {
        public Mandelbrot()
        {
            InitializeComponent();
        }

        public int maxIterations;
        public const double limit = 1e10;

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.Initialize(display);
            Engine.Refresh();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(this.textBox1.Text, out maxIterations))
            {
                maxIterations = 0;
                MessageBox.Show("Please write a number.");
            }
            if (comboBox1.SelectedIndex == 0)
                DrawMandelBW(display);
            else if (comboBox1.SelectedIndex == 1)
                DrawMandelColor(display);
        }

        private void DrawMandelBW(PictureBox display)
        {
            for (int x = 0; x < display.Width; x++)
            {
                for (int y = 0; y < display.Height; y++)
                {
                    double re = -2.0 + (3.0 * x / display.Width);
                    double im = 2.0 - (4.0 * y / display.Height);
                    Cplx c = new Cplx(re, im1, im2);

                    Cplx z = new Cplx(0, 0, 0);
                    int iterations = 0;
                    while (z.Norm() < limit && iterations < maxIterations)
                    {
                        z = z * z + c;
                        iterations++;
                    }

                    Color color = (z.Norm() < limit) ? Color.Black : Color.White;
                    Engine.bmp.SetPixel(x, y, color);
                }
            }
            Engine.Refresh();
        }

        private void DrawMandelColor(PictureBox display)
        {
            //MethodOneRGB(display);
            //MethodTwoRGB(display);
            MethodThreeHSL(display);
        }

        private void MethodOneRGB(PictureBox display)
        {
            for (int x = 0; x < display.Width; x++)
            {
                for (int y = 0; y < display.Height; y++)
                {
                    double re = -1.5 + (2.0 * x / display.Width);
                    double im1 = 1.0 - (2.0 * y / display.Height);
                    double im2= 1.0 - (2.0 * y / display.Height); //de verificat care e valoarea dorita
                    Cplx c = new Cplx(re, im1, im2);

                    Cplx z = new Cplx(0, 0, 0);
                    int iterations = 0;
                    while (z.Norm() < limit && iterations < maxIterations)
                    {
                        z = z * z + c;
                        iterations++;
                    }

                    Color color = Color.Black;
                    if (iterations == maxIterations || z.Norm() >= limit)
                    {
                        int colorValue = (int)((double)iterations / (double)maxIterations * 255.0);
                        color = Color.FromArgb(colorValue, colorValue, colorValue);
                    }
                    Engine.bmp.SetPixel(x, y, color);
                }
            }
            Engine.Refresh();
        }

        private void MethodTwoRGB(PictureBox display)
        {
            for (int x = 0; x < display.Width; x++)
            {
                for (int y = 0; y < display.Height; y++)
                {
                    double re = -2.0 + (3.0 * x / display.Width);
                    double im1 = 2.0 - (4.0 * y / display.Height);
                    double im2 = 2.0 - (4.0 * y / display.Height); //de verificat care e valoarea dorita
                    Cplx c = new Cplx(re, im1,im2);

                    Cplx z = new Cplx(0, 0,0);
                    int iterations = 0;
                    while (z.Norm() < limit && iterations < maxIterations)
                    {
                        z = z * z + c;
                        iterations++;
                    }

                    int r = (int)((iterations % 16) * 16);
                    int g = (int)((iterations % 32) * 8);
                    int b = (int)((iterations % 64) * 4);

                    Color color = (iterations >= maxIterations) ? Color.Black : Color.FromArgb(r, g, b);
                    Engine.bmp.SetPixel(x, y, color);
                }
            }
            Engine.Refresh();
        }

        private void MethodThreeHSL(PictureBox display)
        {

            for (int x = 0; x < display.Width; x++)
            {
                for (int y = 0; y < display.Height; y++)
                {
                    double re = -2.0 + (3.0 * x / display.Width);
                    double im1 = 2.0 - (4.0 * y / display.Height);
                    double im2 = 2.0 - (4.0 * y / display.Height); //de verificat care e valoarea dorita
                    Cplx c = new Cplx(re, im1,im2);

                    Cplx z = new Cplx(0, 0, 0);
                    int iterations = 0;
                    while (z.Norm() < limit && iterations < maxIterations)
                    {
                        z = z * z + c;
                        iterations++;
                    }

                    Color color;
                    if (iterations == maxIterations)
                    {
                        color = Color.Black;
                    }
                    else
                    {
                        // Use HSL color space to create a gradient based on the number of iterations
                        double hue = 360 * iterations / (double)maxIterations;
                        double saturation = 1.0;
                        double lightness = 0.5;
                        color = FromHsl(hue, saturation, lightness);
                    }

                    Engine.bmp.SetPixel(x, y, color);
                }
            }
            Engine.Refresh();
        }

        private Color FromHsl(double hue, double saturation, double lightness) //https://en.wikipedia.org/wiki/HSL_and_HSV#From_HSL
        {
            double chroma = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            double hPrime = hue / 60.0;
            double x = chroma * (1 - Math.Abs(hPrime % 2 - 1));
            double r1, g1, b1;

            if (hPrime >= 0 && hPrime <= 1)
            {
                r1 = chroma;
                g1 = x;
                b1 = 0;
            }
            else if (hPrime > 1 && hPrime <= 2)
            {
                r1 = x;
                g1 = chroma;
                b1 = 0;
            }
            else if (hPrime > 2 && hPrime <= 3)
            {
                r1 = 0;
                g1 = chroma;
                b1 = x;
            }
            else if (hPrime > 3 && hPrime <= 4)
            {
                r1 = 0;
                g1 = x;
                b1 = chroma;
            }
            else if (hPrime > 4 && hPrime <= 5)
            {
                r1 = x;
                g1 = 0;
                b1 = chroma;
            }
            else
            {
                r1 = chroma;
                g1 = 0;
                b1 = x;
            }

            double m = lightness - 0.5 * chroma;
            int r = (int)((r1 + m) * 255);
            int g = (int)((g1 + m) * 255);
            int b = (int)((b1 + m) * 255);

            return Color.FromArgb(r, g, b);
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            Engine.Clear();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "PNG Image|*.png";
            saveFile.Title = "Save an Image File";
            saveFile.ShowDialog();
            display.Image.Save(saveFile.FileName, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
