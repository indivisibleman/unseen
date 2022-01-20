using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(DepthRenderer), PostProcessEvent.BeforeStack, "Custom/Depth")]
public sealed class Depth : PostProcessEffectSettings
{
    public sealed class DepthRenderer : PostProcessEffectRenderer<Depth>
    {
        public override void Render(PostProcessRenderContext context)
        {
            context.command.BlitFullscreenTriangle(
                context.source, 
                context.destination, 
                context.propertySheets.Get(Shader.Find("Hidden/Custom/Depth")), 
                0);
        }

        public override DepthTextureMode GetCameraFlags()
        {
            return DepthTextureMode.Depth;
        }
    }
}
