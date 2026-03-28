using System.IO;
using System.Text;
namespace KG
{
    public partial class Form1 : Form
    {
        const int MaxN = 10; // максимально допустимая размерность матрицы 
        int n = 3;  // текущая размерность матрицы
        TextBox[,] MatrText = null; // матрица элементов типа TextBox
        double[,] Matr1 = new double[MaxN, MaxN]; // матрица 1 чисел с плавающей точкой
        double[,] Matr2 = new double[MaxN, MaxN]; // матрица 2 чисел с плавающей точкой
        double[,] Matr3 = new double[MaxN, MaxN]; // матрица результатов
        bool f1;  // флажок, который указывает о вводе данных в матрицу Matr1
        bool f2;  // флажок, который указывает о вводе данных в матрицу Matr2
        int dx = 40, dy = 20; // ширина и высота ячейки в MatrText[,] 
        Form2
        form2 = null; // экземпляр (объект) класса формы Form2
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Инициализация элементов управления и внутренних переменных 
            textBox1.Text = "";
            f1 = f2 = false;
            label2.Text = "false";
            label3.Text = "false";
            // Выделение памяти и настройка MatrText 
            int i, j;
            // 1. Выделение памяти для формы Form2 
            form2 = new Form2();
            // 2. Выделение памяти под самую матрицу 
            MatrText = new TextBox[MaxN, MaxN];
            // 3. Выделение памяти для каждой ячейки матрицы и ее настройка
            for (i = 0; i < MaxN; i++)
                for (j = 0; j < MaxN; j++)
                {
                    MatrText[i, j] = new TextBox();
                    MatrText[i, j].Text = "0";
                    MatrText[i, j].Location = new System.Drawing.Point(10 + i *
                    dx, 10 + j * dy);
                    MatrText[i, j].Size = new System.Drawing.Size(dx, dy);
                    MatrText[i, j].Visible = false;
                    form2.Controls.Add(MatrText[i, j]);
                }
        }
        private void Clear_MatrText()
        {
            // Обнуление ячеек MatrText
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    MatrText[i, j].Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (textBox1.Text == "") return;
            n = int.Parse(textBox1.Text);
            // 2. Обнуление ячейки MatrText
            Clear_MatrText();
            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * n + j + 1;
                    // 3.2. Сделать ячейку видимой
                    MatrText[i, j].Visible = true;
                }
            // 4. Корректировка размеров формы
            form2.Width = 10 + n * dx + 20;
            form2.Height = 10 + n * dy + form2.button1.Height + 50;
            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + n * dy + 10;
            form2.button1.Width = form2.Width - 30;
            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (MatrText[i, j].Text != "")
                            Matr1[i, j] = Double.Parse(MatrText[i, j].Text);
                        else
                            Matr1[i, j] = 0;
                // 8. Данные в матрицу Matr1 внесены
                f1 = true;
                label2.Text = "true";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (textBox1.Text == "") return;
            n = int.Parse(textBox1.Text);
            // 2. Обнулить ячейки MatrText Clear_MatrText();
            // 3. Настройка свойств ячеек матрицы MatrText
            // с привязкой к значению n и форме Form2
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * n + j + 1;
                    // 3.2. Сделать ячейку видимой
                    MatrText[i, j].Visible = true;
                }
            // 4. Корректировка размеров формы
            form2.Width = 10 + n * dx + 20;
            form2.Height = 10 + n * dy + form2.button1.Height + 50;
            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form2.button1.Left = 10;
            form2.button1.Top = 10 + n * dy + 10;
            form2.button1.Width = form2.Width - 30;
            // 6. Вызов формы Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr2
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        Matr2[i, j] = Double.Parse(MatrText[i, j].Text);
                // 8. Матрица Matr2 сформирована
                f2 = true;
                label3.Text = "true";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int nn;
            nn = Int16.Parse(textBox1.Text);
            if (nn != n)
            {
                f1 = f2 = false;
                label2.Text = "false";
                label3.Text = "false";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;
            // 2. Вычисление произведения матриц. Результат в Matr3
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Matr3[j, i] = 0;
                    for (int k = 0; k < n; k++)
                        Matr3[j, i] = Matr3[j, i] + Matr1[k, i] * Matr2[j, k];
                }
            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i, j].TabIndex = i * n + j + 1;
                    // 3.2. Перевести число в строку
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }
            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileStream fw = null;
            string msg;
            byte[] msgByte = null; // байтовый массив
                                   // 1. Открыть файл для записи fw = new FileStream("Res_Matr.txt",           FileMode.Create);
                                   // 2. Запись матрицы результата в файл

            // 2.1. Сначала записать число элементов матрицы Matr3
            msg = n.ToString() + "\r\n";
            // перевод строки msg в байтовый массив msgByte
            msgByte = Encoding.Default.GetBytes(msg);
            // запись массива msgByte в файл
            fw.Write(msgByte, 0, msgByte.Length);
            // 2.2. Теперь записать саму матрицу
            msg = "";
            for (int i = 0; i < n; i++)
            {
                // формируем строку msg из элементов матрицы
                for (int j = 0; j < n; j++)
                    msg = msg + Matr3[i, j].ToString() + " ";
                msg = msg + "\r\n";
                // добавить перевод строки
            }
            // 3. Перевод строки msg в байтовый массив msgByte
            msgByte = Encoding.Default.GetBytes(msg);
            // 4. запись строк матрицы в файл
            fw.Write(msgByte, 0, msgByte.Length);
            // 5. Закрыть файл
            if (fw != null) fw.Close();
        }

        /// <summary>
        /// Сложение матриц
        /// </summary>
       private void AddMatrices()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Matr3[i, j] = Matr1[i, j] + Matr2[i, j];
                }
            }
        }

        /// <summary>
        /// Вычитание матриц
        /// </summary>
        private void SubtractMatrices()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Matr3[i, j] = Matr1[i, j] - Matr2[i, j];
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;

            // 2. Выполнение сложения матриц
            AddMatrices();

            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }
            }
            // 4. Вывод формы
            form2.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные в обеих матрицах
            if (!((f1 == true) && (f2 == true))) return;

            // 2. Выполнение вычитания матриц
            SubtractMatrices();

            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MatrText[i, j].Text = Matr3[i, j].ToString();
                }
            }

            // 4. Вывод формы
            form2.ShowDialog();
        }

        /// <summary>
        /// Модуль вектора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            // 1. Проверка, задана ли размерность матрицы
            if (textBox1.Text == "")
            {
                MessageBox.Show("Сначала задайте размерность матрицы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Чтение размерности матрицы
            int n = int.Parse(textBox1.Text);

            // 3. Создание формы для ввода вектора
            Form vectorInputForm = new Form();
            vectorInputForm.Text = "Введите вектор";
            vectorInputForm.Width = 500;
            vectorInputForm.Height = 130 + n * 30;

            // 4. Создание текстовых полей для ввода вектора
            TextBox[] vectorTextBoxes = new TextBox[n];
            for (int i = 0; i < n; i++)
            {
                vectorTextBoxes[i] = new TextBox();
                vectorTextBoxes[i].Location = new System.Drawing.Point(10, 10 + i * 30);
                vectorTextBoxes[i].Width = 200;
                vectorTextBoxes[i].Text = "0"; // По умолчанию заполняем нулями
                vectorInputForm.Controls.Add(vectorTextBoxes[i]);
            }

            // 5. Создание кнопки "OK"
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.Location = new System.Drawing.Point(20, 20 + n * 40);
            okButton.DialogResult = DialogResult.OK;
            okButton.Width = 50;
            okButton.Height = 30;
            vectorInputForm.Controls.Add(okButton);

            // 6. Отображение формы и обработка результата
            if (vectorInputForm.ShowDialog() == DialogResult.OK)
            {
                // 7. Чтение вектора из текстовых полей
                double[] vector = new double[n];
                bool isValid = true;

                for (int i = 0; i < n; i++)
                {
                    if (!double.TryParse(vectorTextBoxes[i].Text, out vector[i]))
                    {
                        isValid = false;
                        break;
                    }
                }

                // 8. Проверка корректности ввода
                if (!isValid)
                {
                    MessageBox.Show("Некорректный ввод вектора! Пожалуйста, введите числа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 9. Вычисление модуля вектора
                double modulus = 0;
                for (int i = 0; i < n; i++)
                {
                    modulus += vector[i] * vector[i];
                }
                modulus = Math.Sqrt(modulus);

                // 10. Отображение результата
                MessageBox.Show($"Модуль вектора: {modulus:F2}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
