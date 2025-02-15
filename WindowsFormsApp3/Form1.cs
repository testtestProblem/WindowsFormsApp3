﻿#define EXCEL_DISABLE
#define TEXT_DISABLE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#if EXCEL_ENABLE
using GemBox.Spreadsheet;
using GemBox.Spreadsheet.Tables;
#endif

namespace WindowsFormsApp3
{
    public partial class S101TG_GCS : Form
    {
        private SerialPort My_SerialPort;
        private bool Console_receiving = false;
        int getDataType = -1;
        private Thread receiveData, sendData;

        public delegate void UpdateViewGrid(object a, object b, object c, object d);

        private static string folder = Environment.CurrentDirectory;
        private static string fileName = "\\batteryInformation.txt";
        private static string fileName2 = "\\batteryInformation" + (GetTimestamp(DateTime.Now).Replace("/", "")).Replace(":", "") + ".txt";
        string fullPath = folder + fileName2;    // Fullpath

        Byte batteryIndex = 0, batteryIndexCks = 0x1b, batteryState = 0x30;

        int batterStatecounter = 0;
        string[] batterStateC = new string[3];
        string batteryDetect = "";
        string battryStateTemp = "";
        int batteryError = 0;
        int batteryReadError = 0;

        int[] preBatteryPwVolAmp = new int[3] { 0, 0, 0 };
#if EXCEL_ENABLE
        ExcelFile workbook;
        ExcelWorksheet[] worksheet = new ExcelWorksheet[4];
        int excelRowIndex = 1;
#endif
        private DataTable sampleData()
        {
            using (DataTable table = new DataTable())
            {
                /*
                // Add rows.
                table.Rows.Add();
                table.Rows.Add();
                table.Rows.Add();
                */
                return table;
            }
        }
        public S101TG_GCS()
        {
            InitializeComponent();
            //dataGridView1.DataSource = sampleData();
            /*
            dataGridView2.Columns[0].Name = "Release Date";
            dataGridView2.Columns[1].Name = "1 Date";
            dataGridView2.Columns[2].Name = "2 Date";
            dataGridView2.Columns[3].Name = "3 Date";
            */
            dataGridView1.Rows.Add("aa", "bb", "cc", "dd");

            //can't choose target index 
            dataGridView2.Rows.Add("", "", "", "", "");
            //dataGridView2.Rows.Add("", "", "", "");
            //dataGridView2.Rows.Add("", "", "", "");
            //dataGridView2.Rows.Add("", "", "", "");
            //dataGridView2.Rows.
            //dataGridView2.Columns[0].DisplayIndex = 0;
        }
        public bool ClosedByXButtonOrAltF4 { get; private set; }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
#if EXCEL_ENABLE
            if (ClosedByXButtonOrAltF4)
            MessageBox.Show("Closed by X or Alt+F4");
            else
            {
                for (int j = 0; j < 4; j++)
                    for (int i = 0; i < 4; i++)
                        worksheet[j].Columns[i].SetWidth(144, LengthUnit.Pixel); //Doing this will shrink UI, I don't know reason

                workbook.Save("batteryState.xlsx");
                //MessageBox.Show("Closed by calling Close()");
            }
#endif
        }


        private void Form1_Load(object sender, EventArgs e)
        {
#if EXCEL_ENABLE
            // If using the Professional version, put your serial key below.
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            // var worksheet = workbook.Worksheets.Add("Tables");
            workbook = new ExcelFile();
            worksheet[0] = workbook.Worksheets.Add("Battery 0");
            worksheet[1] = workbook.Worksheets.Add("Battery 1");
            worksheet[2] = workbook.Worksheets.Add("Battery 2");
            worksheet[3] = workbook.Worksheets.Add("Battery 3");

            var data = new object[1, 4] { { "Power(%)", "Votage(mV)", "Ampere(mA)", "Time" } };
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                {
                    worksheet[j].Cells[0, i].Value = data[0, i];
                    //worksheet[j].Columns[i].SetWidth(144, LengthUnit.Pixel); //Doing this will change ui
                }
#endif
        }

        static public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy/MM/dd_HH:mm:ss");
        }

        public void GetTimestampLabel()
        {
            String timeStamp = GetTimestamp(DateTime.Now);
            label_time.Text = timeStamp;
#if TEXT_ENABLE
            File.AppendAllText(fullPath, Environment.NewLine + "time: " + timeStamp);
#endif
        }
#if TEXT_ENABLE
        public void recordData2txt(string data)
        {
            File.AppendAllText(fullPath, Environment.NewLine + data);
            File.AppendAllText(fullPath, Environment.NewLine + "------------------------");
        }
#endif
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
                            if (length == 5) 
                            {
                                if (buffer[3] == 0x3C && buffer[2] == 0xF1)
                                {
                                    battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + "no battery\n";
                                    batterStateC[batteryIndex] = "error";
                                    batteryError = 1;
                                }
                                else
                                {
                                    if ((buffer[1]) == 0x01 || (buffer[1]) == 0x09)    //read battery remain power
                                    {
                                        //     label1.Text += "battery " + (batteryIndex).ToString() + ": " + buffer[2].ToString() + "% power\n";
                                        battryStateTemp += "battery " + "remain power" + ": " + buffer[2].ToString() + "% battery level\n";
                                        batterStateC[0] = buffer[2].ToString();

                                        preBatteryPwVolAmp[0] = (int)buffer[2];
#if EXCEL_ENABLE
                                    worksheet[batteryIndex].Cells[excelRowIndex, 0].Value = batterStateC[batterStatecounter];
#endif
                                    }
                                    else if ((buffer[1]) == 0x02 || (buffer[1]) == 0x0A)   //read battery voltage
                                    {
                                        int temp_v;
                                        temp_v = ((int)buffer[2] + (((int)buffer[3]) << 8));

                                        //label1.Text += "battery " + (batteryIndex).ToString() + ": " + ((int)buffer[2] + (((int)buffer[3]) << 8)).ToString() + "mV\n";
                                        battryStateTemp += "battery " + "voltage" + ": " + temp_v.ToString() + "mV\n";
                                        batterStateC[1] = temp_v.ToString();

                                        preBatteryPwVolAmp[1] = temp_v;
#if EXCEL_ENABLE
                                    worksheet[batteryIndex].Cells[excelRowIndex, 1].Value = batterStateC[batterStatecounter];
#endif
                                        if (temp_v > 20000)
                                        {
                                            batterStateC[1] = "error";
                                            batteryError = 1;
                                        }
                                    }
                                    else if ((buffer[1]) == 0x03 || (buffer[1]) == 0x0B)   //read battery ampere
                                    {
                                        int temp_a;
                                        temp_a = ((int)buffer[2] + (((int)buffer[3]) << 8));

                                        //    label1.Text += "battery " + (batteryIndex).ToString() + ": " + ((int)buffer[2] + (((int)buffer[3]) << 8)).ToString() + "mA\n";
                                        battryStateTemp += "battery " + "ampere" + ": " + temp_a.ToString() + "mA\n";
                                        batterStateC[2] = temp_a.ToString();

                                        preBatteryPwVolAmp[2] = temp_a;
#if EXCEL_ENABLE
                                    worksheet[batteryIndex].Cells[excelRowIndex, 2].Value = batterStateC[batterStatecounter];
#endif
                                        if (temp_a > 20000) batteryError = 1;
                                    }
                                    if ((buffer[1]) == 0x00 || (buffer[1]) == 0x08)    //read battery remain power
                                    {
                                        //     label1.Text += "battery " + (batteryIndex).ToString() + ": " + buffer[2].ToString() + "% power\n";
                                        battryStateTemp += "battery " + "detect" + ": " + buffer[2].ToString() + "% battery level\n";
                                        batteryDetect = (buffer[2] == 1) ? "Exist" : "NaN";

                                        preBatteryPwVolAmp[0] = (int)buffer[2];
#if EXCEL_ENABLE
                                    worksheet[batteryIndex].Cells[excelRowIndex, 0].Value = batterStateC[batterStatecounter];
#endif
                                    }
                                }
                                //Trace.WriteLine(battryStateTemp);

                            }
                            else
                            {
#if TEXT_ENABLE
                                File.AppendAllText(fullPath, Environment.NewLine + "error:" + batteryIndex.ToString() + " length:" + length.ToString());
                                for (int i = 0; i < length; i++) File.AppendAllText(fullPath, " byte" + i.ToString() + ":" + buffer[i].ToString());
                                File.AppendAllText(fullPath, Environment.NewLine + "------------------------");
#endif
                                //batterStateC[batteryIndex - 1] = preBatteryPwVolAmp[batteryIndex - 1];
                                //batterStatecounter = batteryIndex-1;
                            }
                            /*
                            //batterStatecounter++;
                            if (batterStatecounter == (batteryIndex - 1))
                            {
                                batterStatecounter = 0;
                                UpdateViewGrid testUpdateViewGrid = new UpdateViewGrid(updateViewGrid);

                                if (batteryError == 0)
                                {
                                    //this.BeginInvoke(testUpdateViewGrid, new Object[] { (int)batteryIndex, batterStateC[0], batterStateC[1], batterStateC[2] });
                                }
                                else if (batteryError == 1) 
                                {
                                    this.BeginInvoke(testUpdateViewGrid, new Object[] { (int)batteryIndex, "NA", "NA", "NA" });
                                    batteryError = 0;
                                }
#if EXCEL_ENABLE
                                worksheet[batteryIndex].Cells[excelRowIndex, 3].Value = GetTimestamp(DateTime.Now);
#endif
                            }*/
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0 &&
            dataGridView2.SelectedRows[0].Index !=
            dataGridView2.Rows.Count - 1)
            {
                dataGridView2.Rows.RemoveAt(
                    dataGridView2.SelectedRows[0].Index);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
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
                            MessageBox.Show("Please checking power and serial line are connected!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            System.Environment.Exit(0);
                            break;
                        }

                        label_biosName.Text = "";
                        getDataType = 1;                            //parse method
                        serialWrite(0x04, 0xA0, 0x01, 0x5b);        //BIOS name 
                        Thread.Sleep(500);                          //wait for receive data

                        getDataType = -1;                           //do nothing

                        GetTimestampLabel();
#if TEXT_ENABLE
                        File.AppendAllText(fullPath, Environment.NewLine + label6.Text + " " + lable_biosBom.Text);
                        File.AppendAllText(fullPath, Environment.NewLine + label8.Text + " " + label_biosName.Text);
                        File.AppendAllText(fullPath, Environment.NewLine + "------------------------");
#endif
                        break;

                    case 101:    

                        break;

                    case 102:     //get battery state
                        
                        batteryIndexCks = 0x1b;
                        batteryState = 0x00;

                        //this.dataGridView1.DataSource = null;
                        //this.dataGridView1.Rows.Clear();

                        getDataType = 2;        //parse method

                        batteryIndex = 0;
                        serialWrite(0x04, 0xB0, batteryIndex, 0x4C);     //read battery detect
                        Thread.Sleep(600);          //wait for receive data

                        batteryIndex = 1;
                        //         Length Cmd   index        checksum
                        serialWrite(0x04, 0xB0, batteryIndex, 0x4B);     //read battery remain power
                        Thread.Sleep(600);          //wait for receive data

                        //batteryIndex++;
                        batteryIndex=2;
                        //         Length Cmd   index        checksum
                        serialWrite(0x04, 0xB0, batteryIndex, 0x4A);    //read battery voltage
                        Thread.Sleep(600);          //wait for receive data

                        //batteryIndex++;
                        batteryIndex=3;
                        //         Length Cmd   index        checksum
                        serialWrite(0x04, 0xB0, batteryIndex, 0x49);    //read battery ampere
                        Thread.Sleep(600);          //wait for receive data
                        
                        //for test
                        //UpdateViewGrid testUpdateViewGrid = new UpdateViewGrid(updateViewGrid);
                        
                        //Trace.WriteLine("remain power>" + batterStateC[0] + " Voltage>" + batterStateC[1] + " ampere>" + batterStateC[2]);

                        //this.BeginInvoke(testUpdateViewGrid, new Object[] { 0, batterStateC[0], batterStateC[1], batterStateC[2] });
                        /*
                        dataGridView1.Rows.Clear();
                        dataGridView2.Rows[0].SetValues(new object[] { "aa", batterStateC[0], batterStateC[1], batterStateC[2] });
                        dataGridView1.Rows.Add("0", batterStateC[0], batterStateC[1], batterStateC[2]);*/
                        //dataGridView2.Rows.Clear();

                        dataGridView2.Rows[0].SetValues(new object[] { "0", batteryDetect, batterStateC[0], batterStateC[1], batterStateC[2] });

                        //dataGridView2.Rows.Add("aa", batterStateC[0], batterStateC[1], batterStateC[2]);
                        batteryDetect = "";
                        batterStateC[0] = "";
                        batterStateC[1] = "";
                        batterStateC[2] = "";

                        //second battery state

                        batteryIndex = 0;
                        serialWrite(0x04, 0xB0, 0x08, 0x44);     //read battery detect
                        Thread.Sleep(600);          //wait for receive data

                        batteryIndex = 1;
                        //         Length Cmd   index        checksum
                        serialWrite(0x04, 0xB0, 0x09, 0x43);     //read battery remain power
                        Thread.Sleep(600);          //wait for receive data

                        batteryIndex = 2;
                        //         Length Cmd   index        checksum
                        serialWrite(0x04, 0xB0, 0x0A, 0x42);    //read battery voltage
                        Thread.Sleep(600);          //wait for receive data

                        batteryIndex = 3;
                        //         Length Cmd   index        checksum
                        serialWrite(0x04, 0xB0, 0x0B, 0x41);    //read battery ampere
                        Thread.Sleep(600);          //wait for receive data

                        dataGridView2.Rows[1].SetValues(new object[] { "1", batteryDetect, batterStateC[0], batterStateC[1], batterStateC[2] });

                        batterStateC[0] = "";
                        batterStateC[1] = "";
                        batterStateC[2] = "";

                        getDataType = -1;
#if EXCEL_ENABLE
                        excelRowIndex++;
#endif
                        GetTimestampLabel();
#if TEXT_ENABLE
                        recordData2txt(battryStateTemp);
#endif
                        battryStateTemp = "";

                        break;

                    default:
                        //do nothing
                        break;
                }

                if (comboBox_reflashRate.Text == "Normal")
                {
                    Thread.Sleep(1200);  
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
#if TEXT_ENABLE
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
#endif
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
