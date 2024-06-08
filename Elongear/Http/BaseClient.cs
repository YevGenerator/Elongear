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

public class BaseClient : NetCoreServer.HttpClientEx
{
    public string Token { get; set; } = "-";

    public event Action Disconnected;
    public event Action<string> OnErrorResponse;
    public event Action<string> OnResponse;
    public string Url => $"http://{Address}:{Port}";


    public BaseClient(string address, int port) : base(address, port)
    {
        
    }

    public void AddTokenHeader(HttpRequest request) => request.SetHeader("auth_token", Token);

    public void SetHeader(string key, string value) => Request.SetHeader(key, value);

    public void SetHeaders(string[] keys, string[] values)
    {
        for(int i =0; i < keys.Length; i++)
        {
            SetHeader(keys[i], values[i]);
        }
    }

    public void SetBody(string content) => Request.SetBody(content);

    public void SetRequest(string[] keys, string[] values, string content)
    {
        SetHeaders(keys, values);
        SetBody(content);
    }

    public HttpRequest GetPostRequest(string[] headerKeys, string[] headerValues)
    {
        var request = Request;
        request.Clear();
        request.SetBegin("POST", Url);
        for (int i = 0; i < headerKeys.Length; i++)
        {
            request.SetHeader(headerKeys[i], headerValues[i]);
        }
        AddTokenHeader(request);
        return request;
    }

    public HttpRequest GetGetRequest(string part)
    {
        var request = Request.MakeGetRequest(part);
       
        //AddTokenHeader(request);
        return request;
    }
    public bool SendPostRequestAsync(string message, string[] keys, string[] values)
    {
        var request = GetPostRequest(keys, values);
        request.SetBody(message);
        return SendRequestAsync(request);
    }
    public bool SendGetRequestAsync(string part)
    {
        var request = GetGetRequest(part);
        return SendRequestAsync(request);
    }
    public bool SendPostRequestAsync(byte[] message, string[] keys, string[] values)
    {
        var request = GetPostRequest(keys, values);
        request.SetBody(message);
        return SendRequestAsync(request);
    }


    protected override void OnReceivedResponse(HttpResponse response)
    {
        if(response.Status == 500)
        {
            OnErrorResponse?.Invoke(response.Body);
            return;
        }
        OnResponse?.Invoke(response.Body);

    }


    public bool SendImage(string imagePath, string token)
    {
        var image = File.ReadAllBytes(imagePath);
        var mime = MimeHelper.GetImageMimeFromExtension(MimeHelper.GetExtensionFromFileName(imagePath));
        return SendPostRequestAsync(image, 
            ["command", "upload_token", "Content-Type"], 
            ["upload_image", token, mime]);
    }

    public bool SendSignUpCommand(string ljson)
        => SendPostRequestAsync(ljson, ["command"], ["sign_up"]);

    public bool SendSignInCommand(string ljson)
        => SendPostRequestAsync(ljson, ["command"], ["sign_in"]);

    public bool SendSelectCommand()
        => SendGetRequestAsync("select");

    public bool SendActivationCommand(string digits)
        => SendPostRequestAsync(digits, ["command"], ["confirm_activation"]);

    public bool SendResendActivationCommand()
        => SendPostRequestAsync("h", ["command"], ["resend_activation"]);

    public bool SendAddPodcastCommand(string ljson)
        => SendPostRequestAsync(ljson, ["command"], ["add_podcast"]);

    public bool SendGetCategoriesCommand()
        => SendPostRequestAsync("", ["command"], ["get_categories"]);

    protected override void OnDisconnected()
    {
        base.OnDisconnected();
        Disconnected?.Invoke();
    }
}
