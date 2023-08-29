using System;
using OpenTK.Mathematics;
using OpenTKMesh.Shading;
using OpenTKMesh.Materials;

namespace OpenTKMesh;

public static class Primitives
{

	private const uint CIRCLE_VERTEX_COUNT = 32;

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
		var uvs = new float[6]
		{
			v1.X, v1.Y,
			v2.X, v2.Y,
			v3.X, v3.Y,
		};
		return new Mesh(vertices, triangles, uvs, material);
	}

	public static Mesh InstantiateQuad(Vector3 center, float size, Material material = null)
	{
		material ??= Material.Default;
		float[] vertices = {
            0.5f,  0.5f, 0.0f,  // top right
			 0.5f, -0.5f, 0.0f,  // bottom right
			-0.5f, -0.5f, 0.0f, // bottom left
			-0.5f,  0.5f, 0.0f,
        };
		uint[] triangles = {
			0, 1, 3,
			1, 2, 3
		};
		float[] uvs =
		{
			1f, 1f,
			1f, 0f,
            0f, 0f,
            0f, 1f,
        };
		var mesh = new Mesh(vertices, triangles, uvs, material);
		mesh.MoveAt(center);
		mesh.Resize(new Vector3(size, size, size));
		return mesh;
	}

	public static Mesh InstanteCircle(Vector3 center, float radius, Material material = null)
	{
		material ??= Material.Default;
		float[] vertices = new float[3 * (CIRCLE_VERTEX_COUNT + 1)];
		vertices[0] = center.X;
		for (int i = 1; i <= CIRCLE_VERTEX_COUNT; i++)
		{
			float angle = 2f * (float)Math.PI * (float)i / (float)CIRCLE_VERTEX_COUNT;
			vertices[3 * i + 0] = (float)Math.Cos(angle);
			vertices[3 * i + 1] = (float)Math.Sin(angle);
			vertices[3 * i + 2] = center.Z;
		}
		uint[] triangles = new uint[3 * CIRCLE_VERTEX_COUNT];
		for (uint i = 0; i < CIRCLE_VERTEX_COUNT; i++)
		{
			triangles[3 * i + 0] = 0;
			triangles[3 * i + 1] = i + 1;
			triangles[3 * i + 2] = i + 2;
		}
		float[] uvs = new float[2 * CIRCLE_VERTEX_COUNT];
        for (int i = 0; i < CIRCLE_VERTEX_COUNT; i++)
        {
            float angle = 2f * (float)Math.PI * (float)i / (float)CIRCLE_VERTEX_COUNT;
            uvs[2 * i + 0] = (float)Math.Cos(angle);
            uvs[2 * i + 1] = (float)Math.Sin(angle);
        }
        triangles[3 * CIRCLE_VERTEX_COUNT - 3] = 0;
		triangles[3 * CIRCLE_VERTEX_COUNT - 2] = 1;
		triangles[3 * CIRCLE_VERTEX_COUNT - 1] = CIRCLE_VERTEX_COUNT;
		var mesh = new Mesh(vertices, triangles, uvs, material);
		mesh.MoveAt(center);
		mesh.Resize(new Vector3(radius, radius, radius));
		return mesh;
	}

}