using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

class Vox: GameWindow{
    public Vox(int width, int height, string title) : 
    base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) 
    { }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (KeyboardState.IsKeyDown(Keys.Escape)){
            Close();
        }
    }
}