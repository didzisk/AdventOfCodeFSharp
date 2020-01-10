module GridForm

open System.Drawing
open System.Windows.Forms

let mf=
    let mainForm = new Form(Width = 620, Height = 450, Text = "Visualizer")
    mainForm

let pp (mainForm:Form) =

    mainForm.Show()

    let myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
    let formGraphics=mainForm.CreateGraphics();
    formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 200, 300));
    myBrush.Dispose();
    formGraphics.Dispose();
    mainForm

let rr (mainForm:Form) x =

    mainForm.Show()

    let myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
    let formGraphics=mainForm.CreateGraphics();
    formGraphics.FillRectangle(myBrush, new Rectangle(x*10, x*10, 10, 10));
    myBrush.Dispose();
    formGraphics.Dispose();
    mainForm