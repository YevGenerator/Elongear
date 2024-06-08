using Elongear.Observables;
using Ljson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.LjsonConverters;

public class CategoryConverter : LjsonConvert<Category>
{
    public override string[] GetValues(Category obj)
    {
        return [obj.Id.ToString(), obj.Name];
    }

    public override void SetValues(Category obj, string[] values)
    {
        obj.Id = int.Parse(values[0]);
        obj.Name = values[1];
    }
}
