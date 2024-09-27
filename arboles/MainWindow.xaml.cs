using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace arboles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    { 
        public BinaryTree<int> thetree=new BinaryTree<int>();
        public BinaryTree<char> thetreec = new BinaryTree<char>();
        public BinaryTreeNode<int> nint;
        public BinaryTreeNode<char> nchar;
        private bool valorActual;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (valorActual)
            {
                try {
                    int n = int.Parse(eletxt.Text);
                    thetree.Insert(n);
                    if (thetree.Root != null)
                    {
                        TheCanva.Children.Clear();
                        DibujarArbol(thetree.Root);
                    }
                }
                catch(Exception ex) {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                try
                {
                    char n = char.Parse(eletxt.Text.ToUpper());
                    thetreec.Insert(n);
                    if (thetreec.Root != null)
                    {
                        TheCanva.Children.Clear();
                        DibujarArbol(thetreec.Root);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }
            eletxt.Text = "";
        }
        public void DibujarArbol<T>(BinaryTreeNode<T> r)
        {
            DibujarNodo(r, TheCanva.ActualWidth / 2, 20, 200);
        }

        public void DibujarNodo<T>(BinaryTreeNode<T> node, double x, double y, double espacioHorizontal)
        {
            if (node == null) return;

            TextBlock textBlock = new TextBlock
            {
                Text = node.Data.ToString(),
                Width = 30,
                TextAlignment = TextAlignment.Center
            };

            Canvas.SetLeft(textBlock, x - textBlock.Width / 2);
            Canvas.SetTop(textBlock, y);
            TheCanva.Children.Add(textBlock);

            double espacioHijo = espacioHorizontal / 2;
            if (node.Left != null)
            {
                DibujarLinea( x, y, x - espacioHijo, y + 50); 
                DibujarNodo(node.Left, x - espacioHijo, y + 50, espacioHijo);
            }
            if (node.Right != null)
            {
                DibujarLinea(x, y, x + espacioHijo, y + 50);
                DibujarNodo(node.Right, x + espacioHijo, y + 50, espacioHijo);
            }
        }

        private void DibujarLinea(double x1, double y1, double x2, double y2)
        {
            Line line = new Line
            {
                X1 = x1,
                Y1 = y1 + 15,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            TheCanva.Children.Add(line);
        }

        private void btnElimi_Click(object sender, RoutedEventArgs e)
        {
            if (valorActual)
            {
                try
                {
                    int n = int.Parse(eletxt.Text);
                    thetree.Delete(n);
                    if (thetree.Root != null)
                    {
                        TheCanva.Children.Clear();
                        DibujarArbol(thetree.Root);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                try
                {
                    char n = char.Parse(eletxt.Text.ToUpper());
                    thetreec.Delete(n);
                    if (thetreec.Root != null)
                    {
                        TheCanva.Children.Clear();
                        DibujarArbol(thetreec.Root);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }
            eletxt.Text = "";
        }

        private void btnBalanc_Click(object sender, RoutedEventArgs e)
        {
            if (valorActual) {
                try
                {
                    thetree.Root= thetree.Balance();
                    if (thetree.Root != null)
                    {
                        thetree._inOrderTraversalResult.Clear();
                        TheCanva.Children.Clear();
                        DibujarArbol(thetree.Root);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                try
                {
                    thetreec.Root = thetreec.Balance();
                    if (thetreec.Root != null)
                    {
                        thetreec._inOrderTraversalResult.Clear();
                        TheCanva.Children.Clear();
                        DibujarArbol(thetreec.Root);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void btnReini_Click(object sender, RoutedEventArgs e)
        {
            thetree.Clear();
            thetreec.Clear();
            TheCanva.Children.Clear();
        }

        private void rbTipo_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;

            if (radio == radbtNumer)
            {
                valorActual = true;
            }
            else if (radio == radbtCarac)
            {
                valorActual = false;
            }
        }
    }
}