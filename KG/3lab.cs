using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace KG
{
    public partial class _3lab : Form
    {
        bool f = true;
        int k, l; // элементы матрицы сдвига
        int[,] kv = new int[4, 3]; // матрица тела
        int[,] osi = new int[4, 3]; // матрица координат осей
        int[,] matr_sdv = new int[3, 3]; //матрица преобразования
        private int rotationAngle = 0;

        public _3lab()
        {
            InitializeComponent();
        }

        private void DrawStaticAxes()
        {
            // Оси будут рисоваться относительно центра pictureBox
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            Pen axisPen = new Pen(Color.Red, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            // Рисуем горизонтальную ось X
            g.DrawLine(axisPen, 0, centerY, pictureBox1.Width, centerY);

            // Рисуем вертикальную ось Y
            g.DrawLine(axisPen, centerX, 0, centerX, pictureBox1.Height);

            g.Dispose();
            axisPen.Dispose();
        }

        private void DrawFigureAndStaticAxes()
        {
            ClearDrawing(); // Очищаем поле
            DrawStaticAxes(); // Рисуем статичные оси поверх фигуры
        }
        private void ClearDrawing()
        {
            pictureBox1.Image = null;
            pictureBox1.Refresh();
        }
        private void Init_figure()
        {
            kv[0, 0] = -60; kv[0, 1] = 43; kv[0, 2] = 1;   // первая точка
            kv[1, 0] = 1; kv[1, 1] = -105; kv[1, 2] = 1;   // вторая точка  
            kv[2, 0] = -1; kv[2, 1] = 18; kv[2, 2] = 1;    // третья точка
            kv[3, 0] = 61; kv[3, 1] = 45; kv[3, 2] = 1;    // четвертая точка
        }
        private void Draw_Kv()
        {
            Init_figure();
            Init_matr_preob(k, l);
            int[,] kv1 = Multiply_matr(kv, matr_sdv);

            Pen myPen = new Pen(Color.Black, 2);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[1, 0], kv1[1, 1]);  // точка 0 -> точка 1
            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[2, 0], kv1[2, 1]);  // точка 0 -> точка 2
            g.DrawLine(myPen, kv1[2, 0], kv1[2, 1], kv1[3, 0], kv1[3, 1]);  // точка 2 -> точка 3
            g.DrawLine(myPen, kv1[1, 0], kv1[1, 1], kv1[3, 0], kv1[3, 1]);  // точка 1 -> точка 3

            g.Dispose();
            myPen.Dispose();
        }
        private void Init_matr_preob(int k1, int l1)
        {
            matr_sdv[0, 0] = 1; matr_sdv[0, 1] = 0; matr_sdv[0, 2] = 0;
            matr_sdv[1, 0] = 0; matr_sdv[1, 1] = 1; matr_sdv[1, 2] = 0;
            matr_sdv[2, 0] = k1; matr_sdv[2, 1] = l1; matr_sdv[2, 2] = 1;
        }

        private void Init_osi()
        {
            osi[0, 0] = -200; osi[0, 1] = 0; osi[0, 2] = 1;
            osi[1, 0] = 200; osi[1, 1] = 0; osi[1, 2] = 1;
            osi[2, 0] = 0; osi[2, 1] = 200; osi[2, 2] = 1;
            osi[3, 0] = 0; osi[3, 1] = -200; osi[3, 2] = 1;

        }
        private int[,] Multiply_matr(int[,] a, int[,] b)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            int[,] r = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    r[i, j] = 0;
                    for (int ii = 0; ii < m; ii++)
                    {
                        r[i, j] += a[i, ii] * b[ii, j];
                    }
                }
            }
            return r;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //середина pictureBox
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            Draw_Kv();

        }
        private void Draw_osi()
        {
            Init_osi();
            Init_matr_preob(k, l);
            int[,] osi1 = Multiply_matr(osi, matr_sdv);

            Pen myPen = new Pen(Color.Red, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(myPen, osi1[0, 0], osi1[0, 1], osi1[1, 0], osi1[1, 1]);
            g.DrawLine(myPen, osi1[2, 0], osi1[2, 1], osi1[3, 0], osi1[3, 1]);

            g.Dispose();
            myPen.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            Draw_osi();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;

            button8.Text = "Стоп";
            if (f == true)
            {
                timer1.Start();

            }
            else
            {
                timer1.Stop();
                button8.Text = "Старт";
            }
            f = !f;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            k -= 5;
            Draw_Kv();

        }
        private void button4_Click(object sender, EventArgs e)
        {

            ClearDrawing();
            DrawStaticAxes();
            k += 5;
            Draw_Kv();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            k++;
            ClearDrawing();
            DrawStaticAxes();
            Draw_Kv();
            Thread.Sleep(100);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            l += 5;
            Draw_Kv();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            l -= 5;
            Draw_Kv();

        }
        //Масштабирование  фигуры на плоскости
        private void button9_Click(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes();
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Введите число!", "Внимание");
            }
            else
            {
                if (double.Parse(textBox1.Text.ToString()) < 0)
                {
                    MessageBox.Show("Число не может быть меньше 0", "");
                    textBox1.Clear();
                }
                else
                {
                    double n = double.Parse(textBox1.Text);

                    // Матрица масштабирования
                    double[,] scaleMatrix = new double[3, 3] {
                    { n, 0, 0 },
                    { 0, n, 0 },
                    { 0, 0, 1 }
                };

                    Init_figure(); // Инициализация фигуры (4 точки)

                    // Применяем масштабирование к каждой вершине (без сдвига)
                    double[,] temp = new double[4, 3];
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            temp[i, j] = 0;
                            for (int m = 0; m < 3; m++)
                            {
                                temp[i, j] += kv[i, m] * scaleMatrix[m, j];
                            }
                        }
                    }

                    // Применяем сдвиг к отмасштабированным координатам
                    int[,] scaledKv = new int[4, 3];
                    for (int i = 0; i < 4; i++)
                    {
                        scaledKv[i, 0] = (int)Math.Round(temp[i, 0] + k);
                        scaledKv[i, 1] = (int)Math.Round(temp[i, 1] + l);
                        scaledKv[i, 2] = 1;
                    }

                    // Отрисовка увеличенной фигуры
                    Pen myPen = new Pen(Color.Green, 2);
                    Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

                    g.DrawLine(myPen, scaledKv[0, 0], scaledKv[0, 1], scaledKv[1, 0], scaledKv[1, 1]);
                    g.DrawLine(myPen, scaledKv[0, 0], scaledKv[0, 1], scaledKv[2, 0], scaledKv[2, 1]);
                    g.DrawLine(myPen, scaledKv[2, 0], scaledKv[2, 1], scaledKv[3, 0], scaledKv[3, 1]);
                    g.DrawLine(myPen, scaledKv[1, 0], scaledKv[1, 1], scaledKv[3, 0], scaledKv[3, 1]);

                    g.Dispose();
                    myPen.Dispose();
                    textBox1.Clear();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes();

            rotationAngle = (rotationAngle + 90) % 360;

            double[,] rotationMatrix;
            switch (rotationAngle)
            {
                case 90:
                    rotationMatrix = new double[3, 3] {
                    { 0, -1, 0 },
                    { 1, 0, 0 },
                    { 0, 0, 1 }
                };
                    break;
                case 180:
                    rotationMatrix = new double[3, 3] {
                    { -1, 0, 0 },
                    { 0, -1, 0 },
                    { 0, 0, 1 }
                };
                    break;
                case 270:
                    rotationMatrix = new double[3, 3] {
                    { 0, 1, 0 },
                    { -1, 0, 0 },
                    { 0, 0, 1 }
                };
                    break;
                default:
                    rotationMatrix = new double[3, 3] {
                    { 1, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 0, 1 }
                };
                    break;
            }

            Init_figure();

            // Поворачиваем вершины
            double[,] temp = new double[4, 3];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp[i, j] = 0;
                    for (int m = 0; m < 3; m++)
                    {
                        temp[i, j] += kv[i, m] * rotationMatrix[m, j];
                    }
                }
            }

            // Применяем сдвиг
            int[,] rotatedKv = new int[4, 3];
            for (int i = 0; i < 4; i++)
            {
                rotatedKv[i, 0] = (int)Math.Round(temp[i, 0] + k);
                rotatedKv[i, 1] = (int)Math.Round(temp[i, 1] + l);
                rotatedKv[i, 2] = 1;
            }

            // Отрисовка повернутой фигуры
            Pen myPen = new Pen(Color.Green, 2);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(myPen, rotatedKv[0, 0], rotatedKv[0, 1], rotatedKv[1, 0], rotatedKv[1, 1]);
            g.DrawLine(myPen, rotatedKv[0, 0], rotatedKv[0, 1], rotatedKv[2, 0], rotatedKv[2, 1]);
            g.DrawLine(myPen, rotatedKv[2, 0], rotatedKv[2, 1], rotatedKv[3, 0], rotatedKv[3, 1]);
            g.DrawLine(myPen, rotatedKv[1, 0], rotatedKv[1, 1], rotatedKv[3, 0], rotatedKv[3, 1]);

            g.Dispose();
            myPen.Dispose();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes();

            int[,] reflectY = new int[3, 3]
            {
                { 1, 0, 0 },
                { 0, -1, 0 },
                { 0, 0, 1 }
            };

            Init_figure();

            // Применяем отражение
            int[,] temp = Multiply_matr(kv, reflectY);

            // Применяем сдвиг
            int[,] kv1 = new int[4, 3];
            for (int i = 0; i < 4; i++)
            {
                kv1[i, 0] = temp[i, 0] + k;
                kv1[i, 1] = temp[i, 1] + l;
                kv1[i, 2] = 1;
            }

            Pen myPen = new Pen(Color.Green, 2);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[1, 0], kv1[1, 1]);
            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[2, 0], kv1[2, 1]);
            g.DrawLine(myPen, kv1[2, 0], kv1[2, 1], kv1[3, 0], kv1[3, 1]);
            g.DrawLine(myPen, kv1[1, 0], kv1[1, 1], kv1[3, 0], kv1[3, 1]);

            g.Dispose();
            myPen.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes();

            int[,] reflectX = new int[3, 3]
            {
                { -1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            };

            Init_figure();

            int[,] temp = Multiply_matr(kv, reflectX);

            int[,] kv1 = new int[4, 3];
            for (int i = 0; i < 4; i++)
            {
                kv1[i, 0] = temp[i, 0] + k;
                kv1[i, 1] = temp[i, 1] + l;
                kv1[i, 2] = 1;
            }

            Pen myPen = new Pen(Color.Green, 2);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[1, 0], kv1[1, 1]);
            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[2, 0], kv1[2, 1]);
            g.DrawLine(myPen, kv1[2, 0], kv1[2, 1], kv1[3, 0], kv1[3, 1]);
            g.DrawLine(myPen, kv1[1, 0], kv1[1, 1], kv1[3, 0], kv1[3, 1]);

            g.Dispose();
            myPen.Dispose();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}
