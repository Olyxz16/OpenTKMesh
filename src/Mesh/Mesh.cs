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

    public Mesh(float[] vertices, uint[] triangles, Shader shader) 
    {
        if(!Validate(vertices, triangles))
        {
            throw new ArgumentException("Invalid arguments.");
        }
        _vertices = vertices;
        _triangles = triangles;
        _shader = shader;

        VBO = GL.GenBuffer();
        VAO = GL.GenVertexArray();
        EBO = GL.GenBuffer();

        Load();
    }

    private static bool Validate(float[] vertices, uint[] triangles)
    {
        return triangles.Max() < vertices.Length;
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
    public static Mesh Triangle(Vector3 v1, Vector3 v2, Vector3 v3, Shader shader = null) {
        shader ??= Shader.Default;
        var vertices = new float[9] 
        {
            v1.X, v1.Y, v1.Z,
            v2.X, v2.Y, v2.Z,
            v3.X, v3.Y, v3.Z
        };
        var triangles = new uint[3] 
        {
            0, 1, 2
        };
        return new Mesh(vertices, triangles, shader);
    }

    public static Mesh Quad(Vector3 center, float size, Shader shader = null) {
        shader ??= Shader.Default;
        float[] vertices = {
            0.5f*size  + center.X,  0.5f*size + center.Y, 0.0f,
            0.5f*size  + center.X, -0.5f*size + center.Y, 0.0f,
            -0.5f*size + center.X, -0.5f*size + center.Y, 0.0f,
            -0.5f*size + center.X,  0.5f*size + center.Y, 0.0f
        };
        uint[] triangles = {
            0, 1, 2,
            0, 2, 3
        };
        return new Mesh(vertices, triangles, shader);
    }

    public static Mesh Circle(Vector3 center, float radius, Shader shader = null) 
    {
        shader ??= Shader.Default;
        // A mettre dans une const ?
        uint verticesCount = 32;
        float[] vertices = new float[3 * (verticesCount + 1)];
        vertices[0] = center.X;
        for(int i = 1; i <= verticesCount; i++)
        {
            float angle = 2f*(float)Math.PI*(float)i/(float)verticesCount;
            vertices[3 * i + 0] = center.X + radius*(float)Math.Cos(angle);
            vertices[3 * i + 1] = center.Y + radius*(float)Math.Sin(angle);
            vertices[3 * i + 2] = center.Z;
        }
        uint[] triangles = new uint[3 * verticesCount];
        for(uint i = 0; i < verticesCount; i++)
        {
            triangles[3 * i + 0] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }
        triangles[3 * verticesCount - 3] = 0;
        triangles[3 * verticesCount - 2] = 1;
        triangles[3 * verticesCount - 1] = verticesCount ;
        return new Mesh(vertices, triangles, shader);

    }

    // A faire
    public static Mesh Combine(Mesh mesh1, Mesh mesh2) {
        return new Mesh();
    }



}
