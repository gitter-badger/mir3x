namespace LibEditor

open Eto.Forms
open Eto.Drawing

type LEForm () =
    inherit Form()
    do
        // initialize

        base.Title <- "LibraryEditor"
        base.Size <- new Size(814, 1008)

        // setup menus

        let fileMenu = new ButtonMenuItem(Text = "File")

        let newCmd = new Command(MenuText = "New")
        fileMenu.Items.Add newCmd |> ignore
        let openCmd = new Command(MenuText = "Open")
        fileMenu.Items.Add openCmd |> ignore
        fileMenu.Items.Add (new SeparatorMenuItem())
        let save1Cmd = new Command(MenuText = "Save")
        fileMenu.Items.Add save1Cmd |> ignore
        let save2Cmd = new Command(MenuText = "Save As")
        fileMenu.Items.Add save2Cmd |> ignore

        let editMenu = new ButtonMenuItem(Text = "Edit")

        let copyCmd = new Command(MenuText = "Copy To")
        editMenu.Items.Add copyCmd |> ignore
        let countCmd = new Command(MenuText = "Count Blanks")
        editMenu.Items.Add countCmd |> ignore
        let removeCmd = new ButtonMenuItem(Text = "Remove Blanks")
        let safeCmd = new Command(MenuText = "Safe")
        removeCmd.Items.Add safeCmd |> ignore
        editMenu.Items.Add removeCmd
        let convertCmd = new Command(MenuText = "Converter")
        editMenu.Items.Add convertCmd |> ignore

        let menus: MenuItem array = [| fileMenu; editMenu |]
        let menus = new MenuBar(menus)
        menus.IncludeSystemItems <- MenuBarSystemItems.Quit
        base.Menu <- menus

        // sub dialogs

        let openLibDialog = new OpenFileDialog()
        openLibDialog.Filters.Add(FileFilter("Zircon Library", [|".Zl"|]))
        let saveLibDialog = new SaveFileDialog()
        saveLibDialog.Filters.Add(FileFilter("Zircon Library", [|".Zl"|]))
        let importImgDialog = new OpenFileDialog()
        importImgDialog.Filters.Add(FileFilter("Images", [|".BMP"; ".JPG"; ".GIF"; ".PNG"|]))
        importImgDialog.Filters.Add(FileFilter("All Files", [|".*"|]))
        let openWeMDialog = new OpenFileDialog()
        openWeMDialog.Filters.Add(FileFilter("WeMade", [|".Wil"; ".Wtl"|]))
        let openMergDialog = new OpenFileDialog()
        openMergDialog.Filters.Add(FileFilter("Zircon Library", [|".Zl"|]))
        openMergDialog.Filters.Add(FileFilter("WeMade", [|".Wil"; ".Wtl"|]))
        openMergDialog.Filters.Add(FileFilter("Shanda", [|".Wzl"; ".Miz"|]))
        openMergDialog.Filters.Add(FileFilter("Lib", [|".Lib"|]))

        // layout components

        let radios = new RadioButtonList( Orientation = Orientation.Horizontal )
        radios.Items.Add("Image", "image")
        radios.Items.Add("Shadow", "shadow")
        radios.Items.Add("Overlay", "overlay")

        let widthLabel = new Label(Text = "<No Image>")
        let heightLabel = new Label(Text = "<No Image>")
        let offXBox = new TextBox()
        let offYBox = new TextBox()

        let addButton = new Button(Text = "Add Images")
        let delButton = new Button(Text = "Delete Images")
        let replButton = new Button(Text = "Replace Image")
        let instButton = new Button(Text = "Insert Images")
        let mergButton = new Button(Text = "Merge Libraries")
        let exptButton = new Button(Text = "Export Images")
        let addBlkButton = new Button(Text = "Add Blanks")
        let insBlkButton = new Button(Text = "Insert Blanks")

        let preButton = new Button(Text = "<<")
        let nextButton = new Button(Text = ">>")
        let numSelect = new NumericStepper(
                            MaxValue = 650000.0,
                            MinValue = 0.0,
                            MaximumDecimalPlaces = 0,
                            Increment = 1.0)
        let zoomBar = new Slider(
                        MaxValue = 10,
                        MinValue = 1,
                        Value = 1,
                        Orientation = Orientation.Horizontal)
        let opsCheckList = new CheckBoxList()
        opsCheckList.Items.Add "No Blurring"
        opsCheckList.Items.Add "No Anti-aliasing"

        let imgBox = new ImageView()
        imgBox.BackgroundColor <- new Color(0f, 0f, 0f)
        let lsView = new Label(Text = "TODO")

        let statLabel = new Label(Text = "Selected Image:")
        let progressBar = new ProgressBar(MinValue = 0, MaxValue = 100)

        let opPanel = new StackLayout([|
            new TableLayout [|
                TableRow [| TableCell (new Label(Text = "Width")); TableCell widthLabel |];
                TableRow [| TableCell (new Label(Text = "Height")); TableCell heightLabel |];
                TableRow [| TableCell (new Label(Text = "OffSet X:")); TableCell offXBox |];
                TableRow [| TableCell (new Label(Text = "OffSet Y:")); TableCell offYBox |];
                TableRow [| TableCell addButton; TableCell delButton |];
                TableRow [| TableCell replButton; TableCell instButton |];
                TableRow [| TableCell mergButton; TableCell exptButton |];
                TableRow [| TableCell addBlkButton; TableCell insBlkButton |];
            |] |> StackLayoutItem;
            new TableLayout [|
                TableRow [| TableCell preButton; TableCell numSelect; TableCell nextButton |]
            |] |> StackLayoutItem;
            zoomBar |> StackLayoutItem;
            opsCheckList |> StackLayoutItem;
        |])
        opPanel.Orientation <- Orientation.Vertical
        opPanel.HorizontalContentAlignment <- HorizontalAlignment.Stretch

        let panel = new StackLayout([|
            radios |> StackLayoutItem;
            new TableLayout [|
                TableRow [| TableCell opPanel; TableCell imgBox |]
            |] |> StackLayoutItem;
            StackLayoutItem (lsView, true);
            statLabel |> StackLayoutItem;
            progressBar |> StackLayoutItem;
        |])
        panel.Orientation <- Orientation.Vertical
        panel.HorizontalContentAlignment <- HorizontalAlignment.Stretch
        panel.Padding <- new Padding(8)
        panel.Spacing <- 8

        base.Content <- panel
