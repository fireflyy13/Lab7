using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7TA
{
    public partial class Form1 : Form
    {
        private BinaryTree _BinaryTree;
        private Bitmap _Binarybitmap;
        private Graphics _Binarygraphics;
        public Form1()
        {
            InitializeComponent();
            _BinaryTree = new BinaryTree();
            _Binarybitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _Binarygraphics = Graphics.FromImage(_Binarybitmap);
            InitialTree();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void InitialTree()
        {
            int[] nodes = { 42, 28, 21, 17, 31, 30, 35, 62, 59, 57, 60, 71, 67, 82};

            foreach (int value in nodes)
            {
                _BinaryTree.Add(value);
            }

            DrawTree();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int element))
            {
                MessageBox.Show("Будь ласка, введіть коректне значення " +
                    "кількості злитків!",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (_BinaryTree.Search(element))
            {
                MessageBox.Show("Печера, що містить вказану кількість злитків вже існує!",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _BinaryTree.Add(element);
            DrawTree();
            textBox1.Clear();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox2.Text, out int element))
            {
                MessageBox.Show("Будь ласка, введіть коректне значення " +
                    "кількості злитків!",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!(_BinaryTree.Search(element)))
            {
                MessageBox.Show("Не існує печери, що містить вказану кількість злитків!",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _BinaryTree.Remove(element);
            DrawTree();
            textBox2.Clear();
        }

        private void ShowPath_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out int element))
            {
                MessageBox.Show("Будь ласка, введіть коректне значення елемента" +
                    "кількості злитків!",
                    "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!(_BinaryTree.Search(element)))
            {
                MessageBox.Show($"Шуканої печери з кількістю злитків {element} не існує!" +
                    $" Незабаром її буде створено.",
                    "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            List<int> path = _BinaryTree.FindPath(element);
            string pathString = string.Join(" -> ", path);
            DrawTree();
            label4.Text = ($"Шлях до заданого скарбу: {pathString}");
            textBox3.Clear();
        }

        private void DrawTree()
        {
            _Binarygraphics.Clear(Color.CadetBlue);
            DrawNode(_BinaryTree.root, 300, 20, 150);
            pictureBox1.Image = _Binarybitmap;
        }

        private void DrawNode(TreeNode node, float x, float y, float dx)
        {
            if (node == null)
            {
                return;
            }

            float ellipseWidth = 32; 
            float ellipseHeight = 32;
            Pen ellipsePen = new Pen(Color.Black, 2);
            _Binarygraphics.DrawEllipse(ellipsePen, x, y, ellipseWidth, ellipseHeight);
            _Binarygraphics.FillEllipse(Brushes.White, x, y, ellipseWidth, ellipseHeight);

            SizeF textSize = _Binarygraphics.MeasureString(node.Key.ToString(), 
                new Font("Cascadia Mono", 12));

            float textX = x + (ellipseWidth - textSize.Width) / 2;
            float textY = y + (ellipseHeight - textSize.Height) / 2;

            _Binarygraphics.DrawString(node.Key.ToString(), new Font("Cascadia Mono", 12),
                Brushes.Black, textX, textY);
            Pen widePen = new Pen(Color.Black, 1);

            if (node.left != null)
            {
                _Binarygraphics.DrawLine(widePen, x + ellipseWidth / 2, y + ellipseHeight,
                    x - dx + ellipseWidth / 2, y + ellipseHeight + 20);
                DrawNode(node.left, x - dx, y + 52, dx / 2);
            }

            if (node.right != null)
            {
                _Binarygraphics.DrawLine(widePen, x + ellipseWidth / 2, y + ellipseHeight,
                    x + dx + ellipseWidth / 2, y + ellipseHeight + 20);
                DrawNode(node.right, x + dx, y + 52, dx / 2);
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }
    }
}
