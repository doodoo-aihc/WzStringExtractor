using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reWZ;
using reWZ.WZProperties;
using Newtonsoft.Json;
using System.IO;

namespace WzStringExtractor
{
    class ExtractString
    {
        public ExtractString(string fileName, string output)
        {

            WZFile xz = new WZFile(fileName, WZVariant.Classic, true, WZReadSelection.EagerParseStrings);
            WZObject consume = xz.MainDirectory["Consume.img"];

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HashSet<string> listOfDamageSkins = new HashSet<string>();

            string[] exceptions = new string[] { "30", "Protected", "Permanent" };

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
                            if (test.ValueOrDie<String>().Contains("Damage Skin -"))
                            {
                                string damageSkin = test.ValueOrDie<string>();
                                if (exceptions.Any(damageSkin.Contains))
                                {
                                    break;
                                }

                                string[] split = damageSkin.Split('-');
                                string actualString = split[1].Trim();
                                Console.WriteLine(actualString);
                                if (listOfDamageSkins.Contains(actualString))
                                {
                                    Console.WriteLine(actualString + " is already in the list");
                                    break;
                                } else
                                {
                                    listOfDamageSkins.Add(actualString);
                                    writer.WriteStartObject();
                                    writer.WritePropertyName("itemId");
                                    writer.WriteValue(item.Name);
                                    writer.WritePropertyName("itemName");
                                    writer.WriteValue(actualString);
                                    writer.WriteEndObject();
                                    Console.WriteLine(actualString);
                                }
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
            File.WriteAllText(output, sb.ToString());
            Console.Write("done");
            Console.ReadKey();
        }
    }
}
