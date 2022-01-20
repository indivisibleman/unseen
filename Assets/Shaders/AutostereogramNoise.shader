Shader "Hidden/Custom/AutostereogramNoise"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    int _UseNoise;
    int _Strips;
    int _PixelsPerStrip;
    float _DepthFactor;
    float _TimeFactor;
    float _Hue;
    float _Saturation;
    float4 _MainTex_TexelSize;
    TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);

    float4 Frag(VaryingsDefault i) : SV_Target
    {
        float2 texcoord = i.texcoord;
        float pixelsWide = _Strips * _PixelsPerStrip;
        float pixelsHigh = pixelsWide * _MainTex_TexelSize.w / _MainTex_TexelSize.z;
        float depthStrips = _Strips - 1.0;
        float stripWidth = 1.0 / _Strips;
        float hue = _Hue;
        float saturation = _Saturation;

        texcoord.x = floor(texcoord.x * pixelsWide) / pixelsWide;
        texcoord.y = floor(texcoord.y * pixelsHigh) / pixelsHigh;

        [unroll(25)]
        while(texcoord.x > stripWidth)
        {
            float2 depthcoord = texcoord;

            depthcoord.x = (depthcoord.x - stripWidth) * (_Strips / depthStrips);

            float depth = 1.0 - Linear01Depth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, depthcoord).r);

            depth = stripWidth * (floor(depth * _DepthFactor * _PixelsPerStrip) / _PixelsPerStrip);

            texcoord.x -= (stripWidth - depth);
        }

        if(texcoord.x < 0) 
        {
            texcoord.x += stripWidth;
        }

        float value = texcoord.x;

        if(_UseNoise == 1)
        {
            texcoord.y += floor(_Time.y * _TimeFactor);

            value = (sin(dot(texcoord.xy, float2(12.9898, 78.233))) * 43758.5453)
                     - floor(sin(dot(texcoord.xy, float2(12.9898, 78.233))) * 43758.5453);
        }

        float4 RGBA = value;

        float var_h = hue * 6;
        float var_i = floor(var_h);
        float var_1 = value * (1.0 - saturation);
        float var_2 = value * (1.0 - saturation * (var_h-var_i));
        float var_3 = value * (1.0 - saturation * (1 - (var_h - var_i)));
        
        if (var_i == 0)
        {
            RGBA = float4(value, var_3, var_1, 1);
        }
        else if (var_i == 1)
        {
            RGBA = float4(var_2, value, var_1, 1);
        }
        else if (var_i == 2)
        {
            RGBA = float4(var_1, value, var_3, 1);
        }
        else if (var_i == 3)
        {
            RGBA = float4(var_1, var_2, value, 1);
        }
        else if (var_i == 4)
        {
            RGBA = float4(var_3, var_1, value, 1);
        }
        else
        {
            RGBA = float4(value, var_1, var_2, 1);
        }
       
        return (RGBA);
    }
    
    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

            #pragma vertex VertDefault
            #pragma fragment Frag

            ENDHLSL
        }
    }
}