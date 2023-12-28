using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

class Vox: GameWindow{
    public Vox(int width, int height, string title) : 
    base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) 
    { }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(Color.AntiqueWhite);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // stuff

        SwapBuffers(); //OpenGL paints to a screen in the background then swaps with the one on screen when rendering to reduce tearing
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (KeyboardState.IsKeyDown(Keys.Escape)){
            Close();
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }
}