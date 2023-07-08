using OpenTK.Graphics.OpenGL;

namespace OpenTKMesh;

public class Shader
{

    public static Shader Default => FromPath("./src/Shader/Default/Default.vert", "./src/Shader/Default/Default.frag");


    readonly int Handle;


    private Shader(int handle)
    {
        Handle = handle;
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public static Shader FromPath(string vertexShaderPath, string fragmentShaderPath) {
        string vertexShaderSource = File.ReadAllText(vertexShaderPath);
        string fragmentShaderSource = File.ReadAllText(fragmentShaderPath);
        return FromSource(vertexShaderSource, fragmentShaderSource);
    }
    public static Shader FromSource(string vertexShaderSource, string fragmentShaderSource) {
        var VertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(VertexShader, vertexShaderSource);

        var FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(FragmentShader, fragmentShaderSource);

        GL.CompileShader(VertexShader);
        GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(VertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(FragmentShader);
        GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(FragmentShader);
            Console.WriteLine(infoLog);
        }

        int Handle = GL.CreateProgram();

        GL.AttachShader(Handle, VertexShader);
        GL.AttachShader(Handle, FragmentShader);

        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Console.WriteLine(infoLog);
        }
        GL.DetachShader(Handle, VertexShader);
        GL.DetachShader(Handle, FragmentShader);
        GL.DeleteShader(FragmentShader);
        GL.DeleteShader(VertexShader);

        return new Shader(Handle);
    }


    /// <summary>
    /// Dispose
    /// </summary>
    private bool disposedValue = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            GL.DeleteProgram(Handle);

            disposedValue = true;
        }
    }
    ~Shader()
    {
        if (disposedValue == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}