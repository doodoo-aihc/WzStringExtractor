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

            string[] exceptions = new string[] { "30", "Protected", "Permanent", "Box", "Ticket" };

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();
                foreach (var item in consume)
                {
                    foreach (var child in item)
                    {
                        if (child.Name == "name")
                        {
                            if (child.ValueOrDie<String>().Contains("Damage Skin -"))
                            {
                                string damageSkin = child.ValueOrDie<string>();
                                if (exceptions.Any(damageSkin.Contains))
                                {
                                    break;
                                }

                                string[] split = damageSkin.Split('-');
                                string actualString = split[1].Trim();

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
                                    writer.WriteValue(damageSkin);
                                    writer.WriteEndObject();
                                    Console.WriteLine(damageSkin);
                                }
                            }
                        }
                    }
                }
                writer.WriteEndArray();
            }
            File.WriteAllText(output, sb.ToString());
            Console.WriteLine("Number of Damage Skins: " + listOfDamageSkins.Count);
            Console.Write("done");
            Console.ReadKey();
        }
    }
}
