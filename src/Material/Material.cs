using OpenTK.Mathematics;
using System.Runtime.InteropServices;
using OpenTKMesh.Shading;

namespace OpenTKMesh.Materials;


public abstract class Material
{

    public static FlatColorMaterial Default => new FlatColorMaterial();

    public Vector4 Albedo { get; protected set; }

    public int Handle => _shader.Handle;
    protected Shader _shader;



    public void Use()
    {
        _shader.Use();
    }

}