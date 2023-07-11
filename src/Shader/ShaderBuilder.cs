using System;
using System.Text;
using OpenTK.Mathematics;

namespace OpenTKMesh.Shading;

public class ShaderBuilder
{

	private const string HEADER = "#version 330 core";

	private const string FRAG_COLOR_PROPERTY = "out vec4 FragColor;";
	private const string FRAG_COLOR_DECLARATION = "FragColor = vec4(X, Y, Z, 1.0f);";

	private const string VERT_POSITION_PROPERTY = "layout (location = 0) in vec3 aPosition;";
	private const string VERT_TRANSFORM_PROPERTY = "uniform mat4 transform;";
    private const string VERT_POSITION_DECLARATION = "gl_Position = vec4(aPosition, 1.0) * transform;";


	private float red;
	private float green;
	private float blue;


	public ShaderBuilder()
	{
		this.red = 0f;
		this.green = 0f;
		this.blue = 0f;
	}

	public ShaderBuilder SetColor(float red, float green, float blue)
	{
		this.red = red;
		this.green = green;
		this.blue = blue;
		return this;
	}


	
	private string BuildVert()
	{
		StringBuilder source = new StringBuilder();
		source.AppendLine(HEADER);
		source.AppendLine(VERT_POSITION_PROPERTY);
		source.AppendLine(VERT_TRANSFORM_PROPERTY);
		source.AppendLine("void main() {");
		source.AppendLine(VERT_POSITION_DECLARATION);
		source.AppendLine("}");
		return source.ToString();

	}
	private string BuildFrag()
	{
		StringBuilder source = new StringBuilder();
		source.AppendLine(HEADER);
		source.AppendLine(FRAG_COLOR_PROPERTY);
		source.AppendLine("void main() {");
		source.AppendLine(FRAG_COLOR_DECLARATION.Replace("X", red.ToString()).Replace("Y", green.ToString()).Replace("Z", blue.ToString()));
		source.AppendLine("}");
		return source.ToString();
	}

	public Shader Build()
	{
		var vertSource = BuildVert();
		var fragSource = BuildFrag();
		return Shader.FromSource(vertSource, fragSource);
	}


	public static Shader FromColor(float red, float green, float blue)
	{
		return new ShaderBuilder()
			.SetColor(red, green, blue)
			.Build();
	}



}