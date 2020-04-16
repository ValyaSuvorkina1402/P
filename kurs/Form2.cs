using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs
{
    public partial class Form2 : Form
    {
        string path;
        System.Windows.Forms.DataGridView viewRecipes;

        public Form2(string recipesFilePath, System.Windows.Forms.DataGridView viewRecipes)
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
                string newText = "title: " + this.textBox1.Text + "; description: " + this.textBox2.Text + ";";
                string text = "";
                try
                {
                    if (File.Exists(path))
                    {
                        using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                        {
                            text = sr.ReadToEnd().ToString() + newText;
                        }

                        using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                MessageBox.Show("Рецепт создан");

                this.viewRecipes.Rows.Add("?", this.textBox1.Text, this.textBox2.Text);

                this.Close();
            }
            else
            {
                MessageBox.Show("Введите данные");
            }
        }
    }
}
