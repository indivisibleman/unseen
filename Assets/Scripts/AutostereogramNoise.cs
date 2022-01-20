using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(AutostereogramNoiseRenderer), PostProcessEvent.BeforeStack, "Custom/AutostereogramNoise")]
public sealed class AutostereogramNoise : PostProcessEffectSettings
{
    [Range(0, 1), Tooltip("Use noise or display depth")]
    public IntParameter useNoise = new IntParameter
    {
        value = 1
    };

    [Range(0, 24), Tooltip("Number of strips")]
    public IntParameter strips = new IntParameter
    {
        value = 6
    };

    [Range(10, 240), Tooltip("Pixels per strip")]
    public IntParameter pixelsPerStrip = new IntParameter
    {
        value = 20
    };

    [Range(-2, 2), Tooltip("Depth factor")]
    public FloatParameter depthFactor = new FloatParameter
    {
        value = 4f
    };

    [Range(0, 30), Tooltip("Time factor")]
    public FloatParameter timeFactor = new FloatParameter
    {
        value = 0f
    };

    [Range(0, 1), Tooltip("Hue")]
    public FloatParameter hue = new FloatParameter
    {
        value = 0f
    };

    [Range(0, 1), Tooltip("Saturation")]
    public FloatParameter saturation = new FloatParameter
    {
        value = 0f
    };
}

public sealed class AutostereogramNoiseRenderer : PostProcessEffectRenderer<AutostereogramNoise>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/AutostereogramNoise"));
        sheet.properties.SetInt("_UseNoise", settings.useNoise);
        sheet.properties.SetInt("_Strips", settings.strips);
        sheet.properties.SetInt("_PixelsPerStrip", settings.pixelsPerStrip);
        sheet.properties.SetFloat("_DepthFactor", settings.depthFactor);
        sheet.properties.SetFloat("_TimeFactor", settings.timeFactor);
        sheet.properties.SetFloat("_Hue", settings.hue);
        sheet.properties.SetFloat("_Saturation", settings.saturation);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }

    public override DepthTextureMode GetCameraFlags()
    {
        return DepthTextureMode.Depth;
    }
}

