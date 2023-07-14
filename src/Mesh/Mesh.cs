using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTKMesh.Shading;
using OpenTKMesh.Materials;

namespace OpenTKMesh;

public class Mesh
{

    public Vector3 Position { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 Scale { get; private set; }

    private float[] _vertices;
    private uint[] _triangles;
    private float[] _uvs;

    private Material _material;

    private int VBO;     // VertexBufferObject
    private int VAO;     // VertexArrayObject
    private int EBO;     // ElementBufferObject


    public Mesh(float[] vertices, uint[] triangles, float[] uvs, Material material)
    {
        if (!Validate(vertices, triangles))
        {
            throw new ArgumentException("Invalid arguments.");
        }
        
        Position = Vector3.Zero;
        Rotation = Vector3.Zero;
        Scale = Vector3.One;

        _vertices = vertices;
        _triangles = triangles;
        _uvs = uvs;

        _material = material;

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
        MeshHandler.Add(this);

        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
    
        _material.SetAttrib(VAO);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _triangles.Length * sizeof(uint), _triangles, BufferUsageHint.StaticDraw);

    }

    public void Draw() {
        ApplyTransform();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO); 
        GL.BindVertexArray(VAO);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        _material.Use();
        GL.DrawElements(PrimitiveType.Triangles, _triangles.Length, DrawElementsType.UnsignedInt, 0);
    }


    public void Move(Vector3 dir)
    {
        Position += dir;
        ApplyTransform();
    }
    public void MoveAt(Vector3 position)
    {
        Position = position;
        ApplyTransform();
    }
    public void Rotate(Vector3 rotation)
    {
        Rotation += rotation;
        ApplyTransform();
    }
    public void Resize(Vector3 scale)
    {
        Scale *= scale;
        ApplyTransform();
    }

    private void ApplyTransform()
    {
        Matrix4 position = Matrix4.CreateTranslation(Position);
        Matrix4 rotationX = Matrix4.CreateRotationX(Rotation.X);
        Matrix4 rotationY = Matrix4.CreateRotationY(Rotation.Y);
        Matrix4 rotationZ = Matrix4.CreateRotationZ(Rotation.Z);
        Matrix4 scale = Matrix4.CreateScale(Scale);
        Matrix4 transform = scale * rotationX * rotationY * rotationZ * position;
        GL.UseProgram(_material.Handle);
        int location = GL.GetUniformLocation(_material.Handle, "transform");
        GL.UniformMatrix4(location, true, ref transform);
    }

}
