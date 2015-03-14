using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KeePass2PCL.Utility
{
	public static class ColorTranslator
	{
		static Regex longForm = new Regex("^#([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})$");
		static Regex shortForm = new Regex("^#([0-9A-Fa-f])([0-9A-Fa-f])([0-9A-Fa-f])$");

		public static Color FromHtml(string htmlColor)
		{
			Match match = longForm.Match(htmlColor);
			if (!match.Success)
				match = shortForm.Match(htmlColor);
			if (match.Success) {
				var r = int.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
				var g = int.Parse(match.Groups[2].Value, NumberStyles.HexNumber);
				var b = int.Parse(match.Groups[3].Value, NumberStyles.HexNumber);
				return Color.FromArgb(r, g, b);
			}
			throw new Exception(string.Format("Could not parse HTML color '{0}'.", htmlColor));
		}

		public static string ToHtml(Color htmlColor)
		{
			return string.Format("#{0:x2}{1:x2}{2:x2}", htmlColor.R, htmlColor.G, htmlColor.B);
		}
	}
}

