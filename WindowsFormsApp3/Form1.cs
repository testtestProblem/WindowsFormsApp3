#define EXCEL_DISABLE


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
        int[] batterStateC = new int[3];
        string battryStateTemp = "";
        int batteryError = 0;
#if EXCEL_ENABLE
        ExcelFile workbook;
        ExcelWorksheet[] worksheet = new ExcelWorksheet[4];
        int excelRowIndex = 1;
#endif
        public S101TG_GCS()
        {
            InitializeComponent();
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
                            if (buffer[3] == 0x3C && buffer[2] == 0xF1)  
                            {
                                battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + "no battery\n";
                                batterStateC[batterStatecounter] = -1;
                                batteryError = 1;
                            }
                            else 
                            {
                                if ((buffer[1]) == 0x01)    //read battery remain power
                                {
                                    //     label1.Text += "battery " + (batteryIndex).ToString() + ": " + buffer[2].ToString() + "% power\n";
                                    battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + buffer[2].ToString() + "% battery level\n";
                                    batterStateC[batterStatecounter] = buffer[2];
#if EXCEL_ENABLE
                                    worksheet[batteryIndex].Cells[excelRowIndex, 0].Value = batterStateC[batterStatecounter];
#endif
                                }
                                else if ((buffer[1]) == 0x02)   //read battery votage
                                {
                                    int temp_v;
                                    temp_v = ((int)buffer[2] + (((int)buffer[3]) << 8));

                                    //label1.Text += "battery " + (batteryIndex).ToString() + ": " + ((int)buffer[2] + (((int)buffer[3]) << 8)).ToString() + "mV\n";
                                    battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + temp_v.ToString() + "mV\n";
                                    batterStateC[batterStatecounter] = ((int)buffer[2] + (((int)buffer[3]) << 8));
#if EXCEL_ENABLE
                                    worksheet[batteryIndex].Cells[excelRowIndex, 1].Value = batterStateC[batterStatecounter];
#endif
                                    if (temp_v > 20000) batteryError = 1;
                                }
                                else if ((buffer[1]) == 0x03)   //read battery ampere
                                {
                                    int temp_a;
                                    temp_a = ((int)buffer[2] + (((int)buffer[3]) << 8));

                                    //    label1.Text += "battery " + (batteryIndex).ToString() + ": " + ((int)buffer[2] + (((int)buffer[3]) << 8)).ToString() + "mA\n";
                                    battryStateTemp += "battery " + (batteryIndex).ToString() + ": " + temp_a.ToString() + "mA\n";
                                    batterStateC[batterStatecounter] = ((int)buffer[2] + (((int)buffer[3]) << 8));
#if EXCEL_ENABLE
                                    worksheet[batteryIndex].Cells[excelRowIndex, 2].Value = batterStateC[batterStatecounter];
#endif
                                    if (temp_a > 20000) batteryError = 1;
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
#if EXCEL_ENABLE
                                worksheet[batteryIndex].Cells[excelRowIndex, 3].Value = GetTimestamp(DateTime.Now);
#endif
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
                        File.AppendAllText(fullPath, Environment.NewLine + label6.Text + " " + lable_biosBom.Text);
                        File.AppendAllText(fullPath, Environment.NewLine + label8.Text + " " + label_biosName.Text);
                        File.AppendAllText(fullPath, Environment.NewLine + "------------------------");

                        break;

                    case 101:    

                        break;

                    case 102:     //get battery state
                        batteryIndex = 0;
                        batteryIndexCks = 0x1b;
                        batteryState = 0x00;

                        this.dataGridView1.DataSource = null;
                        this.dataGridView1.Rows.Clear();

                        getDataType = 2;        //parse method

                        //         Length Cmd index   checksum
                        serialWrite(0x04, 0xB0, 0x01, 0x4B);     //read battery remain power
                        Thread.Sleep(600);          //wait for receive data

                        //         Length Cmd index   checksum
                        serialWrite(0x04, 0xB0, 0x02, 0x4A);    //read battery votage
                        Thread.Sleep(600);          //wait for receive data

                        //         Length Cmd index   checksum
                        serialWrite(0x04, 0xB0, 0x03, 0x49);    //read battery ampere
                        Thread.Sleep(600);          //wait for receive data


                        getDataType = -1;
#if EXCEL_ENABLE
                        excelRowIndex++;
#endif
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
