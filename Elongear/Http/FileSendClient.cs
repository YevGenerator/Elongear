using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Http;

public class FileSendClient(string address, int port) : BaseClient(address, port)
{
    public event Action<FileSendClient, double> SendProgress;
    public string FilePath { get; set; } = "";
    public string UploadToken { get; set; } = "";
    public long ReadSize { get; set; } = 0;

    public void SendFile()
    {
        var fileStream = File.OpenRead(FilePath);
        var fileBuffer = new byte[10 * OptionSendBufferSize];
        while (ReadSize < fileStream.Length)
        {
            var read = fileStream.Read(fileBuffer, 0, 10 * OptionSendBufferSize);
            ReadSize += read;
            SendProgress?.Invoke(this, (double)ReadSize/ fileStream.Length);
            Send(fileBuffer, 0, read);
        }
    }
    public bool SendFileRequestAsync()
    {
        var size = new FileInfo(FilePath).Length;
        var mime = MimeHelper.GetAudioMimeFromExtension(MimeHelper.GetExtensionFromFileName(FilePath));
        return SendPostRequestAsync("",
            ["command", "upload_token", "fileSize", "Content-Type"],
            ["upload_podcast", UploadToken, size.ToString(), mime]);
    }
    protected override void OnReceivedResponse(HttpResponse response)
    {
        if(response.Body == "Готовий приймати")
        {
            SendFile();
            return;
        }
        if(response.Body == "Прийняв")
        {
            DisconnectAsync();
            SendProgress?.Invoke(this, 1);
            return;
        }        
        base.OnReceivedResponse(response);
    }
}
