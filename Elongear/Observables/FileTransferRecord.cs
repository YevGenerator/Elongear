using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Observables;

public partial class FileTransferRecord: ObservableObject
{
    [ObservableProperty]
    private string podcastName = "";

    [ObservableProperty]
    private double currentProgress = 0;

    [ObservableProperty]
    private bool isUploading = true;
}
