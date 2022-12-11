using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ChislennyeMetody
{
    public partial class Form1 : Form
    {
        TableOutput Output = new TableOutput();
        IntGrafic grafics = new IntGrafic();
        Random rand = new Random();
        public string theme = "null";
        double x = 0;
        double eps = 0.001;
        int epsNum = 10;

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            SetHeaderOfSolve();
            ChangeOutputType();
        }

        private void SetHeaderOfSolve()
        {
            grafics.Hide();

            checkBox1.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox7.Visible = false;
            groupBox8.Visible = false;
            groupBox9.Visible = false;

            if (theme == "null" || theme == "exp(x)" || theme == "cos(x)" || theme == "ln(1+x)")
            {
                label3.Text = "X = ";
                label4.Location = new Point(323, 24);
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                textBox1.Visible = true;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                textBox2.Location = new Point(373, 21);
                checkBox1.Visible = true;
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox3.Visible = true;
                groupBox3.Text = "Вывод в таблицу";
                dataGridView1.RowCount = 1;
                dataGridView1.Columns.Clear();
            }
            else if (theme == "Дихотомия" || theme == "Хорды" || theme == "Касательные" || theme == "Итерации")
            {
                label3.Text = "Y = ";
                label4.Location = new Point(174, 52);
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                textBox1.Visible = false;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                textBox2.Location = new Point(222, 49);
                checkBox1.Visible = false;
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox3.Text = "Вывод в таблицу";
                dataGridView1.RowCount = 1;
                dataGridView1.Columns.Clear();
            }
            else if (theme == "Метод Гаусса" || theme == "Метод Зейделя")
            {
                groupBox3.Visible = true;
                groupBox3.Text = "Матрица";
                groupBox4.Visible = true;

                if (rand.Next(0, 2) == 0)
                    textBox7.Text = "3";
                else
                    textBox7.Text = "4";

                MatrixSet();
                SetDefoultMatrix();
            }
            else if (theme == "Метод Ньютона" || theme == "Метод Лагранжа" || theme == "Обратная интерполяция")
            {
                groupBox5.Visible = true;
                groupBox7.Visible = false;
                groupBox5.Text = "Параметры - " + theme;
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.RowCount = 1;

                if (theme == "Обратная интерполяция")
                {
                    dataGridView2.Columns[1].HeaderText = "Y";
                    dataGridView2.Columns[2].HeaderText = "X";
                    label13.Text = "Y = ";
                    label15.Text = "X = ";
                }
                else
                {
                    dataGridView2.Columns[1].HeaderText = "X";
                    dataGridView2.Columns[2].HeaderText = "Y";
                    label13.Text = "X = ";
                    label15.Text = "Y = ";
                }

                grafics.Show();
            }
            else if (theme == "Метод прямоугольников" || theme == "Метод трапеций" || theme == "Метод Симпсона")
            {
                groupBox7.Visible = true;
                groupBox7.Text = "Интегрирование - " + theme;
                ShowCurrentFunc();
            }
            else if(theme == "Метод Эйлера" || theme == "Усовершенствованый метод Эйлера" || theme == "Метод Рунге-Кутта")
            {
                groupBox8.Visible = true;
                groupBox8.Text = "Дифуры - " + theme;

                label21.Text = "Количество разбиений:";
                label19.Text = "Отрезок";
                label22.Visible = false;
                textBox20.Visible = false;
                label26.Visible = false;
                textBox23.Visible = false;
                textBox17.Visible = false;
                dataGridView3.Visible = false;

                textBox17.Text = "1";
                textBox19.Text = "10";

                if (theme == "Метод Рунге-Кутта")
                {
                    label21.Text = "Введите шаг h:";
                    label19.Text = "В точке X=";
                    label22.Visible = true;
                    textBox20.Visible = true;
                    label26.Visible = true;
                    textBox23.Visible = true;

                    textBox18.Text = "1";
                    textBox19.Text = "0,1";
                }
                else
                {
                    textBox17.Visible = true;
                    dataGridView3.Visible = true;

                    textBox18.Text = "0";
                }
                ShowCurrentFunc();
            }
            else if(theme == "Квадратичная зависимость" || theme == "Линейная зависимость")
            {
                groupBox9.Visible = true;
                groupBox9.Text = theme;
            }
        }

        private void SetDefoultMatrix()
        {
            if (textBox7.Text == "3")
            {
                double[,] _defoult = new double[3, 4]
                {
                {2.7, 1.2, 3.4, 7.4 },
                {1.7, -3.2, 4.6, 11.2 },
                {1.1, 4.1, 0.8, -1.1 }
                };

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = _defoult[i, j].ToString();
                    }
                }
            }
            else
            {
                double[,] _defoult = new double[4, 5]
                {
                    { 10, -1,  2,  0,   6 },
                    { -1, 11, -1,  3,  25 },
                    { 2,  -1, 10, -1, -11 },
                    { 0,   3, -1,  8,  15 }
                };

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = _defoult[i, j].ToString();
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            theme = e.Node.Text;
            groupBox1.Text = "Параметры - " + theme;
            groupBox4.Text = "Параметры - " + theme;
            SetHeaderOfSolve();
        }

        private string ChecDoubleSyntax(string value)
        {
            int index = value.IndexOf('.');

            if (index == -1) return value;
            else
            {
                char[] temp = value.ToCharArray();
                temp[index] = ',';
                value = string.Join("", temp);
            }

            return value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (theme == "null") return;

            x = Convert.ToDouble(ChecDoubleSyntax(textBox1.Text));
            eps = Convert.ToDouble(ChecDoubleSyntax(textBox2.Text));
            epsNum = eps.ToString().Length - 2;

            if (!checkBox2.Checked)
            {
                Output.table = dataGridView1;
                Output.ResetTable();

                List<double> Solve;
                List<string> Data;
                switch (theme)
                {
                    #region Theme 1
                    case "exp(x)":
                        if (checkBox1.Checked)
                        {
                            Output.SetTitles(new string[] { "X", "Math.Exp", "MyExpX.MyExpSimple", "MyExpX.MyExpRecurr" });

                            double[] Xs = new double[] { -100, -28, -9, -5, -1, -0.5, 0, 0.5, 1, 5, 13, 100 };
                            for (int i = 0; i < Xs.Length; i++)
                            {
                                x = Xs[i];
                                Output.AddLine(new string[]
                                {
                                    x.ToString(),
                                    Math.Exp(x).ToString(),
                                    MyExpX.MyExpSimple(x,eps).ToString(),
                                    MyExpX.MyExpRecurr(x,eps).ToString()
                                });
                            }
                        }
                        else
                        {
                            Output.SetTitles(new string[] { "Math.Exp", "MyExpX.MyExpSimple", "MyExpX.MyExpRecurr" });
                            Output.AddLine(new string[]
                            {
                                Math.Exp(x).ToString(),
                                MyExpX.MyExpSimple(x,eps).ToString(),
                                MyExpX.MyExpRecurr(x,eps).ToString()
                            });
                        }
                        break;
                    case "cos(x)":
                        if (checkBox1.Checked)
                        {
                            Output.SetTitles(new string[] { "X", "Math.Cos", "MyCosX.CosX" });

                            double[] Xs = new double[] { -10000, -228, -97, -5, -Math.PI, -1, -0.5, 0, 2 * Math.PI, 0.5, 1, Math.PI, 5, 103, 10000 };
                            for (int i = 0; i < Xs.Length; i++)
                            {
                                x = Xs[i];
                                Output.AddLine(new string[]
                                {
                                    (x == -Math.PI) ? "-Pi" : (x == Math.PI) ? "Pi" : (x == 2*Math.PI) ? "2*Pi" : x.ToString(),
                                    Math.Cos(x).ToString(),
                                    MyCosX.CosX(x,eps).ToString()
                                });
                            }
                        }
                        else
                        {
                            Output.SetTitles(new string[] { "Math.Cos", "MyCosX.CosX" });
                            Output.AddLine(new string[]
                            {
                                Math.Cos(x).ToString(),
                                MyCosX.CosX(x,eps).ToString()
                            });
                        }
                        break;
                    case "ln(1+x)":
                        if (checkBox1.Checked)
                        {
                            Output.SetTitles(new string[] { "X", "Math.Log", "MyLnX.LnX" });

                            double[] Xs = new double[] { -100, -10, -1, -0.9, -0.25, 0, 0.5, 1, Math.E, 4, 10, 24, 103, 228, 10000 };
                            for (int i = 0; i < Xs.Length; i++)
                            {
                                x = Xs[i];
                                Output.AddLine(new string[]
                                {
                                    (x == Math.E) ? "e" : x.ToString(),
                                    Math.Log(1+x, Math.E).ToString(),
                                    MyLn1plusX.Ln1plusX(x, eps).ToString()
                                });
                            }
                        }
                        else
                        {
                            double temp = MyLn1plusX.Ln1plusX(x, eps);
                            Output.SetTitles(new string[] { "Math.Log", "MyLnX.LnX" });
                            Output.AddLine(new string[]
                            {
                                Math.Log(1+x, Math.E).ToString(),
                                (temp == double.PositiveInfinity) ? "Не число" : temp.ToString()
                            });
                        }
                        break;
                    #endregion
                    #region Theme 2
                    case "Дихотомия":
                        GenT2.SetParams(
                            Convert.ToDouble(ChecDoubleSyntax(textBox3.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox4.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox5.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox6.Text))
                        );
                        GenT2.eps = eps;
                        Solve = Dihotomia.GetSolve();
                        Data = new List<string>();

                        for (int i = 0; i < Solve.Count; i++)
                            Data.Add(Math.Round(Solve[i], epsNum).ToString());

                        Output.SetTitles(new string[] { "x0", "x1", "x2", "x3" });
                        Output.AddLine(Data.ToArray());

                        break;
                    case "Хорды":
                        GenT2.SetParams(
                            Convert.ToDouble(ChecDoubleSyntax(textBox3.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox4.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox5.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox6.Text))
                        );
                        GenT2.eps = eps;
                        Solve = Hords.GetSolve();
                        Data = new List<string>();

                        for (int i = 0; i < Solve.Count; i++)
                            Data.Add(Math.Round(Solve[i], epsNum).ToString());

                        Output.SetTitles(new string[] { "x0", "x1", "x2", "x3" });
                        Output.AddLine(Data.ToArray());

                        break;
                    case "Касательные":
                        GenT2.SetParams(
                            Convert.ToDouble(ChecDoubleSyntax(textBox3.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox4.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox5.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox6.Text))
                        );
                        GenT2.eps = eps;
                        Solve = Tangents.GetSolve();
                        Data = new List<string>();

                        for (int i = 0; i < Solve.Count; i++)
                            Data.Add(Math.Round(Solve[i], epsNum).ToString());

                        Output.SetTitles(new string[] { "x0", "x1", "x2", "x3" });
                        Output.AddLine(Data.ToArray());

                        break;
                    case "Итерации":
                        label5.Text = "";
                        GenT2.SetParams(
                            Convert.ToDouble(ChecDoubleSyntax(textBox3.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox4.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox5.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox6.Text))
                        );
                        GenT2.eps = eps;
                        Solve = Iterations.GetSolve();
                        Data = new List<string>();
                        for (int i = 0; i < Solve.Count; i++)
                            if (Solve[i] != double.PositiveInfinity && Solve[i] != double.NegativeInfinity)
                                Data.Add(Math.Round(Solve[i], epsNum).ToString());


                        Output.SetTitles(new string[] { "x0", "x1", "x2", "x3" });
                        Output.AddLine(Data.ToArray());
                        break;
                    #endregion
                    default:
                        Output.SetTitles(new string[] { "null" });
                        break;
                }
            }
            else
            {
                List<double> Solve;
                switch (theme)
                {
                    #region Theme 1
                    case "exp(x)":
                        label5.Text = "Math.Exp = " + Math.Exp(x).ToString() + "\n";
                        label5.Text += "MyExpX.MyExpSimple = " + MyExpX.MyExpSimple(x, eps).ToString() + "\n";
                        label5.Text += "MyExpX.MyExpRecurr = " + MyExpX.MyExpRecurr(x, eps).ToString() + "\n";
                        break;
                    case "cos(x)":
                        label5.Text = "Math.Cos = " + Math.Cos(x).ToString() + "\n";
                        label5.Text += "MyCosX.CosX = " + MyCosX.CosX(x, eps).ToString() + "\n";
                        break;
                    case "ln(1+x)":
                        label5.Text = "Math.Log(x, Math.E) = " + Math.Log(1 + x, Math.E).ToString() + "\n";
                        label5.Text += "MyLnX.LnX = " + MyLn1plusX.Ln1plusX(x, eps).ToString() + "\n";
                        break;
                    #endregion
                    #region Theme 2
                    case "Дихотомия":
                        label5.Text = "";
                        GenT2.SetParams(
                            Convert.ToDouble(ChecDoubleSyntax(textBox3.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox4.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox5.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox6.Text))
                        );
                        GenT2.eps = eps;
                        Solve = Dihotomia.GetSolve();
                        for (int i = 0; i < Solve.Count; i++)
                            label5.Text += "x" + i.ToString() + " = " + Solve[i].ToString() + "\n";
                        break;
                    case "Хорды":
                        label5.Text = "";
                        GenT2.SetParams(
                            Convert.ToDouble(ChecDoubleSyntax(textBox3.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox4.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox5.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox6.Text))
                        );
                        GenT2.eps = eps;
                        Solve = Hords.GetSolve();
                        for (int i = 0; i < Solve.Count; i++)
                            label5.Text += "x" + i.ToString() + " = " + Solve[i].ToString() + "\n";
                        break;
                    case "Касательные":
                        label5.Text = "";
                        GenT2.SetParams(
                            Convert.ToDouble(ChecDoubleSyntax(textBox3.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox4.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox5.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox6.Text))
                        );
                        GenT2.eps = eps;
                        Solve = Tangents.GetSolve();
                        for (int i = 0; i < Solve.Count; i++)
                            label5.Text += "x" + i.ToString() + " = " + Solve[i].ToString() + "\n";
                        break;
                    case "Итерации":
                        label5.Text = "";
                        GenT2.SetParams(
                            Convert.ToDouble(ChecDoubleSyntax(textBox3.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox4.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox5.Text)),
                            Convert.ToDouble(ChecDoubleSyntax(textBox6.Text))
                        );
                        GenT2.eps = eps;
                        Solve = Iterations.GetSolve();
                        for (int i = 0; i < Solve.Count; i++) {
                            if (Solve[i] == double.NaN || Solve[i] == double.PositiveInfinity || Solve[i] == double.NegativeInfinity) continue;
                            label5.Text += "x" + i.ToString() + " = " + Solve[i].ToString() + "\n";
                        }
                        break;
                    #endregion
                    default:
                        label5.Text += "null";
                        break;
                }
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            ChangeOutputType();
        }

        private void ChangeOutputType()
        {
            if (checkBox2.Checked)
            {
                groupBox2.Visible = true;
                groupBox3.Visible = false;
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
            }
            else
            {
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                checkBox1.Enabled = true;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            MatrixSet();
        }

        private void MatrixSet()
        {
            int rows, cols;

            int.TryParse(textBox7.Text, out rows);
            if (rows == 0) rows = 2;

            cols = rows + 1;

            dataGridView1.RowCount = 1;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.RowCount = rows;
            dataGridView1.ColumnCount = cols;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderText = String.Concat("a", column.Index.ToString());
                column.Width = 80;
                if (column.Index == cols - 1) column.HeaderText = "B";
            }
            dataGridView1.Rows.Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rows, cols;

            int.TryParse(textBox7.Text, out rows);
            if (rows == 0) rows = 2;

            cols = rows + 1;

            double[,] matrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            double[] answer = null;
            int ex = 0;
            if (theme == "Метод Гаусса")
            {
                GaussMethod.matrix = matrix;
                GaussMethod.n = rows;
                answer = GaussMethod.GetSolve();
            }
            else
            {
                ZeidelMethod.Matrix = matrix;
                ZeidelMethod.mSize = rows;
                answer = ZeidelMethod.GetSolve();
            }
            dataGridView1.RowCount = rows + 1;
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add("Ответ:");
            dataGridView1.Rows.Add();
            int row = rows + 4;
            if (answer is null) return;

            for (int i = 0; i < answer.Length; i++)
                dataGridView1.Rows[row - 1].Cells[i].Value = "x" + (i + 1).ToString();

            if (ex == 1) dataGridView1.Rows[row].Cells[0].Value = "Итерационное расхождение";
            else
                for (int i = 0; i < answer.Length; i++)
                    dataGridView1.Rows[row].Cells[i].Value = answer[i].ToString();

        }

        //Интерполяция 
        int indexRowInInterpol = 0;
        List<double> masX = new List<double>();
        List<double> masY = new List<double>();

        private void button6_Click(object sender, EventArgs e)
        {
            double _x, _y;
            double.TryParse(textBox8.Text, out _x);
            double.TryParse(textBox9.Text, out _y);

            if (theme == "Обратная интерполяция")
            {
                if (masY.IndexOf(_y) != -1) return;
                double temp = _x;
                _x = _y;
                _y = temp;
            }
            else
            {
                if (masX.IndexOf(_x) != -1) return;
            }

            dataGridView2.Rows.Add(new string[] { indexRowInInterpol.ToString(), _x.ToString(), _y.ToString() });
            masX.Add(_x);
            masY.Add(_y);

            indexRowInInterpol++;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClearInterpolTable();
        }

        private void ClearInterpolTable()
        {
            masX.Clear();
            masY.Clear();
            indexRowInInterpol = 0;
            dataGridView2.RowCount = 1;
            dataGridView2.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearInterpolTable();

            for (int i = -7; i < 8; i++)
            {
                double y = i * i * i;
                if (theme == "Обратная интерполяция")
                {
                    dataGridView2.Rows.Add(new string[] { (i+7).ToString(),
                                                      y.ToString(),
                                                      i.ToString() }
                    );
                    masY.Add(i);
                    masX.Add(y);
                }
                else
                {
                    dataGridView2.Rows.Add(new string[] { (i+7).ToString(),
                                                      i.ToString(),
                                                      y.ToString() }
                    );
                    masX.Add(i);
                    masY.Add(y);
                }
                indexRowInInterpol++;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearInterpolTable();

            int i = 0;
            for (double x = 0; x < 7; x += 0.25d)
            {
                double y = x * x;
                if (theme == "Обратная интерполяция")
                {
                    dataGridView2.Rows.Add(new string[] { i.ToString(),
                                                      y.ToString(),
                                                      x.ToString() }
                    );
                    masY.Add(x);
                    masX.Add(y);
                }
                else
                {
                    dataGridView2.Rows.Add(new string[] { i.ToString(),
                                                      x.ToString(),
                                                      y.ToString() }
                    );
                    masX.Add(x);
                    masY.Add(y);
                }
                i++;
            }
            indexRowInInterpol = i;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double findX;
            double? findY = null;

            if (!double.TryParse(ChecDoubleSyntax(textBox11.Text), out findX)) return;

            AllData.n = indexRowInInterpol - 1;
            AllData.MasX = masX.ToArray();
            AllData.MasY = masY.ToArray();

            switch (theme)
            {
                case "Метод Лагранжа":
                    findY = IntLagrange.GetSolve(findX);
                    grafics.Draw(1);
                    break;
                case "Метод Ньютона":
                    findY = IntNewton.GetSolve(findX);
                    grafics.Draw(0);
                    break;
                case "Обратная интерполяция":
                    findY = IntLagrange.GetSolve(findX);
                    grafics.Draw(2);
                    break;
            }

            textBox10.Text = findY.ToString();
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            HelpWindow form = new HelpWindow();
            form.Visible = true;

        }

        //Интегрирование
        int indexFunc = 0;

        private List<Func<double, double>> funcs = new List<Func<double, double>>()
        {
            (double x) => x*x*x,
            (double x) => x / (x - 1),
            (double x) => Math.Cos(x),
        };
        private List<string> strFuncs = new List<string>()
        {
            "F(x) = x^3",
            "F(x) = x / (x-1)",
            "F(x) = Cos(x)"
        };
        //Solve
        private void button11_Click(object sender, EventArgs e)
        {
            int N = int.Parse(textBox14.Text);
            int A = int.Parse(textBox15.Text);
            int B = int.Parse(textBox16.Text);
            double _solveInteg = 0;
            switch (theme)
            {
                case "Метод прямоугольников":
                    _solveInteg = Integration.Rectangle(funcs[indexFunc], A, B, N);
                    break;
                case "Метод трапеций":
                    _solveInteg = Integration.Trapeze(funcs[indexFunc], A, B, N); 
                    break;
                case "Метод Симпсона":
                    _solveInteg = Integration.Simpson(funcs[indexFunc], A, B, N); 
                    break;
            }

            textBox13.Text = _solveInteg.ToString();
        }
        //Next F
        private void button8_Click(object sender, EventArgs e)
        {
            NextF();
        }
        //Previos F
        private void button9_Click(object sender, EventArgs e)
        {
            PreviosF();
        }

        private void NextF()
        {
            if (indexFunc == funcs.Count - 1) return;
            indexFunc++;
            ShowCurrentFunc();
        }

        private void PreviosF()
        {
            if (indexFunc == 0) return;
            indexFunc--;
            ShowCurrentFunc();
        }

        private void ShowCurrentFunc()
        {
            textBox12.Text = strFuncs[indexFunc];
            textBox22.Text = strFuncsDiff[indexFunc];
        }
        //Дифуры
        private List<string> strFuncsDiff = new List<string>()
        {
            "F(x,y) = 6*x^2 + 5xy",
            "y` = sqrt(2x+1)",
            "F(x,y) = y - (2 * x / y)"
        };
        private List<Func<double, double, double>> funcsDiff = new List<Func<double, double, double>>()
        {
            (double x, double y) => 6 * x * x + 5 * x * y,
            (double x, double y) => Math.Sqrt(2*x+1),
            (double x, double y) => y - (2 * x / y)
        };

        private void button10_Click(object sender, EventArgs e)
        {
            NextF();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            PreviosF();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            double N = double.Parse(textBox19.Text);
            double A = double.Parse(textBox18.Text);
            double B = double.Parse(textBox17.Text);
            double Y = double.Parse(textBox21.Text);
            double X = double.Parse(textBox20.Text);

            double[][] _solveDiff = new double[2][];
            double solveRungeKutta = double.NaN;

            switch (theme)
            {
                case "Метод Эйлера":
                    _solveDiff = DifferentialEquations.Euler(funcsDiff[indexFunc], A, B, Y, (int)Math.Floor(N));
                    break;
                case "Усовершенствованый метод Эйлера":
                    _solveDiff = DifferentialEquations.ImprovedEuler(funcsDiff[indexFunc], A, B, Y, (int)Math.Floor(N));
                    break;
                case "Метод Рунге-Кутта":
                    solveRungeKutta = DifferentialEquations.RungeKutta(funcsDiff[indexFunc], X, Y, N, A);
                    break;
            }

            if (theme == "Метод Рунге-Кутта")
            {
                textBox23.Text = solveRungeKutta.ToString();
            }
            else
            {
                dataGridView3.Rows.Clear();
                for (int i = 0; i < _solveDiff[0].Length; i++)
                {
                    dataGridView3.Rows.Add(new string[] { _solveDiff[0][i].ToString(), _solveDiff[1][i].ToString() });
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string abc = "abcde";
            int countRows = dataGridView4.RowCount-1;
            double[] Xi = new double[countRows];
            double[] Yi = new double[countRows];

            for(int i = 0; i < countRows; i++)
            {
                Xi[i] = double.Parse(ChecDoubleSyntax(dataGridView4.Rows[i].Cells[0].Value.ToString()));
                Yi[i] = double.Parse(ChecDoubleSyntax(dataGridView4.Rows[i].Cells[1].Value.ToString()));
            }

            double[] solution;

            if(theme == "Линейная зависимость")
            {
                solution = LeastSquareMethod.Linear(new double[][] { Xi, Yi });
            }
            else
            {
                solution = LeastSquareMethod.Quadratic(new double[][] { Xi, Yi });
            }

            textBox24.Text = "";

            for(int i = 0; i < solution.Length; i++)
            {
                textBox24.Text += abc[i] + " = " + solution[i].ToString() + "   ";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            double[,] _defoult = new double[2, 9]
            {
                {    0,    4,   10,   15,   21,   29,   36,    51,    68 },
                { 66.7, 71.0, 76.3, 80.6, 85.7, 92.9, 99.4, 113.6, 125.1 }
            };

            dataGridView4.Rows.Clear();

            for (int i = 0; i < 9; i++)
            {
                dataGridView4.Rows.Add(new string[] { _defoult[0,i].ToString(), _defoult[1,i].ToString() });
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            double[,] _defoult = new double[2, 8]
            {
                {    0,    2,    4,    5,    8,   10,   12,   15 },
                { 29.8, 22.9, 17.1, 15.1, 10.7, 10.1, 10.6, 15.2 }
            };

            dataGridView4.Rows.Clear();

            for (int i = 0; i < 8; i++)
            {
                dataGridView4.Rows.Add(new string[] { _defoult[0, i].ToString(), _defoult[1, i].ToString() });
            }
        }
    }
}
