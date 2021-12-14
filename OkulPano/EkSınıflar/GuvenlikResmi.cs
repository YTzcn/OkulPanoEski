using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// GuvenlikResmi için özet açıklama
/// </summary>
public class GuvenlikResmi
{
    int uzunluk;
    string fontIsmi;
    float fontBoyut;
    string sayi;
    public string Sayi
    {
        get
        {
            return sayi;
        }
    }

    public GuvenlikResmi(int uzunluk,string fontIsmi,float fontBoyut)
    {
        this.uzunluk = uzunluk;
        this.fontIsmi = fontIsmi;
        this.fontBoyut = fontBoyut;
    }

    char KaraterGetir()
    {
        bool bulundu = false;
        char c=' ';
        while(!bulundu)
        {
            Random rnd = new Random();
            int sayi = rnd.Next(65, 122);
            if (!(sayi > 90 && sayi < 97))
            {
                bulundu = true;
                c = (char)sayi;
            }
        }
        return c;
    }

    public string GuvenlikKodu()
    {
        string sayi = "";
        for (int i = 0; i < this.uzunluk; i++)
        {
            sayi += KaraterGetir().ToString();
            Thread.Sleep(100);
        }
        return sayi;
    }

    public Bitmap GuvenlikResmiGonder()
    {
        string sayi = GuvenlikKodu();
        this.sayi = sayi;
        Bitmap bmp = new Bitmap(5, 5);
        Graphics gr = Graphics.FromImage(bmp);
        Bitmap bmpyeni = new Bitmap((int)gr.MeasureString(sayi, new Font(fontIsmi, fontBoyut)).Width, (int)gr.MeasureString(sayi, new Font(fontIsmi, fontBoyut)).Height);
        Graphics gryeni = Graphics.FromImage(bmpyeni);
        HatchBrush firca = new HatchBrush(HatchStyle.SmallConfetti, Color.White);
        gryeni.FillRectangle(firca, new Rectangle(0, 0, bmpyeni.Width, bmpyeni.Height));
        gryeni.DrawString(sayi, new Font(fontIsmi, fontBoyut), Brushes.Aqua, new PointF(0, 0));
        gryeni.DrawLine(new Pen(Brushes.Blue, 3f), -20, bmpyeni.Width / 2, 50, 30);
        return bmpyeni;
    }
}