namespace ImgManager
module Program =

    open System
    open System.Runtime.InteropServices;

    [<EntryPoint>]
    [<STAThread>]
    let Main(args) =
        let etoPlatform =
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) then
                Eto.Platforms.Mac64
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) then
                Eto.Platforms.Wpf
            else
                Eto.Platforms.Gtk
        let app = new Eto.Forms.Application(etoPlatform)
        app.Run(new IMForm())
        0