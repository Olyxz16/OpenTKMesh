#version 330 core
layout (location = 0) in vec3 Position;

uniform mat4 transform;
out vec4 Color;

void main()
{
    gl_Position = vec4(Position, 1.0) * transform;
    Color = vec4(clamp(Position, 0.0, 1.0), 1.0);
}