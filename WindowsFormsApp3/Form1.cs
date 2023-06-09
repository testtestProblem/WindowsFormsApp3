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
        int getDataType = 0;
        private Thread t, sendData;

        private static string folder = System.Environment.CurrentDirectory;
        private static string fileName = "\\batteryInformation.txt";
        // Fullpath. 
        string fullPath = folder + fileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy/MM/dd_HH:mm:ss");
        }

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

                        String timeStamp = GetTimestamp(DateTime.Now);
                        label5.Text = timeStamp;
                        File.AppendAllText(fullPath, Environment.NewLine + "time -> " + timeStamp);

                        Array.Resize(ref buffer, length);

                        if (getDataType == 0)
                        {
                            string buf = Encoding.ASCII.GetString(buffer);
                            label1.Text += buf;

                            // Add text to file
                            File.AppendAllText(fullPath, Environment.NewLine + buf);
                            //File.AppendAllText(fullPath, Environment.NewLine + string.Join(" ", buffer));
                        }
                        else if (getDataType == 1)
                        {
                            string buf = Encoding.ASCII.GetString(buffer);
                            label1.Text +=  buf;

                            // Add text to file
                            File.AppendAllText(fullPath, Environment.NewLine + buf);
                            //File.AppendAllText(fullPath, Environment.NewLine + string.Join(" ", buffer));
                        }
                        else if (getDataType == 2)
                        {
                            if (buffer[1] == 0x04 || buffer[1] == 0x20 || buffer[1] == 0x00 || buffer[1] == 0xDD)
                            {
                                label1.Text += "battery " + (batteryIndex).ToString() + ": " + "no battery\n";
                                File.AppendAllText(fullPath, Environment.NewLine + label1.Text);
                            }
                            else
                            {
                                if ((batteryState) == 0x30)
                                {
                                    label1.Text += "battery " + (batteryIndex).ToString() + ": " + buffer[2].ToString() + "% power\n";
                                    File.AppendAllText(fullPath, Environment.NewLine + label1.Text);
                                }
                                else if ((batteryState) == 0x31)
                                {
                                    label1.Text += "battery " + (batteryIndex).ToString() + ": " + ((int)buffer[2] + (((int)buffer[3]) << 8)).ToString() + "mV\n";
                                    File.AppendAllText(fullPath, Environment.NewLine + label1.Text);
                                }
                                else if ((batteryState) == 0x32)
                                {
                                    label1.Text += "battery " + (batteryIndex).ToString() + ": " + ((int)buffer[2] + (((int)buffer[3]) << 8)).ToString() + "mA\n";
                                    File.AppendAllText(fullPath, Environment.NewLine + label1.Text);
                                }
                            }
                        }
                        File.AppendAllText(fullPath, Environment.NewLine + "------------------------");
                        Array.Resize(ref buffer, 1024);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //open serial port
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

                    sendData = new Thread(DoSend);
                    sendData.Start();

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

        //read BIOS BOM
        public void testCommand()
        {
            getDataType = 0;
        }

        Byte batteryIndex = 0, batteryIndexCks = 0x1b, batteryState = 0x30;
        void DoSend()
        {
            switch (getDataType) 
            {
                case 0:
                    label1.Text = "";
                    label1.Text += "BIOS BOM: ";

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

                    Thread.Sleep(1000);
                    getDataType = -1;
                    break;

                case 1:
                    label1.Text = "";
                    label1.Text += "BIOS product name: ";
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

                    Thread.Sleep(1100);
                    getDataType = -1;
                    break;

                case 2:     //get battery state
                    batteryIndex = 0;
                    batteryIndexCks = 0x1b;
                    batteryState = 0x30;

                    label1.Text = "";
                    getDataType = 2;
                    for (int i = 0; i < 12; i++)
                    {
                        try
                        {   //                          Length  Cmd     index       battery         checksum
                            Byte[] buffer = new Byte[5] { 0x05, 0xB0, batteryState, batteryIndex, batteryIndexCks };
                            My_SerialPort.Write(buffer, 0, buffer.Length);
                        }
                        catch (Exception ex)
                        {
                            CloseComport();
                            MessageBox.Show(ex.Message);
                        }
                        Thread.Sleep(550);

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

                    getDataType = -1;
                    break;

                default:
                    //do nothing
                    break;

            }
            
        }
       
        //read battery state
        private void button2_Click(object sender, EventArgs e)
        {
            getDataType = 2;
        }

        //clean UI
        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "";
        }


        //read BIOS product name
        private void testCommand1_Click(object sender, EventArgs e)
        {
            getDataType = 1;
        }
    }
}
