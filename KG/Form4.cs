using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KG
{
    public partial class Form4 : Form
    {
        public int xn, yn, xk, yk;
        Bitmap myBitmap;
        Color currentBorderColor;
        Color currentFillColor;
        Graphics g;
        SolidBrush Brush;
        private ColorDialog fillColorDialog = new ColorDialog();

        public Form4()
        {
            InitializeComponent();
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(myBitmap);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = myBitmap;
            numericUpDownLineWidth.Minimum = 1; 
            numericUpDownLineWidth.Maximum = 20; 
            numericUpDownLineWidth.Value = 1; 
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                xk = e.X;
                yk = e.Y;
                NesimCDA(xn, yn, xk, yk);
                pictureBox1.Image = myBitmap;
            }
        }

        private void NesimCDA(int xStart, int yStart, int xEnd, int yEnd)
        {
            double xOutput, yOutput, Px, Py;
            xn = xStart;
            yn = yStart;
            xk = xEnd;
            yk = yEnd;
            Px = xk - xn;
            Py = yk - yn;
            xOutput = xn;
            yOutput = yn;
            int lineWidth = (int)numericUpDownLineWidth.Value; // Получаем толщину линии
            Pen myPen = new Pen(currentBorderColor, lineWidth); // Создаем перо с выбранной толщиной
            int Xstep = (xk > xn) ? 1 : (xk < xn) ? -1 : 0;
            int Ystep = (yk > yn) ? 1 : (yk < yn) ? -1 : 0;

            if (Math.Abs(Px) >= Math.Abs(Py))
            {
                while ((Xstep == 1 && xOutput <= xk) || (Xstep == -1 && xOutput >= xk))
                {
                    // Рисуем линию с учетом толщины
                    g.DrawRectangle(myPen, (int)xOutput, (int)yOutput, lineWidth, lineWidth);
                    xOutput += Xstep;
                    yOutput += (Py / Px) * Xstep;
                }
            }
            else if (Math.Abs(Py) > Math.Abs(Px))
            {
                while ((Ystep == 1 && yOutput <= yk) || (Ystep == -1 && yOutput >= yk))
                {
                    // Рисуем линию с учетом толщины
                    g.DrawRectangle(myPen, (int)xOutput, (int)yOutput, lineWidth, lineWidth);
                    xOutput += (Px / Py) * Ystep;
                    yOutput += Ystep;
                }
            }

            pictureBox1.Image = myBitmap; // Обновляем изображение
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void FloodFill(int x1, int y1)
        {
            Color oldPixelColor = myBitmap.GetPixel(x1, y1);
            if ((oldPixelColor.ToArgb() != currentBorderColor.ToArgb())
           && (oldPixelColor.ToArgb() != currentFillColor.ToArgb()))
            {
                myBitmap.SetPixel(x1, y1, currentFillColor);
                FloodFill(x1 + 1, y1);
                FloodFill(x1 - 1, y1);
                FloodFill(x1, y1 + 1);
                FloodFill(x1, y1 - 1);
            }
            else
            {
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void _2lab_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                xn = e.X;
                yn = e.Y;
            }

            else
            {
                FloodFill(e.X, e.Y);
                pictureBox1.Image = myBitmap;
            }
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = myBitmap;
        }

        private void btnColorLine_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = colorDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK && radioButton1.Checked)
            {
                currentBorderColor = colorDialog1.Color;
            }
        }

        private void btnColorFill_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = fillColorDialog.ShowDialog(); // Используем отдельный ColorDialog
            if (dialogResult == DialogResult.OK)
            {
                currentFillColor = fillColorDialog.Color; // Сохраняем выбранный цвет заливки
            }
        }
    }
}