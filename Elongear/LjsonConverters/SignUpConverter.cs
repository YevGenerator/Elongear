using Elongear.Observables;
using Elongear.ViewModels;
using Ljson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.LjsonConverters;

public class SignUpConverter : LjsonConvert<RegistrationViewModel>
{
    public override string[] GetValues(RegistrationViewModel obj)
    {
        return [obj.Login, obj.Email, obj.Password, "0"];
    }

    public override void SetValues(RegistrationViewModel obj, string[] values)
    {
        obj.Login = values[0];
        obj.Email = values[1];
        obj.Password = values[2];
    }
}
