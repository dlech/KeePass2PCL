/*
  KeePass Password Safe - The Open-Source Password Manager
  Copyright (C) 2003-2012 Dominik Reichl <dominik.reichl@t-online.de>
  
  Modified to be used with Mono for Android. Changes Copyright (C) 2013 Philipp Crocoll

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;
#if KeePassLibAndroid
using Android.Graphics;
#else
using System.Windows.Forms;
#endif

namespace KeePassLib.Utility
{
	public static class GfxUtil
	{
#if KeePassLibAndroid
		public static Android.Graphics.Bitmap LoadImage(byte[] pb)
#else
		public static Image LoadImage(byte[] pb)
#endif
		{
			if(pb == null) throw new ArgumentNullException("pb");

			MemoryStream ms = new MemoryStream(pb, false);
			try { return LoadImagePriv(ms); }
			catch(Exception)
			{
#if KeePassLibAndroid
				Android.Graphics.Bitmap imgIco = TryLoadIco(pb);
#else
				Image imgIco = TryLoadIco(pb);
#endif
				if(imgIco != null) return imgIco;
				throw;
			}
			finally { ms.Close(); }
		}

#if KeePassLibAndroid
		private static Android.Graphics.Bitmap LoadImagePriv(Stream s)
		{
			Android.Graphics.Bitmap img = BitmapFactory.DecodeStream(s);
			return img;			
		}
#else
		private static Image LoadImagePriv(Stream s)
		{
			// Image.FromStream wants the stream to be open during
			// the whole lifetime of the image; as we can't guarantee
			// this, we make a copy of the image
			Image imgSrc = null;
			try
			{
#if !KeePassLibSD
				imgSrc = Image.FromStream(s);
				Image img = new Bitmap(imgSrc.Width, imgSrc.Height,
					PixelFormat.Format32bppArgb);
#else
				imgSrc = new Bitmap(s);
				Image img = new Bitmap(imgSrc.Width, imgSrc.Height);
#endif

				using(Graphics g = Graphics.FromImage(img))
				{
					g.Clear(Color.Transparent);
					g.DrawImage(imgSrc, 0, 0);
				}

				return img;
			}
			finally { if(imgSrc != null) imgSrc.Dispose(); }
		}
#endif

#if KeePassLibAndroid
		private static Android.Graphics.Bitmap TryLoadIco(byte[] pb)
		{
			throw new NotImplementedException();
		}
#else
		private static Image TryLoadIco(byte[] pb)
		{
#if !KeePassLibSD
			MemoryStream ms = new MemoryStream(pb, false);
			try { return (new Icon(ms)).ToBitmap(); }
			catch(Exception) { }
			finally { ms.Close(); }
#endif

			return null;
		}
#endif
	}
}
