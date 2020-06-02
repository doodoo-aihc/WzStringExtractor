using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reWZ;
using reWZ.WZProperties;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace WzStringExtractor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your mode");
            Console.WriteLine("1. Extract Damage Skin strings");
            Console.WriteLine("2. Extract Damage Skin img");
            Console.WriteLine("3. Extract Damage Skin Numbers");
            Console.WriteLine("Please input only 1, 2 or 3");
            int mode = Convert.ToInt32(Console.ReadLine());
            string fileName;
            string outputName;
            string location = "./DamageSkinImage";
            string jsonFile;
            string iconLocation = "./DamageSkinIconImage";
            OpenFileDialog fd = new OpenFileDialog();
            SaveFileDialog output = new SaveFileDialog();
            FolderBrowserDialog dmgSkinsLocation = new FolderBrowserDialog();
            FolderBrowserDialog dmgSkinsIconLocation = new FolderBrowserDialog();

            output.Title = "Save location for JSON";
            output.DefaultExt = ".json";
            output.AddExtension = true;

            dmgSkinsLocation.Description = "Location for extraction";
            dmgSkinsIconLocation.Description = "Location for extraction of skin icon";

            switch (mode)
            {
                case 1:
                    fd.ShowDialog();
                    output.ShowDialog();
                    fileName = fd.FileName;
                    outputName = output.FileName;
                    ExtractString extractString = new ExtractString(fileName, outputName);
                    break;

                case 2:
                    fd.Title = "Select Item.Wz";
                    fd.ShowDialog();
                    //dmgSkinsLocation.ShowDialog();
                    //dmgSkinsIconLocation.ShowDialog();
                    fileName = fd.FileName;
                    //location = dmgSkinsLocation.SelectedPath;
                    //iconLocation = dmgSkinsIconLocation.SelectedPath;
                    fd.Title = "Select skin.json";
                    fd.ShowDialog();
                    jsonFile = fd.FileName;
                    ExtractImg extractImg = new ExtractImg(fileName, location, jsonFile, iconLocation);
                    break;

                case 3:
                    fd.ShowDialog();
                    dmgSkinsLocation.ShowDialog();
                    fileName = fd.FileName;
                    location = dmgSkinsLocation.SelectedPath;
                    ExtractDamageSkinNumbers extractDamageSkinNumbers= new ExtractDamageSkinNumbers(fileName, location);
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
