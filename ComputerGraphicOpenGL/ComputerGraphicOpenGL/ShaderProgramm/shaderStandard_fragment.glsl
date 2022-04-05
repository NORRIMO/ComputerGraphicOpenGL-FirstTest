#version 460

layout(location = 0) out vec4 color;

in vec2 vTextureCoordinates;

uniform sampler2D uTexture;

void main()
{
	//color = vec4(1, 0, 0, 1);
	color = texture(uTexture, vTextureCoordinates);

}