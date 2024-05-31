using UnityEngine.Rendering;

namespace Editor
{
    public class VoxelRenderPipelineAssets : RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new VoxelRenderPipeline();
        }
    }
}
