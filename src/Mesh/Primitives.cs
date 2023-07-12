using System;
using OpenTK.Mathematics;
using OpenTKMesh.Shading;
using OpenTKMesh.Materials;

namespace OpenTKMesh;

public static class Primitives
{

	public static Mesh InstantiateTriangle(Vector3 v1, Vector3 v2, Vector3 v3, Material material = null)
	{
		material ??= Material.Default;
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
		return new Mesh(vertices, triangles, material);
	}

	public static Mesh InstantiateQuad(Vector3 center, float size, Material material = null)
	{
		material ??= Material.Default;
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
		return new Mesh(vertices, triangles, material);
	}

	public static Mesh InstanteCircle(Vector3 center, float radius, Material material = null)
	{
		material ??= Material.Default;
		// A mettre dans une const ?
		uint verticesCount = 32;
		float[] vertices = new float[3 * (verticesCount + 1)];
		vertices[0] = center.X;
		for (int i = 1; i <= verticesCount; i++)
		{
			float angle = 2f * (float)Math.PI * (float)i / (float)verticesCount;
			vertices[3 * i + 0] = center.X + radius * (float)Math.Cos(angle);
			vertices[3 * i + 1] = center.Y + radius * (float)Math.Sin(angle);
			vertices[3 * i + 2] = center.Z;
		}
		uint[] triangles = new uint[3 * verticesCount];
		for (uint i = 0; i < verticesCount; i++)
		{
			triangles[3 * i + 0] = 0;
			triangles[3 * i + 1] = i + 1;
			triangles[3 * i + 2] = i + 2;
		}
		triangles[3 * verticesCount - 3] = 0;
		triangles[3 * verticesCount - 2] = 1;
		triangles[3 * verticesCount - 1] = verticesCount;
		return new Mesh(vertices, triangles, material);

	}

}