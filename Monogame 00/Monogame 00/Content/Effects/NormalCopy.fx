#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float xSize;
float ySize;
float xDraw;
float yDraw;

float4 filterColor;


texture Texture;
sampler TextureSampler = sampler_state
{
	Texture = <Texture>;
};

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCordinate : TEXCOORD0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	output.Color = input.Color;

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	return input.Color;
}

// Our pixel shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR
{
	float4 texColor = tex2D(TextureSampler, input.TextureCordinate);

	float vertPixSize = 1.0f / ySize;
	float horPixSize = 1.0f / xSize;

	float4 color;
	if (xDraw / xSize < .6f || yDraw / ySize < .6f)
	{
		if (xDraw / xSize < .4f || yDraw / ySize < .4f)
		{
			vertPixSize = 2.0f / ySize;
			horPixSize = 2.0f / xSize;
		}

		float4 aboveColor = tex2D(TextureSampler, (input.TextureCordinate) + float2(0, -vertPixSize));

		float4 belowColor = tex2D(TextureSampler, (input.TextureCordinate) + float2(0, vertPixSize));

		float4 leftColor = tex2D(TextureSampler, (input.TextureCordinate) + float2(-horPixSize, 0));

		float4 rightColor = tex2D(TextureSampler, (input.TextureCordinate) + float2(horPixSize, 0));

		//float greyscaleAverage = (texColor.r + texColor.g + texColor.b)/3;

		 color = float4((texColor.r + aboveColor.r + belowColor.r + leftColor.r + rightColor.r) / 5,
		 (texColor.g + aboveColor.g + belowColor.g + leftColor.g + rightColor.g) / 5,
		 (texColor.b + aboveColor.b + belowColor.b + leftColor.b + rightColor.b) / 5,
		 (texColor.a + aboveColor.a + belowColor.a + leftColor.a + rightColor.a) / 5);
	}
	else
	{
		color = float4(texColor.r, texColor.g, texColor.b, texColor.a);
	}



	return color * filterColor;
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
};