
using OpenTKMesh.Shading;

namespace Framework.Shading;

public static class UnlitShaderDefault
{

    public static Shader Shader => Shader.FromSource(VERT_SHADER, FRAG_SHADER);
    
    public static readonly string VERT_SHADER = @"
        #version 330 core

        layout (location = 0) in vec3 aPosition;
        layout (location = 1) in vec2 aTexCoord;

        out vec2 texCoord;

        uniform mat4 transform;

        void main()
        {
            texCoord = aTexCoord;

            gl_Position = vec4(aPosition, 1.0) * transform;
        }
    ";
    
    public static readonly string FRAG_SHADER = @"
        #version 330 core

        out vec4 FragColor;

        in vec2 texCoord;

        uniform vec4 Albedo;
        uniform sampler2D texture0;

        void main()
        {
            FragColor = texture(texture0, texCoord);
        }
    ";



}
