## LogInspector

### Description
LogInspector is a versatile console application designed to streamline the process of analyzing logs by filtering log data and writing it to a specified file. With LogInspector, you can easily extract relevant information from log files and store it in a clear and readable format.

### Key Features
1. **Log Filtering:** Quickly sift through large log files to find the information you need. LogInspector allows users to specify filter criteria, such as IP address range and time range, enabling them to narrow down the displayed logs efficiently.
   
2. **Customizable Configuration:** Tailor LogInspector to your specific needs with customizable configuration options. Adjust settings via either a configuration file or command-line arguments to fine-tune the filtering process and output format.

3. **Readable Output:** Logs are presented in a visually appealing and easy-to-read format, making it simple to identify patterns, errors, or other important insights.

4. **Efficient Performance:** LogInspector is designed with efficiency in mind, enabling fast processing of log files even when dealing with large volumes of data.

### Usage
   
1. **Provide Input:** Provide the log file you want to analyze.
   Input format:

   ```csharp
   192.168.0.3:2021-04-09 12:13:36
   192.168.0.4:2021-04-30 08:25:36
   ...
   
2. **Adjust Settings:** Adjust filtering criteria either through a configuration file (appsettings.json) or command-line arguments.
   
3. **View Output:** View the filtered logs in the specified output location.

**Command-line arguments:**
- `--file-log`: Path to the log file.
- `--file-output`: Path to the output file.
- `--address-start`: Lower bound of the IP address range. Optional parameter; by default, all addresses are processed.
- `--address-mask`: Subnet mask specifying the upper bound of the address range as a decimal number. Optional parameter. If not specified, all addresses starting from the lower bound are processed. This parameter cannot be used if `address-start` is not specified.
- `--time-start`: Lower bound of the time interval.
- `--time-end`: Upper bound of the time interval.

**Example configuration file (`appsettings.json`):**
```json
{
  "LogFile": "log.txt",
  "OutputFile": "output.txt",
  "AddressStart": "192.168.0.1",
  "AddressMask": "10",
  "TimeStart": "08.04.2021",
  "TimeEnd": "20.04.2021"
}
```
### Get Started
Ready to simplify your log analysis process? Check out LogInspector on GitHub and start using it today!

