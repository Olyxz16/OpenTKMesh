using OpenTK.Graphics.OpenGL;
using OpenTKMesh.Shading;
using OpenTK.Mathematics;
using System.Collections.Specialized;

namespace OpenTKMesh.Materials;

public class UnlitMaterial : Material
{

    private const string VERT_SHADER_PATH = "./src/Shader/Default/UnlitShader.vert";
    private const string FRAG_SHADER_PATH = "./src/Shader/Default/UnlitShader.frag";
    
    public static UnlitMaterial Default => new UnlitMaterial();

    public UnlitMaterial()
        : this(new Vector4(0.7f, 0.7f, 0.7f, 1.0f), null)
    { }
    public UnlitMaterial(Vector4 color, Texture texture = null)
        : base(color, texture)
    {
        _shader = Shader.FromPath(VERT_SHADER_PATH, FRAG_SHADER_PATH);
        GL.UseProgram(_shader.Handle);
        int location = GL.GetUniformLocation(_shader.Handle, "Albedo");
        GL.Uniform4(location, Albedo);
    }

}