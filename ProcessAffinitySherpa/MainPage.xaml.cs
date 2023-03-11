using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessAffinitySherpa;

public partial class MainPage : ContentPage
{
    const string CONFIG = "config.json";

    ObservableCollection<ProcessSettings> processItems = new ObservableCollection<ProcessSettings>();
    uint CoreCount = 0;
    List<long> CoreMaskValues = new List<long>();
    long AllCoresMask = 0;
    List<CheckBox> CoreCheckboxes = new List<CheckBox>();
    ProcessSettings selectedProcess = null;

    public MainPage()
	{
		InitializeComponent();
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Environment.ProcessPath));

        //load
        try
        {
            string json = File.ReadAllText(CONFIG);
            processItems = JsonSerializer.Deserialize(json, typeof(ObservableCollection<ProcessSettings>)) as ObservableCollection<ProcessSettings>;
        }
        catch (FileNotFoundException) { }

        xProcessFileList.ItemsSource = processItems;

        CoreCount = ProcessorSherpa.NumberOfLogical();
        CoreMaskValues = ProcessorSherpa.BuildCoreMaskValues(CoreCount);

        for (int i = 0; i < CoreCount; i++)
		{
            AllCoresMask = AllCoresMask | CoreMaskValues[i];

            HorizontalStackLayout layout = new HorizontalStackLayout();

            CheckBox coreBtn = new CheckBox();
			coreBtn.IsChecked = true;
            CoreCheckboxes.Add(coreBtn);
            layout.Add(coreBtn);

            Label coreLabel = new Label();
            coreLabel.Text = i.ToString();
            coreLabel.VerticalTextAlignment = TextAlignment.Center;
            layout.Add(coreLabel);

            if (i % 2 == 0)
                xCoreList.RowDefinitions.Add(new RowDefinition());

            xCoreList.SetColumn(layout, i % 2);
            xCoreList.SetRow(layout, (int)Math.Floor(i / (double)2));
            xCoreList.Add(layout);
        }
    }

	private async void OnAddExeBtnClicked(object sender, EventArgs e)
	{
        var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".exe" } },
                });

        PickOptions options = new()
        {
            PickerTitle = "Please select an exe file",
            FileTypes = customFileType,
        };

        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("exe", StringComparison.OrdinalIgnoreCase))
                {
                    processItems.Add(new ProcessSettings() { FullPath = result.FullPath, Name = result.FileName.Replace(".exe", ""), Mask = AllCoresMask });
                }
            }
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }
    }

    private void xProcessFileList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is ProcessSettings)
        {
            ProcessSettings ps = e.SelectedItem as ProcessSettings;
            if (ps != null)
            {
                selectedProcess = ps;
                xSelectedExe.Text = ps.Name;
                xMask.Text = ps.Mask.ToString();
                xIsExeRunning.IsChecked = ProcessorSherpa.IsProcessRunning(ps);
                xSelectedExeAffinity.Text = ProcessorSherpa.ProcessAffinity(ps);

                for (int i = 0; i < CoreCheckboxes.Count; i++)
                {
                    CoreCheckboxes[i].IsChecked = (ps.Mask & CoreMaskValues[i]) == CoreMaskValues[i]; // is flagged
                }
                (xCoreList as IView).InvalidateArrange(); // is needed, sigh
            }
        }
    }

    private void OnUpdateMaskClicked(object sender, EventArgs e)
    {
        if (selectedProcess != null)
        {
            long mask = 0;

            for (int i = 0; i < CoreCheckboxes.Count; i++)
            {
                if (CoreCheckboxes[i].IsChecked)
                    mask = mask | CoreMaskValues[i]; // set
            }

            xMask.Text = mask.ToString();
            selectedProcess.Mask = mask;

            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,

            };
            string jsonString = JsonSerializer.Serialize(processItems, options);
            File.WriteAllText(CONFIG, jsonString);
        }
    }

    private void OnApplyClicked(object sender, EventArgs e)
    {
        if (selectedProcess != null)
        {
            ProcessorSherpa.SetAffinity(selectedProcess);

            xIsExeRunning.IsChecked = ProcessorSherpa.IsProcessRunning(selectedProcess);
            xSelectedExeAffinity.Text = ProcessorSherpa.ProcessAffinity(selectedProcess);
        }
    }
}

