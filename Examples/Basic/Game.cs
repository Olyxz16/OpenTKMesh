using OpenTKMesh.Materials;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenTKMesh;

public class Game : GameWindow
{

    private const int WIDTH = 1200;
    private const int HEIGHT = 900;
    private const string TITLE = "Test";

    public Game()
        : base(GameWindowSettings.Default, new NativeWindowSettings() {
            Size = (WIDTH, HEIGHT), Title = TITLE 
        }) {
    }
    
    protected override void OnLoad()
    {
        base.OnLoad();

        GL.Enable(EnableCap.DepthTest);
        GL.ClearColor(0.7f, 0.7f, 0.7f, 1f);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Material material = new UnlitMaterial(Vector4.One, Texture.FromPath("./Textures/texture.png"));
        Primitives.InstantiateQuad(Vector3.Zero, 0.5f, material);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.Enable(EnableCap.DepthTest);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        float angle = 2f*(float)Math.PI*(float)e.Time/20;
        //MeshHandler.Meshes[0].Rotate(new Vector3(angle, angle, angle));
        MeshHandler.Draw();
        
        SwapBuffers();
    }



    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        KeyboardState input = KeyboardState;

        if (input.IsKeyDown(Keys.Escape))
        {
            Close();
        }

    }

}
