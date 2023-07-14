using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using OpenTKMesh.Shading;

namespace OpenTKMesh.Materials;


public abstract class Material
{

    public static UnlitMaterial Default => new UnlitMaterial();

    public Vector4 Albedo { get; protected set; }
    public Texture Texture { get; protected set; }

    public int Handle => _shader.Handle;
    protected Shader _shader;


    public Material(Vector4 albedo, Texture texture)
    {
        Albedo = albedo;
        Texture = texture;
    }


    public void Use()
    {
        Texture?.Use(TextureUnit.Texture0);
        _shader.Use();
    }

    public void SetAttrib(int VAO)
    {
        GL.BindVertexArray(VAO);
        //var vertexLocation = _shader.GetAttribLocation("aPosition");
        var vertexLocation = 0;
        GL.EnableVertexAttribArray(vertexLocation);
        GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        //var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
        var texCoordLocation = 1;
        GL.EnableVertexAttribArray(texCoordLocation);
        GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
    }

}