namespace ImageManager

open System
open Eto.Forms
open Eto.Drawing
open System.ComponentModel

type IMFormModel () =

    let mutable _path = "C:\\Zircon Server\\Data Works\\Test"
    let mutable _needSub = Nullable false
    let mutable _status = "Progress: <None>"
    let mutable _progress =0 

    let ev = Event<_, _>()
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = ev.Publish

    member this.path
        with get() = _path
        and set v =
            _path <- v
            ev.Trigger(this, new PropertyChangedEventArgs("path"))

    member this.needSub
        with get() = _needSub
        and set v =
            _needSub <- v
            ev.Trigger(this, new PropertyChangedEventArgs("needSub"))

    member this.status
        with get() = _status
        and set v =
            _status <- v
            ev.Trigger(this, new PropertyChangedEventArgs("status"))

    member this.progress
        with get() = _progress
        and set v =
            _progress <- v
            ev.Trigger(this, new PropertyChangedEventArgs("progress"))

    member this.convertLibraries () =
        this.progress <- 10

    member this.createLibraries () =
        this.progress <- 100

type IMForm () =
    inherit Form()
    do
        // initialize

        base.Title <- "ImageManager"
        base.Resizable <- false
        base.Width <- 512
        let model = new IMFormModel()

        // setup menus

        base.Menu <- new MenuBar()

        // layout components

        let layout = new TableLayout()
        layout.Padding <- new Padding(8)
        layout.Spacing <- new Size(8, 8)

        let label = new Label(Text = "Select Folder:")
        let folderTextBox = new TextBox()
        folderTextBox.TextBinding.Bind(model, (fun m -> m.path)) |> ignore
        let row = TableRow [| TableCell label; TableCell (folderTextBox, true) |]
        layout.Rows.Add(row)

        let label = new Label(Text = "Include SubFolders:")
        let subCheckBox = new CheckBox()
        subCheckBox.CheckedBinding.Bind(model, (fun m -> m.needSub)) |> ignore
        let row = TableRow [| TableCell label; TableCell subCheckBox |]
        layout.Rows.Add(row)

        layout.Rows.Add(new TableRow())

        let statLabel = new Label()
        statLabel.TextBinding.Bind(model, (fun m -> m.status)) |> ignore
        let progressBar = new ProgressBar(MinValue = 0, MaxValue = 100)
        progressBar.Bind("Value", model, "progress") |> ignore
        let statPanel = new StackLayout([| StackLayoutItem statLabel; StackLayoutItem progressBar |])
        statPanel.HorizontalContentAlignment <- HorizontalAlignment.Stretch

        let convertButton = new Button(Text = "Convert Libraries")
        let convertCmd = new Command()
        convertCmd.Executed.Add(fun _ -> model.convertLibraries())
        convertButton.Command <- convertCmd
        let createButton = new Button(Text = "Create Libraries")
        let createCmd = new Command()
        createCmd.Executed.Add(fun _ -> model.createLibraries())
        createButton.Command <- createCmd
        let opPanel = new StackLayout([| StackLayoutItem convertButton; StackLayoutItem createButton |])

        let row = TableRow [| TableCell(statPanel, true); TableCell opPanel |]
        layout.Rows.Add(row)

        base.Content <- layout;