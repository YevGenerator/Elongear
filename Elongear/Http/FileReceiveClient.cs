using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Http;

public class FileReceiveClient : BaseClient
{
    private FileStream? fileStream;
    public string FileName { get; set; } = "";
    private string Extension { get; set; } = ".mp3";
    public string Directory { get; set; } = "";
    public string FilePath => Path.Combine(Directory, FileName+Extension);
    public long TotalSize { get; set; } = 0;
    public long ReadSize { get; set; } = 0;
    public bool IsReceiving { get; set; } = false;

    public event Action<FileReceiveClient, double> ReceiveProgress;

    public FileReceiveClient(string address, int port) : base(address, port)
    {
        
        
    }

    public void StartFileWrite()
    {
        if (FileName == "")
        {       
            return;
        }
        var exists = System.IO.Directory.Exists(Directory);
        if(!exists)
        {
            System.IO.Directory.CreateDirectory(Directory);
        }
        try
        {
            fileStream = File.OpenWrite(FilePath);
        }
        catch 
        {
            return;
        }
        ReadSize = 0;
        IsReceiving = true;
    }
    public void FinishFileWrite()
    {
        fileStream?.Close();
        IsReceiving = false;
    }

    public void ExtractFileData(HttpResponse response)
    {
        TotalSize = 0;
        
        for (long i = 0; i < response.Headers; i++)
        {
            var header = response.Header((int)i);
           
            if (header.Item1 == "fileSize")
            {
                TotalSize = long.Parse(header.Item2);
                continue;
            }
            if (header.Item1 == "fileName")
            {
                FileName = header.Item2;
                continue;
            }
            /*if(header.Item1 == "Content-Type")
            {
                Extension = MimeHelper.GetExtensionFromAudioMime(header.Item2);
                continue;
            }*/
        }
    }

    public bool SendFileInfoRequest(string podcastId) => SendGetRequestAsync($"download/{podcastId}");

    protected override void OnReceivedResponse(HttpResponse response)
    {
        ExtractFileData(response);
        if (TotalSize == 0 || FileName == "") return;
        StartFileWrite();
        if(response.BodyLength != 0)
        {
            OnReceived(response.BodyBytes, 0, response.BodyLength);
        }

    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        if (IsReceiving)
        {
            fileStream ??= File.OpenWrite(FilePath);
            fileStream.Write(buffer, 0, (int)size);
            ReadSize += size;
            ReceiveProgress?.Invoke(this, (double)ReadSize / TotalSize);
            if (ReadSize >= TotalSize)
            {
                FinishFileWrite();
            }
            return;
        }
        else
        {
            base.OnReceived(buffer, offset, size);
        }
    }


}
