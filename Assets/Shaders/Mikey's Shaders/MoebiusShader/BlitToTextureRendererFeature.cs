using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

/// <summary>
/// RenderFeature that blits the current content of the screen to a render texture
/// </summary>
internal class ColorBlitPass : ScriptableRenderPass
{
    ProfilingSampler m_ProfilingSampler = new ProfilingSampler("ColorBlit");
    RTHandle m_OutputHandle;

    public ColorBlitPass(RenderTexture renderTexture, RenderPassEvent renderEvent)
    {
        renderPassEvent = renderEvent;
        if (renderTexture != null)
            m_OutputHandle = RTHandles.Alloc(renderTexture, "ColorBlitOutput");
    }

    public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
    {
        if (m_OutputHandle == null)
            return;

        UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
        if (!resourceData.activeColorTexture.IsValid())
            return;

        TextureHandle destination = renderGraph.ImportTexture(m_OutputHandle);
        RenderGraphUtils.BlitMaterialParameters parameters = new(
            resourceData.activeColorTexture,
            destination,
            Blitter.GetBlitMaterial(TextureDimension.Tex2D),
            0);
        renderGraph.AddBlitPass(parameters, "ColorBlit");
    }

    public void Dispose()
    {
        m_OutputHandle?.Release();
    }
}

internal class BlitToTextureRendererFeature : ScriptableRendererFeature
{
    public float m_Intensity = 0;
    public RenderTexture m_renderTexture;
    public RenderPassEvent m_event;

    ColorBlitPass m_RenderPass = null;

    public override void AddRenderPasses(ScriptableRenderer renderer,
                                    ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType == CameraType.Game)
        {
            m_RenderPass.ConfigureInput(ScriptableRenderPassInput.Color);
            renderer.EnqueuePass(m_RenderPass);
        }
    }

    public override void Create()
    {
        m_RenderPass = new ColorBlitPass(m_renderTexture, m_event);
    }

    protected override void Dispose(bool disposing)
    {
        m_RenderPass?.Dispose();
    }
}
