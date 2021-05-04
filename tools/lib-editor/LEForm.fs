namespace LibEditor

open Eto.Forms

type LEForm () =
    inherit Form()
    do
        // initialize

        base.Title <- "LibraryEditor"
        base.Resizable <- false

        // setup menus

        base.Menu <- new MenuBar()