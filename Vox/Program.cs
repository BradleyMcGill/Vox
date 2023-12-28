using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

var nativeWindowSettings = new NativeWindowSettings()
{
    ClientSize = new Vector2i(800, 600),
    Title = "Vox",
    // This is needed to run on macos
    Flags = ContextFlags.ForwardCompatible,
};

using (var window = new Vox(GameWindowSettings.Default, nativeWindowSettings))
{
    window.Run();
}