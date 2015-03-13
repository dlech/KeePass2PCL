using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace KeePassLib.Utility
{
	public static class ColorTranslator
	{
		static Regex longForm = new Regex("^#([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})([0-9A-Fa-f]{2})$");
		static Regex shortForm = new Regex("^#([0-9A-Fa-f])([0-9A-Fa-f])([0-9A-Fa-f])$");

		public static Color FromHtml(string htmlColor)
		{
			try {
				return Color.FromName(htmlColor);
			} catch (Exception) {
				Match match = longForm.Match(htmlColor);
				if (!match.Success)
					match = shortForm.Match(htmlColor);
				if (match.Success) {
					var r = int.Parse(match.Groups [1].Value);
					var g = int.Parse(match.Groups [2].Value);
					var b = int.Parse(match.Groups [3].Value);
					return Color.FromArgb(r, g, b);
				}
			}
			throw new Exception(string.Format("Could not parse HTML color '{0}'.", htmlColor));
		}

		public static string ToHtml(Color htmlColor)
		{
			return string.Format("#{0:x2}{1:x2}{2:x2}", htmlColor.R, htmlColor.G, htmlColor.B);
		}
	}
}

