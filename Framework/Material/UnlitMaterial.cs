using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Framework.Shading;


namespace OpenTKMesh.Materials;

public class UnlitMaterial : Material
{

    public static UnlitMaterial Default => new UnlitMaterial();

    private UnlitMaterial()
        : this(new Vector4(0.7f, 0.7f, 0.7f, 1.0f), null)
    { }
    public UnlitMaterial(Vector4 color, Texture texture = null)
        : base(color, texture)
    {
        _shader = UnlitShaderDefault.Shader;
        GL.UseProgram(_shader.Handle);
        int location = GL.GetUniformLocation(_shader.Handle, "Albedo");
        GL.Uniform4(location, Albedo);
    }

}