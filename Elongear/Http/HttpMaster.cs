using NetCoreServer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Http;

public static class HttpMaster
{
    private static string token = "--";
    public static string Token
    {
        get => token;
        set
        {
            token = value;
            if (client is not null) client.Token = value;
        }
    }
    
    //public static string Address => "192.168.43.170";
    public static string Address => "192.168.0.103";
    public static int Port => 8080;
    public static string Url => @$"http://{Address}:{Port}/";
    public static Action<string> OnErrorResponse { get; set; } = (v) => { };
    public static Action<string> OnResponse { get; set; } = (v) => { };
    public static Action OnFinished { get; set; } = () => { };
    public static Action<double> OnSentProgress { get; set; } = (v) => { };
    private static BaseClient? client;

    private static List<FileSendClient> fileSendClients = [];
    private static List<FileReceiveClient> fileReceiveClients = [];
    public static BaseClient Client
    {
        get
        {
            if (client is null || client.IsDisposed)
            {
                client = new BaseClient(Address, Port);
                client.Token = Token;
                SetEvents(client);

            }
            client.ConnectAsync();
            return client;
        }
    }

    
 
    private static void OnDisconnected()
    {
        client = null;
    }
    private static void SetEvents(BaseClient client)
    {
        client.OnErrorResponse += (v) => OnErrorResponse(v);
        client.OnResponse += (v) => OnResponse(v);
        client.Disconnected += OnDisconnected;
    }

    public static bool SendPostRequestAsync(string body, string[] headerKeys, string[] headerValues) =>
        Client.SendPostRequestAsync(body, headerKeys, headerValues);

    public static bool SendGetRequestAsync(string part) =>
        Client.SendGetRequestAsync(part);



    private static void OnSendProgress(FileSendClient client, double value, Action<double> progress)
    {
        progress(value);
        if (value == -1 || value == 1)
        {
            fileSendClients.Remove(client);
            client.Dispose();
        }
    }
    private static void OnReceiveProgress(FileReceiveClient client, double value, Action<double> progress)
    {
        progress(value);
        if (value == -1 || value == 1)
        {
            fileReceiveClients.Remove(client);
            client.Dispose();
        }
    }

    public static void ReceiveFileInNewSession(string downloadDirectory, string podcastId, Action<double> progress)
    {
        var client = new FileReceiveClient(Address, Port)
        {            
            Directory = downloadDirectory,
            FileName = podcastId
        };
        client.ConnectAsync();
        client.SendFileInfoRequest(podcastId);
        client.ReceiveProgress += (c, val) => OnReceiveProgress(client, val, progress);
        fileReceiveClients.Add(client);
    }

    public static bool SendFileInNewSession(string uploadToken, string filePath, Action<double> progress)
    {
        var client = new FileSendClient(Address, Port)
        {
            Token = Token,
            UploadToken = uploadToken,
            FilePath = filePath
        };
        client.ConnectAsync();
        var sent = client.SendFileRequestAsync();
        client.SendProgress += (c, val) => OnSendProgress(client, val, progress);
        fileSendClients.Add(client);
        return sent;
    }

    private static void SetActions(Action<string> OnSuccess, Action<string> OnError)
    {
        OnResponse = (v) => OnSuccess(v);
        OnErrorResponse = (v) => OnError(v);
    }
    public static bool SendSignUpCommand(string ljson, Action<string> OnSuccess, Action<string> OnError)
    {
        OnResponse = (v) =>
        {
            Token = v;
            OnSuccess(v);
        };
        OnErrorResponse = (v) => OnError(v);
        return Client.SendSignUpCommand(ljson);
    }

    public static bool SendSignInCommand(string ljson, Action<string> OnSuccess, Action<string> OnError)
    {
        OnResponse = (v) =>
        {
            Token = v;
            OnSuccess(v);
        };
        OnErrorResponse = (v) => OnError(v);
        return Client.SendSignInCommand(ljson);
    }

    public static bool SendSelectCommand(Action<string> OnSuccess, Action<string> OnError)
    {
        SetActions(OnSuccess, OnError);
        return Client.SendSelectCommand();
    }

    public static bool SendConfirmActivationCommand(string digits, Action<string> OnSuccess, Action<string> OnError)
    {
        SetActions(OnSuccess, OnError);
        return Client.SendActivationCommand(digits);
    }

    public static bool SendResendActivationCommand(Action<string> OnSuccess, Action<string> OnError)
    {
        SetActions(OnSuccess, OnError);
        return Client.SendResendActivationCommand();
    }
    public static bool SendImageCommand(string imagePath, string token, Action<string> OnSuccess, Action<string> OnError)
    {
        SetActions(OnSuccess, OnError);
        return Client.SendImage(imagePath, token);
    }
    public static bool SendGetCategoriesCommand(Action<string> OnSuccess, Action<string> OnError)
    {
        SetActions(OnSuccess, OnError);
        return Client.SendGetCategoriesCommand();
    }
    public static bool SendPodcastCommand
        (string ljson, string imagePath, string podcastPath, Action<string> OnSuccess, Action<string> OnError, Action<double> progress)
    {
        OnResponse = (v) =>
        {
            var uploadToken = v;
            SendImageCommand(imagePath, uploadToken, OnSuccess, OnError);
            SendFileInNewSession(uploadToken, podcastPath, progress);
        };
        OnErrorResponse = (v) => OnError(v);
        return Client.SendAddPodcastCommand(ljson);
    }
}
