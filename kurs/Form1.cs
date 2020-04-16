using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs
{
    public partial class Form1 : Form
    {
        ManualResetEvent _event;
        DataGridViewCellStyle columnHeaderStyle;
        bool isSetNewPath = false;
        string path = @"C:\Users\UserX\source\repos\kurs\recipes.txt";

        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            this._event = new ManualResetEvent(true);

            columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);

            this.viewRecipes.ColumnCount = 3;
            this.viewRecipes.ColumnHeadersVisible = true;
            this.viewRecipes.RowHeadersVisible = false;

            this.viewRecipes.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            this.Column3.Width = 200;
            this.Column3.Width = this.viewRecipes.Width - this.Column2.Width;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == null || string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                MessageBox.Show("Введи путь к файлу для сохранения рецептов");
            }
            else
            {
                string pattern = @"(.*)\\.*\.txt";
                Regex rg = new Regex(pattern);
                MatchCollection matched = rg.Matches(this.textBox1.Text);
                if (matched.Count != 0 && Directory.Exists(matched[0].Groups[1].Value))
                {
                    this.path = @"" + this.textBox1.Text;
                    this.textBox1.Text = null;
                    this.isSetNewPath = true;
                    MessageBox.Show("Путь к файлу для сохранения рецептов успешно задан");
                }
                else
                {
                    MessageBox.Show("Папки в которой вы хотите сохранять файлы не существует");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (!isSetNewPath)
            {
                MessageBox.Show("Поскольку путь к файлу не задан, то будет использоваться стандартный файл recipes.txt");
            }

            Form2 formCreated = new Form2(this.path, this.viewRecipes);
            formCreated.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        string pattern = @"^title: (.*); description: (.*);";
                        Regex rg = new Regex(pattern);

                        int counter = 0;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            counter++;
                            MatchCollection matched = rg.Matches(line);
                            if (matched.Count != 0)
                            {
                                viewRecipes.Rows.Add(counter, matched[0].Groups[1].Value, matched[0].Groups[2].Value);
                            }
                        }  
                    }

                    this.button4.Visible = true;
                    this.button5.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (viewRecipes.Rows.Count >= 1)
            {
                Form3 formEdit = new Form3(this.path, this.viewRecipes);
                formEdit.Show();
            }
            else
            {
                MessageBox.Show("Данные не загружены");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (viewRecipes.Rows.Count >= 1)
            {
                Form4 formDeleting = new Form4(this.path, this.viewRecipes);
                formDeleting.Show();
            }
            else
            {
                MessageBox.Show("Данные не загружены");
            }
        }
    }
}
