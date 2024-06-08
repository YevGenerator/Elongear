using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.Http;

public static class MimeHelper
{
    public static string GetExtensionFromFileName(string fileName) => Path.GetExtension(fileName).Remove(0, 1);
    public static string GetExtensionFromImageMime(string mimetype) => "." + mimetype.Replace("image/", "");
    public static string GetExtensionFromAudioMime(string mimetype)
    {
        mimetype = mimetype.Replace("audio/", "");
        return mimetype switch
        {
            "x-m4a" => ".m4a",
            "mpeg" => ".mp3",
            "vnd.wav" => ".wav",
            "ogg" => ".ogg",
            _ => ".mp3"
        };
    }
    public static string GetAudioMimeFromExtension(string extension) =>
        "audio/" + extension switch
        {
            "mp3" => "mpeg",
            "wav" => "wnv.wav",
            "ogg" => "ogg",
            "m4a" => "x-m4a",
            _ => "mpeg"
        };

    public static string GetImageMimeFromExtension(string extension) => "image/" + extension;
}
