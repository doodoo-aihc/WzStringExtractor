using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reWZ;
using reWZ.WZProperties;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WzStringExtractor
{
    class ExtractDamageSkinNumbers
    {
        public ExtractDamageSkinNumbers(string fileName, string outputLocation)
        {
            int count = 0;
            //var dmgSkins = JsonConvert.DeserializeObject<List<DamageSkin>>(File.ReadAllText(jsonLocation));


            WZFile xz = new WZFile(fileName, WZVariant.MSEA, false);
            WZSubProperty damageSkinNumberImg = (WZSubProperty)xz.MainDirectory["BasicEff.img"]["damageSkin"];
            foreach (var numberType in damageSkinNumberImg)
            {
                Bitmap dmgSkinNumberPng = null;
                if (numberType.HasChild("ItemID"))
                {
                    Console.WriteLine("hi");
                    foreach (var numberImg in numberType)
                    {
                        foreach (var number in numberImg)
                        {
                            WZCanvasProperty test = (WZCanvasProperty)number;
                            string[] pathNames = test.Path.Split('/');
                            int itemId = numberType["ItemID"].ValueOrDefault<Int32>(Int32.Parse(pathNames[4]));
                            if (!(number is WZCanvasProperty))
                            {
                                break;
                            }
                            dmgSkinNumberPng = test.Value;
                            Directory.CreateDirectory($@"{outputLocation}\{itemId.ToString()}\{pathNames[4]}");
                            dmgSkinNumberPng.Save($@"{outputLocation}\{itemId.ToString()}\{pathNames[4]}\{pathNames[5]}.png", ImageFormat.Png);
                            Console.WriteLine("Exported damage skin");
                            count++;
                        }
                    }
                }

            }
            Console.WriteLine($"Successfully dumped {count.ToString()} number of damage skins");
            Console.ReadKey();
        }
    }
}
