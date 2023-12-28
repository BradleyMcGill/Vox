using System.Diagnostics;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

class Vox : GameWindow
{
    float[] vertices = {
     0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f, // top right
     0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f,  // bottom right
    -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f,  // bottom left
    -0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 1.0f,   // top left
    };

    uint[] indices = {  // note that we start from 0!
    0, 2, 6,   // first triangle
    2, 4, 6    // second triangle
    };

    int VertexBufferObject;
    int ElementBufferObject;
    int VertexArrayObject;
    Shader shader;
    Stopwatch _timer = new Stopwatch();

    public Vox(int width, int height, string title) :
    base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title })
    {
        shader = new Shader("shader.vert", "shader.frag");
        _timer.Start();
    }



    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(Color.AntiqueWhite);

        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);

        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        ElementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // stuff
        shader.Use();

        double timeValue = _timer.Elapsed.TotalSeconds;
        float greenValue = (float)Math.Sin(timeValue) / 2.0f + 0.5f;
        int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
        GL.Uniform4(vertexColorLocation, 0.0f, greenValue, 0.0f, 1.0f);

        GL.BindVertexArray(VertexArrayObject);
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

        SwapBuffers(); //OpenGL paints to a screen in the background then swaps with the one on screen when rendering to reduce tearing
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        shader.Dispose();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }
}