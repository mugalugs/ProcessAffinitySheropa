using ProcessAffinitySherpa;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Forms;

namespace AffinitySherpa
{
    public partial class MainForm : Form
    {
        const string CONFIG = "config.json";

        uint CoreCount = 0;
        List<long> CoreMaskValues = new List<long>();
        long AllCoresMask = 0;
        List<CheckBox> CoreCheckboxes = new List<CheckBox>();
        ProcessSettings? selectedProcess = null;
        System.Windows.Forms.Timer oneSecondTimer = new System.Windows.Forms.Timer();
        List<ProcessSettings> processSettingsList = new List<ProcessSettings>();

        public MainForm()
        {
            InitializeComponent();
            openFileDialog1.Filter = "exe files (*.exe)|*.exe";
            openFileDialog1.FilterIndex = 0;
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Environment.ProcessPath));

            oneSecondTimer.Tick += OneSecondTimer_Tick;
            oneSecondTimer.Interval = 5000; // used to be 1000
            oneSecondTimer.Start();

            //init cores and UI
            CoreCount = ProcessorSherpa.NumberOfLogical();
            CoreMaskValues = ProcessorSherpa.BuildCoreMaskValues(CoreCount);

            for (int i = 0; i < CoreCount; i++)
            {
                AllCoresMask = AllCoresMask | CoreMaskValues[i];

                CheckBox coreBtn = new CheckBox();
                coreBtn.Checked = true;
                coreBtn.Text = i.ToString();
                coreBtn.Width = 50;

                coreBtn.Top = 25 + ((int)Math.Floor(i / (double)2) * 22);

                if (i % 2 == 0)
                    coreBtn.Left = 15;
                else
                    coreBtn.Left = 65;

                CoreCheckboxes.Add(coreBtn);

                corePanel.Controls.Add(coreBtn);
            }

            //load config
            try
            {
                string json = File.ReadAllText(CONFIG);
                processSettingsList = JsonSerializer.Deserialize(json, typeof(List<ProcessSettings>)) as List<ProcessSettings>;
                UpdateExeList();
            }
            catch (FileNotFoundException) { }

            //these crash on startup
            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
            {
                if (arg.ToLowerInvariant() == "-startMinimised".ToLowerInvariant())
                {
                    WindowState = FormWindowState.Minimized;
                    notifyIcon1.Visible = true;
                    ShowInTaskbar = false;
                }

                if (arg.ToLowerInvariant() == "-autoApply".ToLowerInvariant())
                {
                    autoApply.Checked = true;
                }
            }

            if (WindowState != FormWindowState.Minimized)
                Resize += MainForm_Resize;
        }

        private void OneSecondTimer_Tick(object? sender, EventArgs e)
        {
            if (autoApply.Checked)
            {
                foreach (ProcessSettings ps in processSettingsList)
                {
                    ProcessorSherpa.SetAffinity(ps);
                }
            }
        }

        private void addExeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName.EndsWith("exe", StringComparison.OrdinalIgnoreCase))
                    {
                        processSettingsList.Add(new ProcessSettings() { FullPath = openFileDialog1.FileName, Name = Path.GetFileNameWithoutExtension(openFileDialog1.FileName), Mask = AllCoresMask });
                        UpdateExeList();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void exeList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (exeList.SelectedIndices.Count > 0)
            {
                ProcessSettings ps = processSettingsList[exeList.SelectedIndices[0]] as ProcessSettings;
                if (ps != null)
                {
                    selectedProcess = ps;
                    selectedName.Text = $"Name: {ps.Name}";
                    selectedFullPath.Text = $"Path: {ps.FullPath}";
                    selectedMask.Text = $"Mask: {ps.Mask}";

                    for (int i = 0; i < CoreCheckboxes.Count; i++)
                    {
                        CoreCheckboxes[i].Checked = (ps.Mask & CoreMaskValues[i]) == CoreMaskValues[i]; // is flagged
                    }

                    processList.Items.Clear();
                    List<Process> procs = ProcessorSherpa.GetProcesses(selectedProcess);
                    foreach (Process process in procs)
                    {
                        processList.Items.Add(new ListViewItem(new string[] { process.Id.ToString(), process.ProcessorAffinity.ToString() }));
                    }
                }
            }
        }

        private void updateMaskBtn_Click(object sender, EventArgs e)
        {
            if (selectedProcess != null)
            {
                long mask = 0;

                for (int i = 0; i < CoreCheckboxes.Count; i++)
                {
                    if (CoreCheckboxes[i].Checked)
                        mask = mask | CoreMaskValues[i]; // set
                }

                selectedMask.Text = $"Mask: {mask}";
                selectedProcess.Mask = mask;
                SaveExeList();
            }
        }

        private void applyAffinityBtn_Click(object sender, EventArgs e)
        {
            if (selectedProcess != null)
            {
                ProcessorSherpa.SetAffinity(selectedProcess);

                Thread.Sleep(200);

                processList.Items.Clear();
                List<Process> procs = ProcessorSherpa.GetProcesses(selectedProcess);
                foreach (Process process in procs)
                {
                    processList.Items.Add(new ListViewItem(new string[] { process.Id.ToString(), process.ProcessorAffinity.ToString() }));
                }
            }
        }

        private void UpdateExeList()
        {
            exeList.Items.Clear();
            foreach (ProcessSettings item in processSettingsList)
            {
                exeList.Items.Add(item.AsListItem());
            }
        }

        private void SaveExeList()
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(processSettingsList, options);
            File.WriteAllText(CONFIG, jsonString);
        }

        private void exeList_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (exeList.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    processSettingsList.RemoveAt(exeList.SelectedIndices[0]);
                    UpdateExeList();
                    SaveExeList();
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //infinite loop on startup if minimised
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                ShowInTaskbar = false;
                Resize -= MainForm_Resize;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            ShowInTaskbar = true;
            Activate();
            WindowState = FormWindowState.Normal;
            Resize += MainForm_Resize;
        }

        private void createLauncherBtn_Click(object sender, EventArgs e)
        {
            if (selectedProcess != null)
            {
                //affinity is hex based
                //@"c:\windows\system32\cmd.exe /C start /affinity 1 {selectedProcess.FullPath}";
            }
        }
    }
}
