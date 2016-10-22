//------------------------------------------------------
//--                                                  --
//--           www.riemers.net                    --
//--               Basic shaders                     --
//--        Use/modify as you like                --
//--                                                  --
//------------------------------------------------------

struct VertexToPixel
{
    float4 Position       : POSITION;    
    float4 Color        : COLOR0;
    float LightingFactor: TEXCOORD0;
    float2 TextureCoords: TEXCOORD1;
};

struct PixelToFrame
{
    float4 Color : COLOR0;
};

//------- Constants --------
float4x4 xView;
float4x4 xProjection;
float4x4 xWorld;
float3 xLightDirection;
float xAmbient;
bool xEnableLighting;

//------- XNA-to-HLSL variabler --------
uniform extern float3 xDiffuseLight;
uniform extern float3 xDiffuseMaterial;
uniform extern float3 xAmbientLight;
uniform extern float3 xAmbientMaterial;

//------- Shader-parametre --------
float4x4 xInvTransWorld;

struct OutputVS
{
	float4 posH : POSITION0;
	float3 normalW : TEXCOORD0;
	float3 posW : TEXCOORD1;
	float4 color : COLOR0;
};
struct OutputPS
{
	float4 color : COLOR0;
};

//------- Technique: Phong --------
OutputVS PhongVS( float4 inPos : POSITION, float4 inColor: COLOR, float3
inNormal: NORMAL)
{
	OutputVS outVS = (OutputVS)0;
	float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection);
	outVS.posH= mul(inPos, preWorldViewProjection);
	outVS.posW = mul(inPos, xWorld);
	//Normaliserer normalvektoren:
	float3 normal = normalize(inNormal);
	//Fjerner translasjon fra world-matrisa:
	float3x3 rotMatrix = (float3x3)xWorld;
	//Transformerer normalvektoren:
	float3 rotNormal = mul(normal, rotMatrix);
	outVS.normalW = normalize(rotNormal);
	return outVS;
}

OutputPS PhongPS(in OutputVS inPS) : COLOR0
{
	OutputPS outPS = (OutputPS)0;
	//Må normalisere på nytt:
	float3 normal = normalize(inPS.normalW);
	//GJØR ALL LYSBEREGNING HER:
	float s = max(dot(normal, -xLightDirection), 0.0f);
	float3 diffuse = s * (xDiffuseMaterial * xDiffuseLight).rgb;
	float3 ambient = xAmbientMaterial * xAmbientLight;
	//ambient = xAmbient;
	outPS.color.rgb = (ambient + diffuse);
	return outPS;
}

technique Phong
{
	pass P0
	{
		VertexShader = compile vs_2_0 PhongVS();
		PixelShader = compile ps_2_0 PhongPS();
	}
}

//------- Technique: Belysning --------
VertexToPixel LysVS( float4 inPos : POSITION, float4 inColor: COLOR, float3
inNormal: NORMAL)
{
	VertexToPixel Output = (VertexToPixel)0;
	float4x4 preViewProjection = mul(xView, xProjection);
	float4x4 preWorldViewProjection = mul(xWorld, preViewProjection);
	Output.Position = mul(inPos, preWorldViewProjection);
	Output.Color = inColor;
	//Trenger ikke normalisere her dersom gjort i C#-koden:
	float3 normal = normalize(inNormal);
	//Bruker invers-transpose:
	float3 rotNormal = mul(normal, xInvTransWorld);
	//Normaliserer på nytt:
	rotNormal = normalize(rotNormal);
	Output.LightingFactor = 1;
	if (xEnableLighting) {
	Output.LightingFactor = max(dot(rotNormal, -xLightDirection),
	0);
	}
	return Output;
}


//------- Technique: Belysning --------

PixelToFrame LysPS(VertexToPixel PSIn)
{
	PixelToFrame Output = (PixelToFrame)0;
	Output.Color = PSIn.Color;
	return Output;
}

technique Belysning
{
	pass P0
	{
	VertexShader = compile vs_2_0 LysVS();
	PixelShader = compile ps_2_0 LysPS();
	}
}