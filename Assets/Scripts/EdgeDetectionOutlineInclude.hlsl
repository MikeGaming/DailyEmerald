#ifndef SOBEL_INCLUDED
#define SOBEL_INCLUDED

// Simple 3x3 convolution kernels

// Applies a simple blur
static float simpleBlur[9] =
{
	1,1,1,
	1,1,1,
	1,1,1
};

// Applies a gaussian blur
static float gaussianBlur[9] =
{
	1,2,1,
	2,4,2,
	1,2,1
};

// Sobel filter for edge detection verticaly
static float sobelX[9] =
{
	1,0,-1,
	2,0,-2,
	1,0,-1
};

// Sobel filter for edge detection horizontaly
static float sobelY[9] =
{
	1,2,1,
	0,0,0,
	-1,-2,-1
};

// Sample points for the sobel filter
static float2 sobelSamplePoints[9] = 
{
	float2(-1, 1),
	float2(0, 1),
	float2(1, 1),

	float2(-1, 0),
	float2(0, 0),
	float2(1, 1),

	float2(-1, -1),
	float2(0, -1),
	float2(1, -1)
};

void TextureSobel_float(float2 UV, float Thickness, UnityTexture2D Tex, UnitySamplerState SS, out float Out)
{ 
    float2 sobel = 0;
	[unroll] for (int i = 0; i < 9; i++)
	{
        float depth = SAMPLE_TEXTURE2D(Tex, SS, UV + sobelSamplePoints[i] * Thickness).r;
        sobel += depth * float2(sobelX[i], sobelY[i]);
    }
	
    Out = length(sobel);
}

void DepthSobel_float(float2 UV, float Thickness, out float Out)
{
    float2 sobel = 0;
	[unroll]
    for (int i = 0; i < 9; i++)
    {
        float depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV + sobelSamplePoints[i] * Thickness).r;
        sobel += depth * float2(sobelX[i], sobelY[i]);
    }
	
    Out = length(sobel);
}

void NormalSobel_float(float2 UV, float Thickness, out float Out)
{
    float2 sobel = 0;
	[unroll]
    for (int i = 0; i < 9; i++)
    {
        float normal = mul(SHADERGRAPH_SAMPLE_SCENE_NORMAL(UV + sobelSamplePoints[i] * Thickness), (float3x3) UNITY_MATRIX_I_V);
        sobel += normal * float2(sobelX[i], sobelY[i]);
    }
	
    Out = length(sobel);
}

void NormalTextureSample_float(float2 UV, out float3 Out)
{
    Out = mul(SHADERGRAPH_SAMPLE_SCENE_NORMAL(UV), (float3x3) UNITY_MATRIX_I_V);
}

void ColorSobel_float(float2 UV, float Thickness, out float Out)
{
    float2 sobelR = 0;
    float2 sobelG = 0;
    float2 sobelB = 0;
	
	[unroll]
    for (int i = 0; i < 9; i++)
    {
        float3 color = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV + sobelSamplePoints[i] * Thickness);
        float2 kernel = float2(sobelX[i], sobelY[i]);
        sobelR += color.r * kernel;
        sobelG += color.g * kernel;
        sobelB += color.b * kernel;
    }
	
    Out = max(length(sobelR), max(length(sobelG), length(sobelB)));
}

#endif