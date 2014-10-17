sampler2D Input : register(S0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
   float4 color = tex2D( Input, uv );
   float4 inverted_color = 1 - color;
   inverted_color.a = color.a;
   inverted_color.rgb *= inverted_color.a;
   return inverted_color;
}


