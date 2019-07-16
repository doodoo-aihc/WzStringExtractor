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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your mode");
            Console.WriteLine("1. Extract Damage Skin strings");
            Console.WriteLine("2. Extract Damage Skin img");
            int mode = Convert.ToInt32(Console.ReadLine());

            switch (mode)
            {
                case 1:
                    ExtractString extractString = new ExtractString();
                    break;

                case 2:
                    ExtractImg extractImg = new ExtractImg();
                    break;

                default:
                    break;
            }
        }
    }

    class DamageSkins
    {
        public List<DamageSkin> damageSkins { get; set; }
    }

    class DamageSkin
    {
        public string itemId { get; set; }
        public string itemName { get; set; }
    }
}
