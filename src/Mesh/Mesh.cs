using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenTKMesh;

public class Mesh
{
    
    private float[] _vertices;
    private uint[] _triangles;

    private Shader _shader;

    private int VBO;     // VertexBufferObject
    private int VAO;     // VertexArrayObject
    private int EBO;     // ElementBufferObject

    
    public Mesh()
    : this(new float[10], new uint[10], Shader.Default) {}

    public Mesh(float[] vertices, uint[] triangles)
    : this(vertices, triangles, Shader.Default) {}

    public Mesh(float[] vertices, uint[] triangles, Shader shader) {
        _vertices = vertices;
        _triangles = triangles;
        _shader = shader;

        VBO = GL.GenBuffer();
        VAO = GL.GenVertexArray();
        EBO = GL.GenBuffer();

        Load();
    }


    public void Load() {
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
    
        GL.BindVertexArray(VAO);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _triangles.Length * sizeof(uint), _triangles, BufferUsageHint.StaticDraw);
    }

    public void Draw() {
        _shader.Use();
        GL.DrawElements(PrimitiveType.Triangles, _triangles.Length, DrawElementsType.UnsignedInt, 0);
    }



    public static Mesh Triangle(Vector3 v1, Vector3 v2, Vector3 v3) {
        var vertices = new float[9] {
            v1.X, v1.Y, v1.Z,
            v2.X, v2.Y, v2.Z,
            v3.X, v3.Y, v3.Z
        };
        var triangles = new uint[3] {
            0, 1, 2
        };
        return new Mesh(vertices, triangles);
    }

    public static Mesh Quad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
        return new Mesh();
    }


    // A faire
    public static Mesh Combine(Mesh mesh1, Mesh mesh2) {
        return new Mesh();
    }



}
