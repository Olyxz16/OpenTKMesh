#version 330 core
out vec4 FragColor;

uniform vec4 Albedo;

void main()
{
    FragColor = Albedo;
}