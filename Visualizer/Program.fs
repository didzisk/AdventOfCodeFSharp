// Learn more about F# at http://fsharp.org

open System


(*
[<STAThread>]
do
    Application.Run(mainForm)
*)

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let f = GridForm.mf

    let mutable x=(Console.ReadLine() |> int)

    while x>0 do
        GridForm.rr f x
        x<-(Console.ReadLine() |> int)

    

    0 // return an integer exit code

(*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visualizer
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [<STAThread>]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
*)