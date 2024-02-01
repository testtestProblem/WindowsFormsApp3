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
