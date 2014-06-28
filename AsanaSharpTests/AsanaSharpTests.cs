using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AsanaSharp;

namespace AsanaSharpTests
{
    public class AsanaSharpTests
    {
        [Test]
        public void Deserializer()
        {
            var asana = new AsanaSharpC();
            asana.Deserialize();
        }
        [Test]
        public void Stackoverflow()
        {
//            Program.Main();
        }
    }
}
