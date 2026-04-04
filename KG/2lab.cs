using KG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class _2lab : Form
    {
        public int xn, yn, xk, yk;
        Bitmap myBitmap;
        Color currentBorderColor;
        Color currentFillColor;
        Graphics g;
        bool isDashedLine; // Флаг для пунктирной линии

        public _2lab()
        {
            InitializeComponent();
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(myBitmap);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = myBitmap;
            numericUpDownLineWidth.Minimum = 1;
            numericUpDownLineWidth.Maximum = 10;
            numericUpDownLineWidth.Value = 1;
            isDashedLine = false; // По умолчанию сплошная линия
        }
        
        private void ChkDashedLine_CheckedChanged(object sender, EventArgs e)
        {
            isDashedLine = ((CheckBox)sender).Checked;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                int index, numberNodes;
                double xOutput, yOutput, dx, dy;
                int lineWidth = (int)numericUpDownLineWidth.Value;

                xk = e.X;
                yk = e.Y;
                dx = xk - xn;
                dy = yk - yn;
                numberNodes = 200;
                xOutput = xn;
                yOutput = yn;

                if (isDashedLine)
                {
                    // Рисуем пунктирную линию
                    DrawDashedLine(xn, yn, xk, yk, lineWidth);
                }
                else
                {
                    // Рисуем сплошную линию
                    Pen myPen = new Pen(currentBorderColor, lineWidth);
                    for (index = 1; index <= numberNodes; index++)
                    {
                        g.DrawRectangle(myPen, (int)xOutput, (int)yOutput, 2, 2);
                        xOutput = xOutput + dx / numberNodes;
                        yOutput = yOutput + dy / numberNodes;
                    }
                }

                pictureBox1.Image = myBitmap;
            }
        }

        private void DrawDashedLine(int x1, int y1, int x2, int y2, int lineWidth)
        {
            int dashLength = 10; // Длина штриха
            int gapLength = 5;   // Длина промежутка

            double dx = x2 - x1;
            double dy = y2 - y1;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance == 0) return;

            double stepX = dx / distance;
            double stepY = dy / distance;

            double currentX = x1;
            double currentY = y1;
            double remainingDistance = distance;

            Pen myPen = new Pen(currentBorderColor, lineWidth);
            bool draw = true;

            while (remainingDistance > 0)
            {
                double segmentLength = draw ? dashLength : gapLength;
                if (segmentLength > remainingDistance)
                    segmentLength = remainingDistance;

                if (draw)
                {
                    double endX = currentX + stepX * segmentLength;
                    double endY = currentY + stepY * segmentLength;

                    int numberNodes = (int)(segmentLength * 10);
                    if (numberNodes < 1) numberNodes = 1;

                    double subDx = endX - currentX;
                    double subDy = endY - currentY;
                    double xOutput = currentX;
                    double yOutput = currentY;

                    for (int i = 1; i <= numberNodes; i++)
                    {
                        g.DrawRectangle(myPen, (int)xOutput, (int)yOutput, 2, 2);
                        xOutput = xOutput + subDx / numberNodes;
                        yOutput = yOutput + subDy / numberNodes;
                    }
                }

                currentX += stepX * segmentLength;
                currentY += stepY * segmentLength;
                remainingDistance -= segmentLength;
                draw = !draw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = myBitmap;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = colorDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK && radioButton1.Checked)
            {
                currentBorderColor = colorDialog1.Color;
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            if (radioButton1.Checked == true)
            {
                if (isDashedLine)
                {
                    DrawDashedLine(10, 10, 10, 110, (int)numericUpDownLineWidth.Value);
                    DrawDashedLine(10, 10, 110, 10, (int)numericUpDownLineWidth.Value);
                    DrawDashedLine(10, 110, 110, 110, (int)numericUpDownLineWidth.Value);
                    DrawDashedLine(110, 10, 110, 110, (int)numericUpDownLineWidth.Value);

                    DrawDashedLine(150, 10, 150, 200, (int)numericUpDownLineWidth.Value);
                    DrawDashedLine(250, 50, 150, 200, (int)numericUpDownLineWidth.Value);
                    DrawDashedLine(150, 10, 250, 150, (int)numericUpDownLineWidth.Value);
                }
                else
                {
                    CDA(10, 10, 10, 110);
                    CDA(10, 10, 110, 10);
                    CDA(10, 110, 110, 110);
                    CDA(110, 10, 110, 110);

                    CDA(150, 10, 150, 200);
                    CDA(250, 50, 150, 200);
                    CDA(150, 10, 250, 150);
                }
            }
            else
            {
                if (radioButton2.Checked == true)
                {
                    xn = 160;
                    yn = 40;
                    FloodFill(xn, yn);
                }
            }
            pictureBox1.Image = myBitmap;
            button1.Enabled = true;
            button2.Enabled = true;
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
                button1.Enabled = false;
                button2.Enabled = false;
                FloodFill(e.X, e.Y);
                button1.Enabled = true;
                button2.Enabled = true;
                pictureBox1.Image = myBitmap;
            }
        }

        private void CDA(int xStart, int yStart, int xEnd, int yEnd)
        {
            int index, numberNodes;
            double xOutput, yOutput, dx, dy;
            int lineWidth = (int)numericUpDownLineWidth.Value;
            Pen myPen = new Pen(currentBorderColor, lineWidth);

            xn = xStart;
            yn = yStart;
            xk = xEnd;
            yk = yEnd;
            dx = xk - xn;
            dy = yk - yn;
            numberNodes = 200;
            xOutput = xn;
            yOutput = yn;
            for (index = 1; index <= numberNodes; index++)
            {
                g.DrawRectangle(myPen, (int)xOutput, (int)yOutput, 2, 2);
                xOutput = xOutput + dx / numberNodes;
                yOutput = yOutput + dy / numberNodes;
            }

            pictureBox1.Image = myBitmap;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = colorDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                currentFillColor = colorDialog1.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }
    }
}