using Elongear.ViewModels;
using Ljson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.LjsonConverters;

public class AddPodcastConverter : LjsonConvert<AddPodcastViewModel>
{
    public override string[] GetValues(AddPodcastViewModel obj)
    {
        return [obj.PodcastName, obj.PodcastDescription, "0", obj.CategoryName];
    }

    public override void SetValues(AddPodcastViewModel obj, string[] values)
    {
        obj.PodcastName = values[0];
        obj.PodcastDescription = values[1];
        
    }
}
