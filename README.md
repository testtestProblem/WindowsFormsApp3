# checkBatteryState
Using Win32 API to control UART for reading battery state. It can show battery capacity, current, votage.

* UI
<img width="366" alt="Capture" src="https://github.com/testtestProblem/checkBatteryState/assets/107662393/7f8f4a2f-42a9-4cd4-8197-e484eabfe06b">

* Open com port
```C#
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
      Console_receiving = true;

        sendData = new Thread(DoSend);
        sendData.IsBackground = true;
        sendData.Start();

        receiveData = new Thread(DoReceive);
        receiveData.IsBackground = true;
        receiveData.Start();
	}
}
else
{
    MessageBox.Show("Serial port has been opened.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

* Receive data  
```C#
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

.......
```
* Write data
```C#
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
```

```C#
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
..............
```
