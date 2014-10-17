sampler2D Input : register(s0);
float frequency : register(C0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 Color;
	
	uv.y = uv.y + (sin((uv.x + frequency)*17.54389695)*0.004210526);
    uv.x = uv.x + (cos((uv.y + frequency)*17.54389695)*0.004210526);

	Color = tex2D(Input, uv.xy);
    return Color;

}