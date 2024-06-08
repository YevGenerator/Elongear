using Elongear.ViewModels;
using Ljson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elongear.LjsonConverters;

public class UserLoginConverter : LjsonConvert<LoginViewModel>
{
    public override string[] GetValues(LoginViewModel obj)
    {
        return [obj.LoginOrEmail, obj.Password];
    }

    public override void SetValues(LoginViewModel obj, string[] values)
    {
        obj.LoginOrEmail = values[0];
        obj.Password = values[1];
    }
}
