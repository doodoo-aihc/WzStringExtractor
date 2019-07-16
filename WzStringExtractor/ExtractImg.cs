using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MapleLib.WzLib;
//using MapleLib.MapleCryptoLib;
//using MapleLib.Helpers;
//using MapleLib.PacketLib;
//using MapleLib.WzLib.Util;
//using MapleLib.WzLib.WzProperties;
using reWZ;
using reWZ.WZProperties;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace WzStringExtractor
{
    class ExtractImg
    {
        public ExtractImg()
        {
            int count = 0;
            var dmgSkins = JsonConvert.DeserializeObject<List<DamageSkin>>(File.ReadAllText(@"F:\Bots\workspace\187MSEA_DmgSkin.json"));
            WZFile xz = new WZFile(@"F:\MapleStorySEA\Item.wz", WZVariant.Classic, true);
            WZImage dmgSkinImg = (WZImage) xz.MainDirectory["Consume"]["0243.img"];

            foreach (var itemImg in dmgSkinImg)
            {
                foreach (var jsonDmg in dmgSkins)
                {
                    //Console.Write("debug");
                    string itemId = "0" + jsonDmg.itemId;
                    if (itemImg.Name == itemId)
                    {
                        if (itemImg["info"].HasChild("sample")) {
                            Bitmap dmgSkinPng = itemImg["info"]["sample"].ValueOrDefault<Bitmap>(null);
                            dmgSkinPng.Save($@"F:\Bots\workspace\dmgskin\{itemId}.png", ImageFormat.Png);
                            Console.WriteLine("done");
                            count++;
                        }
                    }
                }
            }
            Console.WriteLine("Count: " + count.ToString());
            Console.WriteLine("test");
            Console.ReadKey();
        }
    }
}
