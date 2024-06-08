using Elongear.Observables;

using System.Collections.ObjectModel;


namespace Elongear.ViewModels;

public class FileProgressViewModel: BaseViewModel
{
    public ObservableCollection<FileTransferRecord> Files { get; set; } = [];
    public Action<double> AddUploadRecord(string podcastName)
    {
        var record = new FileTransferRecord()
        {
            PodcastName = podcastName,
            IsUploading = true
        };
        Files.Add(record);
        return (v) => record.CurrentProgress = v;
    }

    public Action<double> AddDownloadRecord(string podcastName)
    {
        var record = new FileTransferRecord()
        {
            PodcastName = podcastName,
            IsUploading = false
        };
        Files.Add(record);
        return (v) => record.CurrentProgress = v;
    }
}
