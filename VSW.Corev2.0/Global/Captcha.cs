using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Web;
using VSW.Core.Web;

namespace VSW.Core.Global
{
	
	public class Captcha
	{
		private string Text
		{
			[CompilerGenerated]
			get
			{
				return this.text;
			}
		}	
		public Bitmap Image { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		private string SessionName
		{
			get
			{
				return this._SessionName;
			}
			set
			{
				this._SessionName = value;
			}
		}
		public static int CharLength
		{
			get
			{
				return Captcha._CharLength;
			}
			set
			{
				Captcha._CharLength = value;
			}
		}
		public static bool CaseSensitive
		{
			get
			{
				return Captcha._CaseSensitive;
			}
			set
			{
				Captcha._CaseSensitive = value;
			}
		}
		private static bool OnlyNumber { get; set; }
		private static bool OnlyChar { get; set; }
		public Captcha(int width, int height)
		{
			this.text = Captcha.GenerateRandomCode();
			string original = string.Concat(new string[]
			{
				"CaptchaANGKORICH.Secure.",
				HttpContext.Current.Request.UserHostAddress,
				".",
				string.Format("yyyy.MM.dd.hh", new object[0]),
				".",
				this.Text
			});
			Session.SetValue(this.SessionName, CryptoString.Encrypt(original));
			this.SetDimensions(width, height);
			this.SetFamilyName(this._fontList[this._random.Next(0, this._fontList.Length - 1)]);
			this.GenerateImage();
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Image.Dispose();
			}
		}
		private void SetDimensions(int width, int height)
		{
			this.Width = width;
			this.Height = height;
		}
		private void SetFamilyName(string familyName)
		{
			try
			{
				Font font = new Font(this._familyName, 12f);
				this._familyName = familyName;
				font.Dispose();
			}
			catch
			{
				this._familyName = FontFamily.GenericSerif.Name;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A310 File Offset: 0x00008510
		private void GenerateImage()
		{
			Bitmap image = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(image);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
			Color color = Color.FromArgb(this._random.Next(128, 255), this._random.Next(128, 255), this._random.Next(128, 255));
			Color backColor = Captcha.ColorInvert(color);
			HatchBrush hatchBrush = new HatchBrush(this.RandomEnum<HatchStyle>(), color, Color.White);
			graphics.FillRectangle(hatchBrush, rectangle);
			float num = (float)(rectangle.Height + 11);
			Font font;
			do
			{
				num -= 1f;
				font = new Font(this._familyName, num, FontStyle.Bold);
			}
			while (graphics.MeasureString(this.Text, font).Width > (float)rectangle.Width);
			StringFormat format = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddString(this.Text, font.FontFamily, (int)font.Style, font.Size, rectangle, format);
			PointF[] destPoints = new PointF[]
			{
				new PointF((float)this._random.Next(rectangle.Width) / 4f, (float)this._random.Next(rectangle.Height) / 4f),
				new PointF((float)rectangle.Width - (float)this._random.Next(rectangle.Width) / 4f, (float)this._random.Next(rectangle.Height) / 4f),
				new PointF((float)this._random.Next(rectangle.Width) / 4f, (float)rectangle.Height - (float)this._random.Next(rectangle.Height) / 4f),
				new PointF((float)rectangle.Width - (float)this._random.Next(rectangle.Width) / 4f, (float)rectangle.Height - (float)this._random.Next(rectangle.Height) / 4f)
			};
			Matrix matrix = new Matrix();
			matrix.Translate(0f, 0f);
			graphicsPath.Warp(destPoints, rectangle, matrix, WarpMode.Perspective, 0f);
			hatchBrush = new HatchBrush(this.RandomEnum<HatchStyle>(), Color.DimGray, backColor);
			graphics.FillPath(hatchBrush, graphicsPath);
			int num2 = Math.Max(rectangle.Width, rectangle.Height);
			for (int i = 0; i < (int)((float)(rectangle.Width * rectangle.Height) / 30f); i++)
			{
				int x = this._random.Next(rectangle.Width);
				int y = this._random.Next(rectangle.Height);
				int width = this._random.Next(num2 / 50);
				int height = this._random.Next(num2 / 50);
				graphics.FillEllipse(hatchBrush, x, y, width, height);
			}
			font.Dispose();
			hatchBrush.Dispose();
			graphics.Dispose();
			this.Image = image;
		}
		private static string GenerateRandomCode()
		{
			ArrayList arrayList = new ArrayList();
			Random random = new Random();
			if (!Captcha.OnlyNumber)
			{
				for (char c = 'A'; c <= 'Z'; c += '\u0001')
				{
					arrayList.Add(c);
				}
				for (char c2 = 'a'; c2 <= 'z'; c2 += '\u0001')
				{
					if (c2 == 'l')
					{
						c2 += '\u0001';
					}
					arrayList.Add(c2);
				}
			}
			if (!Captcha.OnlyChar)
			{
				for (char c3 = '0'; c3 <= '9'; c3 += '\u0001')
				{
					arrayList.Add(c3);
				}
			}
			string text = "";
			for (int i = 0; i < Captcha.CharLength; i++)
			{
				text += arrayList[random.Next(arrayList.Count)].ToString();
			}
			if (!Captcha.CaseSensitive)
			{
				text = text.ToLower();
			}
			return text;
		}
		private T RandomEnum<T>()
		{
			T[] array = (T[])Enum.GetValues(typeof(T));
			return array[this._random.Next(0, array.Length)];
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A788 File Offset: 0x00008988
		private static Color ColorInvert(Color colorIn)
		{
			return Color.FromArgb((int)colorIn.A, (int)(Color.White.R - colorIn.R), (int)(Color.White.G - colorIn.G), (int)(Color.White.B - colorIn.B));
		}

		// Token: 0x04000076 RID: 118
		private string[] _fontList = new string[]
		{
			"Arial",
			"Times New Roman",
			"Verdana",
			"Comic Sans MS",
			"Courier New",
			"Georgia",
			"Impact",
			"Palatino Linotype",
			"Lucida Console",
			"Marlett"
		};

		
		private string _familyName;

		
		private readonly Random _random = new Random();

		
		private string _SessionName = "CaptchaANGKORICH";

		
		private static int _CharLength = 5;

		
		private static bool _CaseSensitive = true;

		
		private string text;
	}
}
