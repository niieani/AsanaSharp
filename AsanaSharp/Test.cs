using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AsanaSharp
{
    class Test
    {
        void gen()
        {
            var assembly = typeof(Asana).Assembly;
            var types = assembly.GetTypes();
            var props = typeof (Asana).GetProperties();
            var typesAsMember = types.Cast<MemberInfo>().ToArray();
            var propsAsMembers = props.Cast<MemberInfo>().ToArray();
        }
    }
}
