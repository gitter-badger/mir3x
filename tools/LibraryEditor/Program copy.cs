using System;
using System.Runtime.InteropServices;
using Eto.Forms;

namespace ImageManager
{
	class MainClass
	{
		[STAThread]
		public static void Main(string[] args)
		{
			string etoPlatform = Eto.Platforms.Gtk;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				etoPlatform = Eto.Platforms.Mac64;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				etoPlatform = Eto.Platforms.Wpf;
			new Application(etoPlatform).Run(new MainForm());
		}
	}
}
