using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Arduino_Console
{
    public partial class Form1 : Form
    {
        private string veri;
        public Form1()
        {
            InitializeComponent();
        }
        void portlar()
        {
            string[] portlar = SerialPort.GetPortNames();
            foreach (string Port in portlar)
            {
                comboBox1.Items.Add(Port);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
         
              
            portlar();
            button1.Enabled = false;
            button3.Enabled = false;
            textBox1.ReadOnly = true;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
        }
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            veri = serialPort1.ReadLine();                      
            this.Invoke(new EventHandler(displayData_event));
        }
        private void displayData_event(object sender, EventArgs e)
        {      
            switch (cbdurum)
            {
                case 1:
                    textBox1.Text +=DateTime.Now.ToString() + "|  -Aygıt- " + veri + Environment.NewLine;
                    break;
                case 0:
                    textBox1.Text += "-Aygıt- " + veri + Environment.NewLine;
                    break ;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null || comboBox2.Text != "")
            {
                
                try
                {
                    if(maskedTextBox1.Text != "")
                    {
                        serialPort1.WriteTimeout = Convert.ToInt32(maskedTextBox1.Text);
                    }
                    else
                    {
                        serialPort1.WriteTimeout = -1;
                    }
                    if (maskedTextBox2.Text != "")
                    {
                        serialPort1.ReadTimeout = Convert.ToInt32(maskedTextBox2.Text);
                    }
                    else
                    {
                        serialPort1.ReadTimeout = -1;
                    }

                    int baud = Convert.ToInt32(comboBox2.SelectedItem);
                    serialPort1.BaudRate = baud;
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.Open();
                    button2.Enabled = false;
                    button3.Enabled = true;
                    label3.Text = "Bağlandı";
                    label3.ForeColor = Color.Green;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Karta Bağlanılamadı Lütfen Doğru Bağladığınızdan Emin Olun !");
                }
            }
            else
            {
                if(comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen Port Seçiniz !");
                }
                else if (comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen Baud Rate Seçiniz !");
                }
            }
         
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = false;
            serialPort1.Close();
            button1.Enabled = false;
            label3.Text = "Bağlı Değil !";
            label3.ForeColor = Color.Red;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                switch (cbdurum)
                {
                    case 1:
                        textBox1.Text += DateTime.Now.ToString() + "|  -Siz- " + textBox2.Text + Environment.NewLine;
                        break;
                    case 0:
                        textBox1.Text += "-Siz- " + textBox2.Text + Environment.NewLine;
                        break ;
                }
                serialPort1.Write(textBox2.Text);
            }
            else
            {
                MessageBox.Show("Lütfen Mesaj Girin !");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            portlar();
        }
        int cbdurum = 0;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
            switch (checkBox1.Checked)
            {
                case true: cbdurum = 1;
                    break;
                case false:
                    cbdurum = 0;
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int baud = Convert.ToInt32(comboBox2.SelectedItem);
            serialPort1.BaudRate = baud;
        }
      
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            switch (checkBox2.Checked)
            {
                case true:
                    label5.Visible = true;
                    label6.Visible = true;
                    maskedTextBox1.Visible = true;
                    maskedTextBox2.Visible = true;
                    break;
                case false:
                    label5.Visible = false;
                    label6.Visible = false;
                    maskedTextBox1.Visible = false;
                    maskedTextBox2.Visible = false;
                    break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            serialPort1.WriteTimeout = Convert.ToInt32(maskedTextBox1.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            serialPort1.ReadTimeout = Convert.ToInt32(maskedTextBox2.Text);
        }
    }
}
