#version 460

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 textureCoordinates;

uniform mat4 uniformMatrix;

out vec2 vTextureCoordinates;

void main()
{
	vTextureCoordinates = textureCoordinates;
	vec4 positionNew = uniformMatrix * vec4(position, 1.0);
	gl_Position = positionNew;
}