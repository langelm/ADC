using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(PortDataReceivedEvent);
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void PortDataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {
            ProgressBar[] MyprogressBar = GetProgressBar();
            byte[] Data = new byte[serialPort1.BytesToRead];
            serialPort1.Read(Data, 0, Data.Length);
            foreach (byte MyData in Data)
            {
                for (int i = 0; i < 10; i++)
                {
                    MyprogressBar[10 - i].Value = MyprogressBar[10 - i - 1].Value;
                }
                progressBar1.Value = (int)MyData;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(482, 195);
        }
        ProgressBar[] GetProgressBar()
        {
            return new ProgressBar[]
            {
                progressBar1,progressBar2,progressBar3,progressBar4,progressBar5,
                progressBar6,progressBar7,progressBar8,progressBar9,progressBar10,
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                groupBox2.Visible = false;
                this.Size = new Size(482, 195);
                serialPort1.Close();
                button1.Text = "打开串口";
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    groupBox2.Visible = true;
                    this.Size = new Size(482, 512);
                    button1.Text = "关闭串口";

                }
                catch (Exception)
                {
                    MessageBox.Show("串口打开错误", "错误");
                }
            }
        }
    }
}
