using System;
using System.Windows;
using System.Windows.Controls;

namespace CalculadoraCientifica
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private float num1;
        private float num2;
        private string operacionActual = "";
        private bool nuevoNumero = true;
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button pulsar = (Button)sender;
            string textoBoton = pulsar.Content.ToString();

            if (textoBoton == "C")
            {
                txtResultado.Text = "0";
                num1 = 0;
                num2 = 0;
                operacionActual = "";
                nuevoNumero = true;
                return;
            }
            if (textoBoton == "+/-")
            {
                if (float.TryParse(txtResultado.Text, out float numeroActual))
                {
                    numeroActual *= -1;
                    txtResultado.Text = numeroActual.ToString();

                    if (string.IsNullOrEmpty(operacionActual))
                    {
                        num1 = numeroActual;
                    }
                    else
                    {
                        num2 = numeroActual;
                    }
                }
                return;
            }

            if (char.IsDigit(textoBoton[0]) || textoBoton == ",")
            {
                if (nuevoNumero)
                {
                    txtResultado.Text = (textoBoton == ",") ? "0," : "";
                    nuevoNumero = false;
                }

                if (textoBoton == "," && txtResultado.Text.Contains(","))
                    return;

                txtResultado.Text += textoBoton;
                return;
            }

            if (textoBoton == "=")
            {
                if (!string.IsNullOrEmpty(operacionActual) && float.TryParse(txtResultado.Text, out num2))
                {
                    float resultado = Calcular(num1, num2, operacionActual);
                    txtResultado.Text =resultado!=0 ? resultado.ToString(): "ERROR";
                    num1 = resultado;
                    operacionActual = "";
                    nuevoNumero = true;
                }
                return;
            }
            if (float.TryParse(txtResultado.Text, out num1))
            {
                operacionActual = textoBoton;
                nuevoNumero = true;
            }
        }

        private float Calcular(float n1, float n2, string op)
        {
            switch (op)
            {
                case "+":
                    return n1 + n2;
                case "-":
                    return n1 - n2;
                case "*":
                    return n1 * n2;
                case "%":
                    return (n1 * n2) / 100;
                case "/":
                    return n2 != 0 ? n1 / n2 : 0;
                case "**":
                    return (float)Math.Pow(n1, n2);
                default:
                    return n2;
            }
        }
    }
}
