using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapleLib.WzLib;
using MapleLib.MapleCryptoLib;
using MapleLib.Helpers;
using MapleLib.PacketLib;
using MapleLib.WzLib.Util;
using MapleLib.WzLib.WzProperties;
using reWZ;
using reWZ.WZProperties;

namespace WzStringExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            WZFile xz = new WZFile(@"D:\Program Files (x86)\MapleStorySEA\String.wz", WZVariant.Classic, true, WZReadSelection.EagerParseStrings);
            WZObject consume = (WZObject)xz.MainDirectory["Consume.img"];

            foreach (var item in consume)
            {
                foreach (var test in item)
                {
                    if (test.Name == "name")
                    {
                        Console.WriteLine(test.ValueOrDie<String>());
                        Console.WriteLine(item.Name);
                    }
                }

            }

            //WZStringProperty property = xz.ResolvePath("Consume.img/2000002/name").ValueOrDie<WZStringProperty>();
            //Console.Write(property);
            //WzFile file = new WzFile($@"D:\Program Files (x86)\MapleStorySEA\String.wz", 187, WzMapleVersion.CLASSIC);
            //file.ParseWzFile();

            ////file.WzDirectory.ParseImages();

            //foreach (var item in file.WzDirectory.WzImages)
            //{
            //    //item.ParseImage(true);
            //    WzObject test = file.GetObjectFromPath("Consume.img");
            //    Console.WriteLine(item.Name);
            //}


            ////var test = file.GetObjectFromPath("Consume.img");
            Console.Write("test");
            Console.ReadKey();
        }
    }
}
