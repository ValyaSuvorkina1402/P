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
    public partial class Form4 : Form
    {
        string path;
        System.Windows.Forms.DataGridView viewRecipes;

        public Form4(string recipesFilePath, System.Windows.Forms.DataGridView viewRecipes)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.path = recipesFilePath;
            this.viewRecipes = viewRecipes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text != null)
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
                                    currentText += "\n";
                                else
                                {
                                    currentText += line;
                                }   
                            }

                            //TOD: удаление данных о рецепте в гриде
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

                MessageBox.Show("Удалено");

                this.Close();
            }
            else
            {
                MessageBox.Show("Порядковый номер не введен");
            }
        }
    }
}
