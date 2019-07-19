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
            string location;
            string jsonFile;
            OpenFileDialog fd = new OpenFileDialog();
            SaveFileDialog output = new SaveFileDialog();
            FolderBrowserDialog dmgSkinsLocation = new FolderBrowserDialog();

            output.Title = "Save location for JSON";
            output.DefaultExt = ".json";
            output.AddExtension = true;

            dmgSkinsLocation.Description = "Location for extraction";

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
                    fd.ShowDialog();
                    dmgSkinsLocation.ShowDialog();
                    fileName = fd.FileName;
                    location = dmgSkinsLocation.SelectedPath;
                    fd.ShowDialog();
                    jsonFile = fd.FileName;
                    ExtractImg extractImg = new ExtractImg(fileName, location, jsonFile);
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
