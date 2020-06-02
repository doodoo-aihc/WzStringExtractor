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
    class ExtractImg
    {
        public ExtractImg(string fileName, string location, string jsonLocation, string iconLocation)
        {
            int count = 0;
            var dmgSkins = JsonConvert.DeserializeObject<List<DamageSkin>>(File.ReadAllText(jsonLocation));

            Directory.CreateDirectory(location);
            Directory.CreateDirectory(iconLocation);

            WZFile xz = new WZFile(fileName, WZVariant.MSEA, false);
            WZImage dmgSkinImg = (WZImage)xz.MainDirectory["Consume"]["0243.img"];
            WZImage dmgSkinImg2 = (WZImage)xz.MainDirectory["Consume"]["0263.img"];

            foreach (var itemImg in dmgSkinImg)
            {
                foreach (var jsonDmg in dmgSkins)
                {
                    //Console.Write("debug");
                    string itemId = "0" + jsonDmg.itemId;

                    if (itemId.StartsWith("0243"))
                    {
                        if (itemImg.Name == itemId)
                        {
                            if (itemImg["info"].HasChild("sample"))
                            {
                                Bitmap dmgSkinPng = null;
                                Bitmap dmgSkinIconPng = null;
                                WZCanvasProperty iconTest = null;
                                WZCanvasProperty test = (WZCanvasProperty)itemImg["info"]["sample"];

                                if (itemImg["info"].HasChild("icon"))
                                {
                                    if (itemImg["info"]["icon"].Type == WZObjectType.UOL)
                                    {
                                        iconTest = (WZCanvasProperty)itemImg["info"]["iconRaw"];
                                    } else
                                    {
                                        iconTest = (WZCanvasProperty)itemImg["info"]["icon"];
                                    }

                                    if (iconTest.HasChild("_outlink"))
                                    {
                                        string path = iconTest["_outlink"].ValueOrDie<string>();
                                        path = path.Substring(path.IndexOf('/') + 1);
                                        dmgSkinIconPng = xz.ResolvePath(path).ValueOrDie<Bitmap>();
                                    }
                                    else if (iconTest.HasChild("_inlink"))
                                    {
                                        string path = iconTest["_inlink"].ValueOrDie<string>();
                                        string[] pathList = path.Split('/');
                                        dmgSkinIconPng = dmgSkinImg[pathList[0]][pathList[1]][pathList[2]].ValueOrDie<Bitmap>();
                                    }
                                    else
                                    {
                                        dmgSkinIconPng = iconTest.Value;
                                    }
                                }
                                // Damage Skin Section
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
                                    dmgSkinPng = dmgSkinImg[pathList[0]][pathList[1]][pathList[2]].ValueOrDie<Bitmap>();
                                }
                                else
                                {
                                    dmgSkinPng = test.Value;
                                }
                                dmgSkinPng.Save($@"{location}\{itemId}.png", ImageFormat.Png);
                                dmgSkinIconPng.Save($@"{iconLocation}\{itemId}.png", ImageFormat.Png);
                                Console.WriteLine($"Dumped {jsonDmg.itemId} - {jsonDmg.itemName}");
                                count++;
                            }
                        }
                    }
                }
            }

            foreach (var itemImg2 in dmgSkinImg2)
            {
                foreach (var jsonDmg in dmgSkins)
                {
                    //Console.Write("debug");
                    string itemId = "0" + jsonDmg.itemId;
                    if (itemId.StartsWith("0263"))
                    {
                        if (itemImg2.Name == itemId)
                        {
                            if (itemImg2["info"].HasChild("sample"))
                            {
                                Bitmap dmgSkinPng = null;
                                Bitmap dmgSkinIconPng = null;
                                WZCanvasProperty iconTest = null;
                                WZCanvasProperty test = (WZCanvasProperty)itemImg2["info"]["sample"];

                                if (itemImg2["info"].HasChild("icon"))
                                {
                                    if (itemImg2["info"]["icon"].Type == WZObjectType.UOL)
                                    {
                                        iconTest = (WZCanvasProperty)itemImg2["info"]["iconRaw"];
                                    }
                                    else
                                    {
                                        iconTest = (WZCanvasProperty)itemImg2["info"]["icon"];
                                    }

                                    if (iconTest.HasChild("_outlink"))
                                    {
                                        string path = iconTest["_outlink"].ValueOrDie<string>();
                                        path = path.Substring(path.IndexOf('/') + 1);
                                        dmgSkinIconPng = xz.ResolvePath(path).ValueOrDie<Bitmap>();
                                    }
                                    else if (iconTest.HasChild("_inlink"))
                                    {
                                        string path = iconTest["_inlink"].ValueOrDie<string>();
                                        string[] pathList = path.Split('/');
                                        dmgSkinIconPng = dmgSkinImg2[pathList[0]][pathList[1]][pathList[2]].ValueOrDie<Bitmap>();
                                    }
                                    else
                                    {
                                        dmgSkinIconPng = iconTest.Value;
                                    }
                                }
                                // damage skin
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
                                    }
                                    else
                                    {
                                        dmgSkinPng = dmgSkinImg2[pathList[0]][pathList[1]][pathList[2]].ValueOrDie<Bitmap>();
                                    }
                                }
                                else
                                {
                                    dmgSkinPng = test.Value;
                                }
                                dmgSkinIconPng = iconTest.Value;
                                dmgSkinPng.Save($@"{location}\{itemId}.png", ImageFormat.Png);
                                dmgSkinIconPng.Save($@"{iconLocation}\{itemId}.png", ImageFormat.Png);
                                Console.WriteLine($"Dumped {jsonDmg.itemId} - {jsonDmg.itemName}");
                                count++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Successfully dumped {count.ToString()} number of damage skins");
            Console.ReadKey();
        }
    }
}
