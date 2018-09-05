using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteMaker
{
    public partial class UserControl1: UserControl
    {
        const int TILESIZE = 8;
        public List<Color> ColorList = new List<Color>();
        public UserControl1()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            List<String> TileStringList = new List<string>();
            Bitmap TempBitMap = new Bitmap(openFileDialog1.FileName);

            int DividedWidth = TempBitMap.Width / TILESIZE;
            for (int x = 0; x <= DividedWidth - 1; x++)
            {
                TileStringList.Add(DrawTile(TempBitMap, x * TILESIZE));
            }

            foreach(String TileString in TileStringList)
            {
                richTextBox1.Text += TileString;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            SpriteBox.ImageLocation = openFileDialog1.FileName;

            Bitmap TempBitMap = new Bitmap(openFileDialog1.FileName);
            for (int x = 0; x <= TempBitMap.Width - 1; x++)
            {
                for (int y = 0; y <= TempBitMap.Height - 1; y++)
                {
                    CheckForDoubleColors(TempBitMap.GetPixel(x,y));
                }
            }
        }
        
        private void CheckForDoubleColors(Color pixel)
        {
            bool IsPixelUsed = false;
            foreach(Color PixelListCheck in ColorList)
            {
               if (pixel == PixelListCheck)
               {
                    IsPixelUsed = true;
               }
            }

            if(IsPixelUsed == false)
            {
                ColorList.Add(pixel);
            }
        }

        private string DrawTile(Bitmap TempBitMap, int TileX)
        {
            string TileString = "";
            for (int y = 0; y <= TempBitMap.Height - 1; y++)
            {
                for (int x = TileX; x <= TileX + TILESIZE - 1; x++)
                {
                    TileString += GetPixelColor(TempBitMap.GetPixel(x, y)).ToString();
                }
                TileString += "\n";
            }
            TileString = TileString.Replace("\n", "\ndc.l 0x");
            return TileString;
        }
        private int GetPixelColor(Color color)
        {
            for(int x = 0; x <= ColorList.Count - 1; x++)
            {
                if(ColorList[x] == color)
                {
                    return x;
                }
            }
            return 0;
        }
        private string ConvertDecimalToHexString(int x)
        {
           
            string Hex = "";
            switch(x)
            {
                case 10: Hex = "A"; break;
                case 11: Hex = "B"; break;
                case 12: Hex = "C"; break;
                case 13: Hex = "D"; break;
                case 14: Hex = "E"; break;
                case 15: Hex = "F"; break;
            }
            return Hex;
        }
    }
}
