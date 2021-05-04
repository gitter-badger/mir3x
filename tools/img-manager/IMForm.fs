namespace ImgManager

open System
open Eto.Forms
open Eto.Drawing
open System.ComponentModel
open System.IO
open System.Threading.Tasks
open ImageManager
open System.Threading

type IMFormModel () =

    let mutable _dir = "C:\\Zircon Server\\Data Works\\Test"
    let mutable _needSub = Nullable false
    let mutable _status = "Progress: <None>"
    let mutable _progress =0 

    let ev = Event<_, _>()
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = ev.Publish

    member this.dir
        with get() = _dir
        and set v =
            _dir <- v
            ev.Trigger(this, new PropertyChangedEventArgs("dir"))

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

        let dir = new DirectoryInfo(this.dir)
        let searchOpt =
            if this.needSub = Nullable true then
                SearchOption.AllDirectories
            else
                SearchOption.TopDirectoryOnly
        let targets =
            if not dir.Exists then [||]
            else dir.GetFiles("*.WTL", searchOpt);

        let mutable totalCount = targets.Length
        let mutable progressCount = 0

        let task = Task.Run(fun () ->
            let parallelOpt = new ParallelOptions( MaxDegreeOfParallelism = 10 )
            Parallel.For(0, totalCount, parallelOpt, fun i ->
                if not (targets.[i].FullName.EndsWith("_S.wtl")) then
                    using (new WTLLibrary(targets.[i].FullName)) (fun wtl ->
                        let lib = wtl.Convert ()
                        lib.Save(Path.ChangeExtension(targets.[i].FullName, @".Zl"))
                    )
                Interlocked.Increment(ref progressCount) |> ignore
            ) |> ignore
        )

        (new Thread(fun () ->
            while not task.IsCompleted do
                this.status <- $"{progressCount} of {totalCount}."
                this.progress <-
                    if totalCount = 0 then 100
                    else progressCount / totalCount
                Thread.Sleep 100
            this.status <- "Completed."
        )).Start()

    member this.createLibraries () =

        let dir = new DirectoryInfo(this.dir)
        let searchOpt =
            if this.needSub = Nullable true then
                SearchOption.AllDirectories
            else
                SearchOption.TopDirectoryOnly
        let targets =
            if not dir.Exists then [||]
            else dir.GetDirectories("*.*", searchOpt);
        let targets =
            if (not dir.Exists) || (targets.Length <> 0) then targets
            else [|dir|]

        let mutable totalCount = targets.Length
        let mutable progressCount = 0

        let task = Task.Run(fun () ->
            let parallelOpt = new ParallelOptions( MaxDegreeOfParallelism = 15 )
            Parallel.For(0, totalCount, parallelOpt, fun i ->

                let placementsFile = targets.[i].FullName + "\\Placements.txt"
                let placements =
                    if File.Exists placementsFile then
                        File.ReadAllLines placementsFile
                    else [||]
                let lib = new Mir3Library( Images = Array.zeroCreate placements.Length )

                let parallelOpt = new ParallelOptions( MaxDegreeOfParallelism = 15 )
                Parallel.For(0, placements.Length, parallelOpt, fun (j: int) ->

                    let fileName = String.Format(targets.[i].FullName + "\\{0:00000}.bmp", j)
                    if File.Exists fileName then
                        let placement = placements.[j].Split ','
                        let x = int16 placement.[0]
                        let y = int16 placement.[1]

                        using (new System.Drawing.Bitmap(fileName)) (fun image ->
                            lib.Images.[j] <- new Mir3Image (
                                Width = int16 image.Width,
                                Height = int16 image.Height,
                                OffSetX = x,
                                OffSetY = y,
                                Data = MImage.GetBytes image
                            )
                        )

                    ()
                ) |> ignore

                lib.Save(targets.[i].FullName + ".Zl")
                Interlocked.Increment(ref progressCount) |> ignore
            )
        )

        (new Thread(fun () ->
            while not task.IsCompleted do
                this.status <- $"{progressCount} of {totalCount}."
                this.progress <-
                    if totalCount = 0 then 100
                    else progressCount / totalCount
                Thread.Sleep 100
            this.status <- "Completed."
        )).Start()

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
        folderTextBox.TextBinding.Bind(model, (fun m -> m.dir)) |> ignore
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