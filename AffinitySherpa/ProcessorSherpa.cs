using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace ProcessAffinitySherpa
{
    internal class ProcessorSherpa
    {
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
            Process[] Procs = Process.GetProcessesByName(ps.Name);
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
            }
            //WARN: Process not found
        }

        public static List<Process> GetProcesses(ProcessSettings ps)
        {
            List<Process> processList = new List<Process>();
            Process[] procs = Process.GetProcessesByName(ps.Name);
            foreach (Process proc in procs)
            {
                if (proc.MainModule.FileName == ps.FullPath)
                {
                    processList.Add(proc);
                }
            }

            return processList;
        }
    }
}
