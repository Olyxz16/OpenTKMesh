using OpenTK.Graphics.OpenGL;
using OpenTKMesh.Shading;
using OpenTK.Mathematics;
using System.Collections.Specialized;

namespace OpenTKMesh.Materials;

public class FlatColorMaterial : Material
{

    private const string VERT_SHADER_PATH = "./src/Shader/Default/FlatColorShader.vert";
    private const string FRAG_SHADER_PATH = "./src/Shader/Default/FlatColorShader.frag";
    
    public static FlatColorMaterial Default => new FlatColorMaterial();


    public FlatColorMaterial()
    : this(new Vector4(0.7f, 0.7f, 0.7f, 1.0f)) {}
    public FlatColorMaterial(Vector3 color)
    : this(new Vector4(color.X, color.Y, color.Z, 1.0f)) {}
    public FlatColorMaterial(float r, float g, float b, float a)
    : this(new Vector4(r, g, b, a)) {}
    public FlatColorMaterial(float r, float g, float b)
    : this(new Vector4(r, g, b, 1.0f)) {}

    public FlatColorMaterial(Vector4 color)
    {
        Albedo = color;
        _shader = Shader.FromPath(VERT_SHADER_PATH, FRAG_SHADER_PATH);
        GL.UseProgram(_shader.Handle);
        int location = GL.GetUniformLocation(_shader.Handle, "Albedo");
        GL.Uniform4(location, ref color);
    }




}