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
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WzStringExtractor
{
    class ExtractImg
    {
        public ExtractImg()
        {
            int count = 0;
            var dmgSkins = JsonConvert.DeserializeObject<List<DamageSkin>>(File.ReadAllText(@"F:\Bots\workspace\187MSEA_DmgSkin.json"));

            //WzFile f = new WzFile(@"F:\MapleStorySEA\Item.wz", WzMapleVersion.CLASSIC);
            //f.ParseWzFile();
            ////f.GetObjectsFromRegexPath("Consume/0243.img")
            //WzDirectory dmgSkinDir = (WzDirectory)f.WzDirectory.WzDirectories.FirstOrDefault(x => x.Name == "Consume");
            //WzImage dmgSkinImg = dmgSkinDir.WzImages.FirstOrDefault(x => x.Name == "0243.img");

            //foreach (var item in dmgSkinImg.WzProperties)
            //{
            //    foreach (var jsonDmg in dmgSkins)
            //    {
            //        string itemId = "0" + jsonDmg.itemId;
            //        if (item.Name == itemId)
            //        {
            //            Bitmap test = item.WzProperties.FirstOrDefault(x => x.Name == "info").WzProperties.FirstOrDefault(x => x.Name == "sample").GetBitmap();
            //            test.Save($@"F:\Bots\workspace\dmgskin\{itemId}.png", ImageFormat.Png);
            //            Console.WriteLine($"Dumped {jsonDmg.itemId} - {jsonDmg.itemName}");
            //            Console.Write("test");
            //        }
            //    }
            //}

            string fileName;
            OpenFileDialog fd = new OpenFileDialog();
            fd.ShowDialog();
            fileName = fd.FileName;
            WZFile xz = new WZFile(@"F:\MapleStorySEA\Item.wz", WZVariant.MSEA, false);
            WZImage dmgSkinImg = (WZImage)xz.MainDirectory["Consume"]["0243.img"];
            WZImage dmgSkinImg2 = (WZImage)xz.MainDirectory["Consume"]["0263.img"];

            //foreach (var itemImg in dmgSkinImg)
            //{
            //    foreach (var jsonDmg in dmgSkins)
            //    {
            //        //Console.Write("debug");
            //        string itemId = "0" + jsonDmg.itemId;
            //        if (itemImg.Name == itemId)
            //        {
            //            if (itemImg["info"].HasChild("sample"))
            //            {
            //                Bitmap dmgSkinPng = null;
            //                WZCanvasProperty test = (WZCanvasProperty)itemImg["info"]["sample"];
            //                if (test.HasChild("_outlink"))
            //                {
            //                    string path = test["_outlink"].ValueOrDie<string>();
            //                    path = path.Substring(path.IndexOf('/') + 1);
            //                    dmgSkinPng = xz.ResolvePath(path).ValueOrDie<Bitmap>();
            //                } else if (test.HasChild("_inlink"))
            //                {
            //                    string path = test["_inlink"].ValueOrDie<string>();
            //                    string[] pathList = path.Split('/');
            //                    dmgSkinPng = dmgSkinImg[pathList[0]][pathList[1]][pathList[2]].ValueOrDie<Bitmap>();
            //                } else
            //                {
            //                    dmgSkinPng = test.Value;
            //                }
            //                dmgSkinPng.Save($@"F:\Bots\workspace\dmgskin\{itemId}.png", ImageFormat.Png);
            //                Console.WriteLine($"Dumped {jsonDmg.itemId} - {jsonDmg.itemName}");
            //                count++;
            //            }
            //        }
            //    }
            //}

            foreach (var itemImg2 in dmgSkinImg2)
            {
                foreach (var jsonDmg in dmgSkins)
                {
                    //Console.Write("debug");
                    string itemId = "0" + jsonDmg.itemId;
                    if (itemImg2.Name == itemId)
                    {
                        if (itemImg2["info"].HasChild("sample"))
                        {
                            Bitmap dmgSkinPng = null;
                            WZCanvasProperty test = (WZCanvasProperty)itemImg2["info"]["sample"];
                            if (test.HasChild("_outlink"))
                            {
                                string path = test["_outlink"].ValueOrDie<string>();
                                path = path.Substring(path.IndexOf('/') + 1);
                                dmgSkinPng = xz.ResolvePath(path).ValueOrDie<Bitmap>();
                            }
                            else if (test.HasChild("_inlink"))
                            {
                                string path = test["_inlink"].ValueOrDie<string>();
                                string[] pathList = path.Split('/');
                                if (path.Contains("2630086"))
                                {
                                    dmgSkinPng = dmgSkinImg2[pathList[0]][pathList[1]][pathList[2]][pathList[3]].ValueOrDie<Bitmap>();
                                } else
                                {
                                    dmgSkinPng = dmgSkinImg2[pathList[0]][pathList[1]][pathList[2]].ValueOrDie<Bitmap>();
                                }
                            }
                            else
                            {
                                dmgSkinPng = test.Value;
                            }
                            dmgSkinPng.Save($@"F:\Bots\workspace\newdmgskin\{itemId}.png", ImageFormat.Png);
                            Console.WriteLine($"Dumped {jsonDmg.itemId} - {jsonDmg.itemName}");
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
