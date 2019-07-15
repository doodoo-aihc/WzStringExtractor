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
            //WZFile xz = new WZFile(@"F:\MapleStorySEA\String.wz", WZVariant.MSEA, true, WZReadSelection.EagerParseStrings);
            //WZStringProperty property = xz.ResolvePath("Consume.img/2000002/name").ValueOrDie<WZStringProperty>();
            //Console.Write(property);
            WzFile file = new WzFile($@"F:\MapleStorySEA\String.wz", 187, WzMapleVersion.GETFROMZLZ);
            file.ParseWzFile();

            file.WzDirectory.ParseImages();

            foreach (var item in file.WzDirectory.WzImages)
            {
                item.ParseImage(true);
                Console.WriteLine(item.Name);
            }


            //var test = file.GetObjectFromPath("Consume.img");
            Console.Write("test");
            Console.ReadKey();
        }
    }
}
