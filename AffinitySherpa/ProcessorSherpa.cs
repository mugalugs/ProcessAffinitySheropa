using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ProcessAffinitySherpa
{
    internal class ProcessorSherpa
    {
        private const int PROCESS_PERMISSION = 0x1F0FFF;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetProcessAffinityMask(IntPtr hProcess, out IntPtr processAffinityMask, out IntPtr systemAffinityMask);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetProcessAffinityMask(IntPtr hProcess, IntPtr processAffinityMask);


        public static List<long> BuildCoreMaskValues(uint coreCount)
        {
            List<long> result = new List<long>();

            long num = 1;
            result.Add(num);

            for (int i = 0; i < coreCount; i++)
            {
                num = num * 2;
                result.Add(num);
            }

            return result;
        }

        public static uint NumberOfLogical()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select NumberOfLogicalProcessors from Win32_Processor"))
            {
                foreach (var item in searcher.Get())
                {
                    return (uint)item["NumberOfLogicalProcessors"];
                }
            }

            return (uint)Environment.ProcessorCount;
        }

        public static void SetAffinity(ProcessSettings ps)
        {
            IntPtr hSnapshot = CreateToolhelp32Snapshot(2 /* TH32CS_SNAPPROCESS */, 0);
            if (hSnapshot == IntPtr.Zero)
            {
                //Console.WriteLine("Failed to create snapshot.");
                return;
            }

            try
            {
                PROCESSENTRY32 processEntry = new PROCESSENTRY32();
                processEntry.dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32));

                if (Process32First(hSnapshot, ref processEntry))
                {
                    do
                    {
                        if (processEntry.szExeFile.Equals(ps.Name + ".exe", StringComparison.OrdinalIgnoreCase))
                        {
                            IntPtr hProcess = OpenProcess(PROCESS_PERMISSION, false, (int)processEntry.th32ProcessID);
                            if (hProcess != IntPtr.Zero)
                            {
                                try
                                {
                                    IntPtr oldAffinityMask;
                                    IntPtr systemAffinityMask;
                                    if (GetProcessAffinityMask(hProcess, out oldAffinityMask, out systemAffinityMask))
                                    {
                                        if (oldAffinityMask != (IntPtr)ps.Mask)
                                        {
                                            if (SetProcessAffinityMask(hProcess, (IntPtr)ps.Mask))
                                            {
                                                // Successfully set the affinity mask
                                            }
                                            else
                                            {
                                                // Failed to set the affinity mask
                                                // You can handle the error here
                                            }
                                        }
                                        else
                                        {
                                            // Affinity mask is already set
                                        }
                                    }
                                    else
                                    {
                                        // Failed to retrieve the affinity mask
                                        // You can handle the error here
                                    }
                                }
                                finally
                                {
                                    CloseHandle(hProcess);
                                }
                            }
                            else
                            {
                                // Failed to open the process
                                // You can handle the error here
                            }
                        }
                    }
                    while (Process32Next(hSnapshot, ref processEntry));
                }
                else
                {
                    //Console.WriteLine("Failed to retrieve process entry.");
                }
            }
            finally
            {
                CloseHandle(hSnapshot);
            }
        }

        public static Dictionary<int, nint> GetProcesses(ProcessSettings ps)
        {
            Dictionary<int, nint> dict = new Dictionary<int, nint>();
            IntPtr hSnapshot = CreateToolhelp32Snapshot(2 /* TH32CS_SNAPPROCESS */, 0);
            if (hSnapshot == IntPtr.Zero)
            {
                //Console.WriteLine("Failed to create snapshot.");
                return dict;
            }

            try
            {
                PROCESSENTRY32 processEntry = new PROCESSENTRY32();
                processEntry.dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32));

                if (Process32First(hSnapshot, ref processEntry))
                {
                    do
                    {
                        if (processEntry.szExeFile.Equals(ps.Name + ".exe", StringComparison.OrdinalIgnoreCase))
                        {
                            IntPtr hProcess = OpenProcess(PROCESS_PERMISSION, false, (int)processEntry.th32ProcessID);
                            if (hProcess != IntPtr.Zero)
                            {
                                try
                                {
                                    IntPtr oldAffinityMask;
                                    IntPtr systemAffinityMask;
                                    if (GetProcessAffinityMask(hProcess, out oldAffinityMask, out systemAffinityMask))
                                    {
                                        dict.Add((int)processEntry.th32ProcessID, oldAffinityMask);
                                    }
                                    else
                                    {
                                        // Failed to retrieve the affinity mask
                                        // You can handle the error here
                                    }
                                }
                                finally
                                {
                                    CloseHandle(hProcess);
                                }
                            }
                            else
                            {
                                // Failed to open the process
                                // You can handle the error here
                            }
                        }
                    }
                    while (Process32Next(hSnapshot, ref processEntry));
                }
                else
                {
                    //Console.WriteLine("Failed to retrieve process entry.");
                }
            }
            finally
            {
                CloseHandle(hSnapshot);
            }

            return dict;
        }

        /*public static void SetAffinity(ProcessSettings ps)
        {
            Process[]? Procs = Process.GetProcessesByName(ps.Name);
            foreach (Process proc in Procs)
            {
                if (proc.MainModule.FileName == ps.FullPath)
                {
                    if (proc.ProcessorAffinity != ps.Mask)
                    {
                        proc.ProcessorAffinity = (nint)ps.Mask;
                    }
                    //INFO: Affinity is the same as mask
                }
                //ERR: Name match but path different

                proc.Close();
                proc.Dispose();
            }
            //WARN: Process not found

            Procs = null;
            CleanupProcesses();
        }*/

        /*public static List<Process> GetProcesses(ProcessSettings ps)
        {
            List<Process> processList = new List<Process>();
            Process[]? procs = Process.GetProcessesByName(ps.Name);
            foreach (Process proc in procs)
            {
                if (proc.MainModule.FileName == ps.FullPath)
                {
                    processList.Add(proc);
                }

                proc.Close();
                proc.Dispose();


            }

            procs = null;
            CleanupProcesses();

            return processList;
        }*/

        //Curious memory leak in Process around performance counter hashtables (private static readonly)
        //https://stackoverflow.com/questions/13084585/process-getprocessesbynamestring-string-memory-leak
        //https://stackoverflow.com/questions/26225100/memory-leak-in-process-getprocesses
        /*private static void CleanupProcesses()
        {
            PerformanceCounter.CloseSharedResources();
            //GC.Collect();

            //doesn't help
        }*/
    }
}
