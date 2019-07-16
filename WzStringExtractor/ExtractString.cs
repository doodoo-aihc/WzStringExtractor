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
using Newtonsoft.Json;
using System.IO;

namespace WzStringExtractor
{
    class ExtractString
    {
        public ExtractString()
        {
            WZFile xz = new WZFile(@"F:\MapleStorySEA\String.wz", WZVariant.Classic, true, WZReadSelection.EagerParseStrings);
            WZObject consume = xz.MainDirectory["Consume.img"];

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();
                foreach (var item in consume)
                {
                    foreach (var test in item)
                    {
                        if (test.Name == "name")
                        {
                            if (test.ValueOrDie<String>().Contains("Damage Skin"))
                            {
                                writer.WriteStartObject();
                                writer.WritePropertyName("itemId");
                                writer.WriteValue(item.Name);
                                writer.WritePropertyName("itemName");
                                writer.WriteValue(test.ValueOrDie<String>());
                                writer.WriteEndObject();
                                //Console.WriteLine(test.ValueOrDie<String>());
                                //Console.WriteLine(item.Name);
                            }
                        }
                    }
                }
                writer.WriteEndArray();
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
            File.WriteAllText(@"F:\Bots\workspace\187MSEA_DmgSkin.json", sb.ToString());
            Console.Write("done");
            Console.ReadKey();
        }
    }
}
