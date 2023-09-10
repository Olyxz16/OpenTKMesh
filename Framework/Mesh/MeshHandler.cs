using System.Collections.Generic;

namespace OpenTKMesh;

public static class MeshHandler
{

    private static List<Mesh> _meshes = new List<Mesh>();
    public static IReadOnlyList<Mesh> Meshes => _meshes;

    public static void Add(Mesh mesh)
    {
        _meshes.Add(mesh);
    }

    public static void Draw()
    {
        foreach(Mesh mesh in _meshes)
        {
            mesh.Draw();
        }
    }


}