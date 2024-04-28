using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LexicalAnalyzer;
using SyntacsycalAnalyzer;
using Struct;

namespace Lab3
{
    public partial class Form1 : Form
    {
        LexicalBlock lb = new LexicalBlock();
        SyntacticalBlock sb = new SyntacticalBlock();
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string Path = openFileDialog1.FileName;
                textBox1.Text = File.ReadAllText(@Path);

            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            lb.tree1.Clear();
            sb.lexemList.Clear();
            sb.nodeList.Clear();
            sb.lastCell = 0;
            sb.structList.Clear();
            sb.k = 0;
            sb.mark = -1;
            sb.jumpFlag = false;
            sb.elseFlag = 0;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            lb.allTextProgram = textBox1.Text;
            lb.AllTextAnalyser();
            sb.RefreshTree(lb.tree1);
            bool flag = sb.SyntacticalAnalyzer();
            if (flag)
            {
                foreach (ClassID.TreeNode item in sb.nodeList)
                {
                    dataGridView1.Rows.Add(item.Name, item.Data.ToString(), 1);

                }
                foreach (Structures item in sb.structList)
                {
                    dataGridView2.Rows.Add(item.ToString(), 1);
                }
            }
            else
            {
                MessageBox.Show("Отвергнуть");
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
