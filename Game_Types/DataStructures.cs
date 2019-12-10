using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Game
{
    //[Serializable]
    //public struct Vec2
    //{
    //    public float X, Y;
    //    public Vec2(float x, float y) { X = x; Y = y; }
    //    public Vec2(Vector2 vec) { X = vec.X; Y = vec.Y; }
    //    public Vector2 Convert() { return new Vector2(X, Y); }
    //};
    //[Serializable]
    //public struct Rect
    //{
    //    public int X, Y, Width, Height;
    //    public Rect(int x, int y, int w, int h) { X = x; Y = y; Width = w; Height = h; }
    //    public Rect(Rectangle rect) { X = rect.X; Y = rect.Y; Width = rect.Width; Height = rect.Height; }
    //    public Rectangle Convert() { return new Rectangle(X, Y, Width, Height); }
    //};
    //[Serializable]
    //public struct Col
    //{
    //    public int R,G,B;
    //    public Col(int r, int g, int b) { R = r; G = g; B = b; }
    //    public Col(Color color) { R = color.R; G = color.G; B = color.B; }
    //    public Color Convert() 
    //    { 
    //        return new Color(R,G,B,255);
    //    }
    //};
    //[Serializable]
    //public struct Tex2D
    //{
    //    string filepath;
    //    public Tex2D(string fp) { filepath = fp; }
    //    public Texture2D GetTexture(GraphicsDevice graphicsDevice) 
    //    {
    //        try
    //        {
    //            return Texture2D.FromStream(graphicsDevice, new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read));
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }
    //};
}
