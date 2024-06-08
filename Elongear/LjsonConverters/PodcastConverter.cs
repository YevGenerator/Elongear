using Elongear.Observables;
using Ljson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.LjsonConverters;

public class PodcastConverter : LjsonConvert<Podcast>
{
    public override string[] GetValues(Podcast obj)
    {
        return [obj.PodcastId, obj.Name, obj.Description];
    }

    public override void SetValues(Podcast obj, string[] values)
    {
        obj.PodcastId = values[0];
        obj.Name = values[1];
        obj.Description = values[2];
    }
}
