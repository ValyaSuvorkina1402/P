using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace kurs
{
    public partial class Form3 : Form
    {
        string path;
        System.Windows.Forms.DataGridView viewRecipes;

        public Form3(string recipesFilePath, System.Windows.Forms.DataGridView viewRecipes)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.path = recipesFilePath;
            this.viewRecipes = viewRecipes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != null && this.textBox2.Text != null)
            {
                string currentText = "";
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
                                if (counter == Int32.Parse(this.textBox3.Text))
                                {
                                    string editText = "\ntitle: " + this.textBox1.Text + "; description: " + this.textBox2.Text + ";\n";
                                    currentText += editText;

                                    //TOD: замена значений в гриде на новые для этого рецепта
                                    /*MatchCollection matched = rg.Matches(line);
                                    if (matched.Count != 0)
                                    {
                                        
                                    }*/
                                }
                                else
                                {
                                    currentText += line;
                                }
                            }
                        }

                        using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(currentText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                MessageBox.Show("Сохранено");

                this.Close();
            }
            else
            {
                MessageBox.Show("Введите данные");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text != null)
            {
                this.button2.Visible = false;
                this.label3.Visible = false;
                this.textBox3.Visible = false;

                this.button1.Visible = true;
                this.textBox1.Visible = true;
                this.textBox2.Visible = true;

                try
                {
                    if (File.Exists(path))
                    {
                        using (StreamReader sr = new StreamReader(path))
                        {
                            string pattern = @"^title: (.*); description: (.*);";
                            Regex rg = new Regex(pattern);

                            int counter = 0;
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                counter++;
                                if (counter == Int32.Parse(this.textBox3.Text))
                                {
                                    MatchCollection matched = rg.Matches(line);
                                    if (matched.Count != 0)
                                    {
                                        this.textBox1.Text = matched[0].Groups[1].Value;
                                        this.textBox2.Text = matched[0].Groups[2].Value;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Порядковый номер не введен");
            }
        }
    }
}
