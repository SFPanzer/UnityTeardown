using System.Collections;
using System.Collections.Generic;
using Runtime.RenderPipeline;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

public class VoxelRenderPipeline : RenderPipeline
{
    private GBuffer _gBuffer;
    public VoxelRenderPipeline()
    {
        _gBuffer = new GBuffer(1);
    }

    private void GBufferPass(ScriptableRenderContext context, Camera camera)
    {
        Profiler.BeginSample("G-Buffer Pass");

        var cmd = new CommandBuffer();
        cmd.name = "G-Buffer pass";

        // Setup and clear render target.
        cmd.SetRenderTarget(_gBuffer.gBufferID, _gBuffer.gDepth);
        cmd.ClearRenderTarget(true, true, Color.magenta);
        context.ExecuteCommandBuffer(cmd);
        cmd.Clear();

        // Culling.
        camera.TryGetCullingParameters(out var cullingParameters);
        var cullingResults = context.Cull(ref cullingParameters);

        // Config settings.
        var shaderTagId = new ShaderTagId("GBuffer");
        var sortingSettings = new SortingSettings(camera);
        var drawingSettings = new DrawingSettings(shaderTagId, sortingSettings);
        var filteringSettings = FilteringSettings.defaultValue;

        // Draw
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
        context.Submit();

        Profiler.EndSample();
    }
    
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (var camera in cameras)
        {
            context.SetupCameraProperties(camera);

            // Passes.
            GBufferPass(context, camera); 

            // Skybox and Gizmos.
            if (camera.clearFlags == CameraClearFlags.Skybox && RenderSettings.skybox != null)
            {
                context.DrawSkybox(camera);
            }

            if (Handles.ShouldRenderGizmos())
            {
                context.DrawGizmos(camera, GizmoSubset.PreImageEffects);
                context.DrawGizmos(camera, GizmoSubset.PostImageEffects);
            }

            context.Submit();
        }
    }
}
