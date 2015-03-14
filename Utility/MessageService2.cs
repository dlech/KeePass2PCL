using System;

namespace KeePass2PCL
{
	public static class MessageService
	{
		public static string NewLine {
			get { return Environment.NewLine; }
		}

		public static string NewParagraph {
			get { return Environment.NewLine + Environment.NewLine; }
		}

		public static void ShowWarning (params object[] line)
		{
			throw new NotImplementedException ();
		}
	}
}

