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
        int getDataType = -1;
        private Thread receiveData, sendData;

        public delegate void UpdateViewGrid(object a, object b, object c, object d);

        private static string folder = Environment.CurrentDirectory;
        private static string fileName = "\\batteryInformation.txt";
        string fullPath = folder + fileName;    // Fullpath


        Byte batteryIndex = 0, batteryIndexCks = 0x1b, batteryState = 0x30;

        int batterStatecounter = 0;
        int[] batterStateC = new int[3];
        string battryStateTemp = "";
        int batteryError = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy/MM/dd_HH:mm:ss");
        }
        public void GetTimestampLabel()
        {
            String timeStamp = GetTimestamp(DateTime.Now);
            label_time.Text = timeStamp;
            File.AppendAllText(fullPath, Environment.NewLine + "time: " + timeStamp);
        }
        public void recordData2txt(string data)
        {
            File.AppendAllText(fullPath, Environment.NewLine + data);
            File.AppendAllText(fullPath, Environment.NewLine + "------------------------");
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
            receiveData.Abort();
            sendData.Abort();
        }

        public void updateViewGrid(object a, object b, object c, object d)
        {
            dataGridView1.Rows.Add(a, b, c, d);
        }

        public void serialWrite(byte a, byte b, byte c, byte d)
        {
            try
            {
                Byte[] buffer = new Byte[4] { a, b, c, d };     //BIOS name
                My_SerialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                CloseComport();
                MessageBox.Show(ex.Message);
            }
        }
        public void serialWrite(byte a, byte b, byte c, byte d, byte e)
        {
            try
            {
                Byte[] buffer = new Byte[5] { a, b, c, d, e};     //BIOS name
                My_SerialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                CloseComport();
                MessageBox.Show(ex.Message);
            }
        }

        private void DoReceive()
        {
            Byte[] buffer = new Byte[32];
            try
            {
                while (Console_receiving)
                {
                    if (My_SerialPort.BytesToRead > 0)
                    {
                        Int32 length = My_SerialPort.Read(buffer, 0, buffer.Length);
                        Array.Resize(ref buffer, length);

                        if (getDataType == 0)       //for parse BIOS BOM
                        {
                            string buf = Encoding.ASCII.GetString(buffer);
                            lable_biosBom.Text += buf;
                        }
                        else if (getDataType == 1)  //for parse BIOS name
                        {
                            string buf = Encoding.ASCII.GetString(buffer);
                            label_biosName.Text += buf;
                        }
                        else if (getDataType == 2)  //for parse battery state
                        {
                            if (buffer[1] == 0x04 || buffer[1] == 0x20 || buffer[1] == 0x00 || buffer[1] == 0xDD)
                            {
                                battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + "no battery\n";
                                batterStateC[batterStatecounter] = -1;
                                batteryError = 1;
                            }
                            else
                            {
                                if ((batteryState) == 0x30)
                                {
                                    //     label1.Text += "battery " + (batteryIndex).ToString() + ": " + buffer[2].ToString() + "% power\n";
                                    battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + buffer[2].ToString() + "% power\n";
                                    batterStateC[batterStatecounter] = buffer[2];
                                }
                                else if ((batteryState) == 0x31)
                                {
                                    int temp_v;
                                    temp_v = ((int)buffer[2] + (((int)buffer[3]) << 8));

                                    //label1.Text += "battery " + (batteryIndex).ToString() + ": " + ((int)buffer[2] + (((int)buffer[3]) << 8)).ToString() + "mV\n";
                                    battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + temp_v.ToString() + "mV\n";
                                    batterStateC[batterStatecounter] = ((int)buffer[2] + (((int)buffer[3]) << 8));

                                    if (temp_v > 10000) batteryError = 1;
                                }
                                else if ((batteryState) == 0x32)
                                {
                                    int temp_a;
                                    temp_a = ((int)buffer[2] + (((int)buffer[3]) << 8));

                                    //    label1.Text += "battery " + (batteryIndex).ToString() + ": " + ((int)buffer[2] + (((int)buffer[3]) << 8)).ToString() + "mA\n";
                                    battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + temp_a.ToString() + "mA\n";
                                    batterStateC[batterStatecounter] = ((int)buffer[2] + (((int)buffer[3]) << 8));

                                    if (temp_a > 10000) batteryError = 1;
                                }
                            }

                            batterStatecounter++;
                            if (batterStatecounter == 3)
                            {
                                batterStatecounter = 0;
                                UpdateViewGrid testUpdateViewGrid = new UpdateViewGrid(updateViewGrid);

                                if (batteryError == 0)
                                {
                                    this.BeginInvoke(testUpdateViewGrid, new Object[] { (int)batteryIndex, batterStateC[0], batterStateC[1], batterStateC[2] });
                                }
                                else if (batteryError == 1) 
                                {
                                    this.BeginInvoke(testUpdateViewGrid, new Object[] { (int)batteryIndex, "NA", "NA", "NA" });
                                    batteryError = 0;
                                }
                            }
                        }
                        Array.Resize(ref buffer, 1024);
                    }
                    Thread.Sleep(100);  //for reduce cpu utilization
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void DoSend()
        {
            while (true)
            {
                switch (getDataType)
                {
                    case 100:     //get BIOS BOM and BIOS name
                        
                        lable_biosBom.Text = "";
                        getDataType = 0;                            //parse method
                        serialWrite(0x04, 0xA0, 0x00, 0x5c);        //BIOS BOM
                        Thread.Sleep(750);                          //wait for receive data

                        if (lable_biosBom.Text == "")
                        {
                            MessageBox.Show("Please check power and serial line are connected!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        label_biosName.Text = "";
                        getDataType = 1;                            //parse method
                        serialWrite(0x04, 0xA0, 0x01, 0x5b);        //BIOS name 
                        Thread.Sleep(500);                          //wait for receive data

                        getDataType = -1;                           //do nothing

                        GetTimestampLabel();
                        File.AppendAllText(fullPath, Environment.NewLine + label6 + " " + lable_biosBom.Text);
                        File.AppendAllText(fullPath, Environment.NewLine + label8 + " " + label_biosName.Text);
                        File.AppendAllText(fullPath, Environment.NewLine + "------------------------");

                        break;

                    case 101:    

                        break;

                    case 102:     //get battery state
                        batteryIndex = 0;
                        batteryIndexCks = 0x1b;
                        batteryState = 0x30;

                        this.dataGridView1.DataSource = null;
                        this.dataGridView1.Rows.Clear();

                        getDataType = 2;        //parse method
                        for (int i = 0; i < 12; i++)
                        {
                            //         Length  Cmd     index       battery         checksum
                            serialWrite(0x05, 0xB0, batteryState, batteryIndex, batteryIndexCks);
                            Thread.Sleep(300);          //wait for receive data

                            batteryState++;
                            batteryIndexCks--;

                            if (batteryState == 0x33)
                            {
                                batteryState = 0x30;
                                batteryIndex++;

                                // batteryIndexCks = 0x1b;
                                batteryIndexCks = (byte)(batteryIndexCks + 0x02);

                                if (batteryIndex == 0x04) batteryState = 0x04;
                            }
                        }
                        getDataType = -1;

                        GetTimestampLabel();
                        recordData2txt(battryStateTemp);
                        battryStateTemp = "";

                        break;

                    default:
                        //do nothing
                        break;
                }

                if (comboBox_reflashRate.Text == "Normal")
                {
                    Thread.Sleep(1500);  
                    getDataType = 102;      //get battery state
                }
                else if (comboBox_reflashRate.Text == "Rapid")
                {
                    getDataType = 102;
                }
                else if (comboBox_reflashRate.Text == "Slow")
                {
                    Thread.Sleep(4300);  
                    getDataType = 102;
                }
                else if (comboBox_reflashRate.Text == "Stop") 
                {
                    getDataType = -1;       //do nothing
                }
                Thread.Sleep(100);      //for reducing cpu utilization

            }
        }

        //read BIOS BOM
        public void testCommand()
        {
            getDataType = 100;
        }

        private void button_openSerial_Click(object sender, EventArgs e)
        {
            My_SerialPort = new SerialPort();

            if (My_SerialPort.IsOpen)
            {
                CloseComport();
            }

            //config Serial Port
            My_SerialPort.PortName = comboBox_comPortSelecter.Text;
            My_SerialPort.BaudRate = 9600;
            My_SerialPort.DataBits = 8;
            My_SerialPort.StopBits = StopBits.One;

            if (!My_SerialPort.IsOpen)
            {
                int error = 0;

                //open Serial Port
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
                    try
                    {
                        // Check if file already exists.    
                        if (File.Exists(fullPath))
                        {
                            //File.Delete(fullPath);
                            GetTimestampLabel();
                            recordData2txt("Open serial port");
                        }
                        else
                        {
                            // Create a new file     
                            using (FileStream fs = File.Create(fullPath))
                            {
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    Console_receiving = true;

                    sendData = new Thread(DoSend);
                    sendData.IsBackground = true;
                    sendData.Start();

                    receiveData = new Thread(DoReceive);
                    receiveData.IsBackground = true;
                    receiveData.Start();

                    testCommand();
                }
            }
            else
            {
                MessageBox.Show("Serial port has been opened.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
