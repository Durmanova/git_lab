using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace lab8
{
    public partial class Form1 : Form
    {
        RegistryKey key = Registry.CurrentUser.CreateSubKey("Lab8", true);
        public void OpenFD()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            fileName = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(fileName);
            richTextBox1.Text = fileText;
            key.SetValue("FilePath", fileName);
            MessageBox.Show("Файл открыт");
        }
        public void SaveFD()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, richTextBox1.Text);
            key.OpenSubKey("Lab8", true);
            key.SetValue("FilePath", fileName);
            key.Close();
            MessageBox.Show("Файл сохранен");
        }
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            key.OpenSubKey("Lab8", true);
        }
        string fileName;
        private void Form1_Load(object sender, EventArgs e)
        {
            key.OpenSubKey("Lab8", true);
                fileName = key.GetValue("FilePath") as string;
                if (fileName != null && File.Exists(fileName))
                {
                    richTextBox1.Text = File.ReadAllText(fileName);
                }
                else OpenFD();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Файл сохранен");
            if (fileName != "")
            {
                System.IO.File.WriteAllText(fileName, richTextBox1.Text);
            }
            else
            {
                SaveFD();
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFD();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            fileName = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(fileName, richTextBox1.Text);
            key.SetValue("FilePath", fileName);
            MessageBox.Show("Файл сохранен");

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            key.DeleteValue("FilePath");
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void вернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void очиститьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            key.Close();
        }
    }
}
