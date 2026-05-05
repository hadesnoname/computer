namespace KG
{
    public partial class Form6 : Form
    {
        double[,] kv = new double[8, 3]; // матрица тела
        double[,] matr_sdv = new double[3, 3]; //матрица преобразовани€
        double[,] matr_vras = new double[3, 3]; // ћатрица поворота
        double[,] sm = new double[3, 3];
        double[,] currentIndv = new double[8, 3];// “екущее состо€ние фигуры
        int k, l; // элементы матрицы сдвига
        double n = 1; //дл€ уменьшени€
        List<Color> colors = new List<Color>
        {
            Color.Purple,
            Color.Red,
            Color.Green,
            Color.Purple,
            Color.Orange,
            Color.Yellow,
            Color.Blue,
            Color.Magenta,
            Color.Cyan,
            Color.DarkKhaki,
        };
        int currentColorIndex = 0;
        public Form6()
        {
            InitializeComponent();
        }
        private void Init_small(double m)
        {
            sm[0, 0] = m; sm[0, 1] = 0; sm[0, 2] = 0;
            sm[1, 0] = 0; sm[1, 1] = m; sm[1, 2] = 0;
            sm[2, 0] = 0; sm[2, 1] = 0; sm[2, 2] = 1;
        }
        private void Init_8ygolnik()// матрица тела
        {
            kv[0, 0] = 150; kv[0, 1] = 0; kv[0, 2] = 1;
            kv[1, 0] = 106; kv[1, 1] = 106; kv[1, 2] = 1;
            kv[2, 0] = 0; kv[2, 1] = 150; kv[2, 2] = 1;
            kv[3, 0] = -106; kv[3, 1] = 106; kv[3, 2] = 1;
            kv[4, 0] = -150; kv[4, 1] = 0; kv[4, 2] = 1;
            kv[5, 0] = -106; kv[5, 1] = -106; kv[5, 2] = 1;
            kv[6, 0] = 0; kv[6, 1] = -150; kv[6, 2] = 1;
            kv[7, 0] = 106; kv[7, 1] = -106; kv[7, 2] = 1;

            Array.Copy(kv, currentIndv, kv.Length);
        }
        private void Init_vrashen()// матрица вращени€ фигуры
        {
            if (!int.TryParse(textBox1.Text, out int degrees))
            {
                MessageBox.Show("¬ведите корректное целое число дл€ угла!");
                return;
            }

            double radians = degrees * Math.PI / 180;
            matr_vras[0, 0] = Math.Cos(radians); matr_vras[0, 1] = Math.Sin(radians); matr_vras[0, 2] = 0;
            matr_vras[1, 0] = -Math.Sin(radians); matr_vras[1, 1] = Math.Cos(radians); matr_vras[1, 2] = 0;
            matr_vras[2, 0] = 0; matr_vras[2, 1] = 0; matr_vras[2, 2] = 1;
        }
        private void Init_matr_preob(int k1, int l1)// матрица дл€ перемешени€ фигуры на центр
        {
            matr_sdv[0, 0] = 1; matr_sdv[0, 1] = 0; matr_sdv[0, 2] = 0;
            matr_sdv[1, 0] = 0; matr_sdv[1, 1] = 1; matr_sdv[1, 2] = 0;
            matr_sdv[2, 0] = k1; matr_sdv[2, 1] = l1; matr_sdv[2, 2] = 1;
        }
        private double[,] Multiply_matr(double[,] a, double[,] b)// умнжение матриц
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            double[,] r = new double[n, m];
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
        private int[,] Convert(double[,] doubleMatrix) //‘ункци€ дл€ преобразовани€ из double в int
        {
            int rows = doubleMatrix.GetLength(0);
            int cols = doubleMatrix.GetLength(1);

            int[,] intMatrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    intMatrix[i, j] = (int)Math.Round(doubleMatrix[i, j]); // ќкругл€ем до ближайшего целого
                }
            }

            return intMatrix;
        }

        private void Draw_Kv()
        {

            Init_8ygolnik(); //инициализаци€ матрицы тела
            Init_matr_preob(k, l); //инициализаци€ матрицы преобразовани€
            double[,] kv11 = Multiply_matr(kv, matr_sdv); //перемножение матриц
            int[,] kv1 = Convert(kv11);// из добл в инт
            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //создаем новый объект Graphics (поверхность рисовани€) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            // рисуем 1 сторону 
            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[1, 0], kv1[1, 1]);
            // рисуем 2 сторону 
            g.DrawLine(myPen, kv1[1, 0], kv1[1, 1], kv1[2, 0], kv1[2, 1]);
            // рисуем 3 сторону 
            g.DrawLine(myPen, kv1[2, 0], kv1[2, 1], kv1[3, 0], kv1[3, 1]);
            // рисуем 4 сторону 
            g.DrawLine(myPen, kv1[3, 0], kv1[3, 1], kv1[4, 0], kv1[4, 1]);
            //рисуем 5 сторону
            g.DrawLine(myPen, kv1[4, 0], kv1[4, 1], kv1[5, 0], kv1[5, 1]);
            // рисуем 6 сторону 
            g.DrawLine(myPen, kv1[5, 0], kv1[5, 1], kv1[6, 0], kv1[6, 1]);
            // рисуем 7 сторону 
            g.DrawLine(myPen, kv1[6, 0], kv1[6, 1], kv1[7, 0], kv1[7, 1]);
            // рисуем 8 сторону 
            g.DrawLine(myPen, kv1[7, 0], kv1[7, 1], kv1[0, 0], kv1[0, 1]);

            g.Dispose();// освобождаем все ресурсы, св€занные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, св€занные с Pen

        }
        private void DrawTransformedFigure() // отрисовывалка дл€ поворота
        {
            if (string.IsNullOrEmpty(textBox1.Text) || !int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("¬ведите корректное целое число!");
                return;
            }

            Init_vrashen(); // ћатрица поворота
            Init_matr_preob(k, l); // ћатрица сдвига
            n -= 0.02;
            Init_small(n);
            // ѕримен€ем поворот к текущему состо€нию фигуры
            double[,] rot = Multiply_matr(currentIndv, matr_vras);
            double[,] rotated = Multiply_matr(rot, sm);
            // ќбновл€ем текущее состо€ние фигуры
            Array.Copy(rotated, currentIndv, rotated.Length);

            // ѕримен€ем сдвиг
            double[,] translated = Multiply_matr(rotated, matr_sdv);

            //  онвертируем в целочисленные координаты
            int[,] figureInt = Convert(translated);
            Color currentColor = colors[currentColorIndex];
            currentColorIndex = (currentColorIndex + 1) % colors.Count; // ÷иклическое переключение

            // ќтрисовываем фигуру
            using (Pen myPen = new Pen(currentColor, 2))
            using (Graphics g = Graphics.FromHwnd(pictureBox1.Handle))
            {


                // –исуем линии фигуры
                g.DrawLine(myPen, figureInt[0, 0], figureInt[0, 1], figureInt[1, 0], figureInt[1, 1]);
                g.DrawLine(myPen, figureInt[1, 0], figureInt[1, 1], figureInt[2, 0], figureInt[2, 1]);
                g.DrawLine(myPen, figureInt[2, 0], figureInt[2, 1], figureInt[3, 0], figureInt[3, 1]);
                g.DrawLine(myPen, figureInt[3, 0], figureInt[3, 1], figureInt[4, 0], figureInt[4, 1]);
                g.DrawLine(myPen, figureInt[4, 0], figureInt[4, 1], figureInt[5, 0], figureInt[5, 1]);
                g.DrawLine(myPen, figureInt[5, 0], figureInt[5, 1], figureInt[6, 0], figureInt[6, 1]);
                g.DrawLine(myPen, figureInt[6, 0], figureInt[6, 1], figureInt[7, 0], figureInt[7, 1]);
                g.DrawLine(myPen, figureInt[7, 0], figureInt[7, 1], figureInt[0, 0], figureInt[0, 1]);


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            Draw_Kv();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrawTransformedFigure();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            n = 1;
        }
    }
}
