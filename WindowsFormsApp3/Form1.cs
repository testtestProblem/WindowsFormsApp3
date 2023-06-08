using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private SerialPort My_SerialPort;
        private bool Console_receiving = false;
        private Thread t;

        private static string folder = System.Environment.CurrentDirectory;
        private static string fileName = "\\batteryInformation.txt";
        // Fullpath. 
        string fullPath = folder + fileName;
        //File.WriteAllLines(fullPath, " ");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        //  ...later on in the code
        //String timeStamp = GetTimestamp(new DateTime());

        // 接收資料
        private void DoReceive()
        {
            Byte[] buffer = new Byte[32];

            try
            {
                while (Console_receiving)
                //while (My_SerialPort.DataReceived)
                {
                    if (My_SerialPort.BytesToRead > 0)
                    {
                        Int32 length = My_SerialPort.Read(buffer, 0, buffer.Length);
                        
                        Array.Resize(ref buffer, length);
                        string buf = Encoding.ASCII.GetString(buffer);
                        label1.Text += buf;
                        label6.Text += string.Join(" ", buffer);
                        label6.Text += "\n";
                        /*
                        Array.Resize(ref buffer, length);
                        Display d = new Display(ConsoleShow);
                        this.Invoke(d, new Object[] { buf });
                        Array.Resize(ref buffer, 1024);
                        */

                        String timeStamp = GetTimestamp(DateTime.Now);
                        label5.Text = timeStamp;

                        // Add text to file
                        File.AppendAllText(fullPath, Environment.NewLine + "time -> " + timeStamp);
                        File.AppendAllText(fullPath, Environment.NewLine + buf);
                        File.AppendAllText(fullPath, Environment.NewLine + string.Join(" ", buffer));
                        File.AppendAllText(fullPath, Environment.NewLine + "------------------------");
                        
                        Array.Resize(ref buffer, 1024);
                    }
                    Thread.Sleep(20);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Console 發送資料
        public void SendData(Object sendBuffer)
        {
            if (sendBuffer != null)
            {
                Byte[] buffer = sendBuffer as Byte[];

                try
                {
                    My_SerialPort.Write(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    CloseComport();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //關閉 Console
        public void CloseComport()
        {
            try
            {
                My_SerialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Console_receiving = false;
            t.Abort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            My_SerialPort = new SerialPort();

            if (My_SerialPort.IsOpen)
            {
                CloseComport();
            }

            //設定 Serial Port 參數
            My_SerialPort.PortName = comboBox1.Text;
            My_SerialPort.BaudRate = 9600;
            My_SerialPort.DataBits = 8;
            My_SerialPort.StopBits = StopBits.One;

            if (!My_SerialPort.IsOpen)
            {
                int error = 0;

                //開啟 Serial Port
                try
                {
                    My_SerialPort.Open();
                }
                catch (Exception ex)
                {
                    error = 1;
                    MessageBox.Show(ex.Message);
                }

                if (error == 0)
                {
                    Console_receiving = true;

                    //開啟執行續做接收動作
                    t = new Thread(DoReceive);
                    t.IsBackground = true;
                    t.Start();

                    testCommand();
                }
            }

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fullPath))
                {
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }

        public void testCommand()
        {
            try
            {
                Byte[] buffer = new Byte[4] { 0x04, 0xA0, 0x00, 0x5c };
                My_SerialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                CloseComport();
                MessageBox.Show(ex.Message);
            }
        }

        Byte batteryIndex =0, batteryIndexCks=0x1b, batteryState=0x30;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Byte[] buffer = new Byte[5] {/*Length*/ 0x05,/*Cmd*/ 0xB0,/*index*/ batteryState,/*battery*/ batteryIndex, batteryIndexCks };
               My_SerialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                CloseComport();
                MessageBox.Show(ex.Message);
            }

            label4.Text = "battery index: " + batteryIndex.ToString();

            batteryIndex++;
            batteryIndexCks--;

            if (batteryIndex == 4)
            {
                batteryIndex = 0;
                // batteryIndexCks = 0x1b;
                batteryIndexCks = (byte)(batteryIndexCks + 0x03);

                batteryState++;
                if (batteryState == 0x33) batteryState = 0x30;

               // batteryIndexCks = (byte)(batteryIndexCks - (batteryState - 0x30) - 1); //batteryState is base 0x30
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label6.Text = "";
        }

        private void testCommand1_Click(object sender, EventArgs e)
        {
            try
            {
                Byte[] buffer = new Byte[4] { 0x04, 0xA0, 0x01, 0x5b };
                My_SerialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                CloseComport();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
