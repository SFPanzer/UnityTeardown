using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

namespace Runtime.RenderPipeline
{
    public class GBuffer
    {
        public RenderTexture gDepth { get; private set; }
        public RenderTexture[] gBuffer { get; private set; }
        public RenderTargetIdentifier[] gBufferID { get; private set; }


        public GBuffer(float factor)
        {
            var gBufferSize = new Vector2Int((int)(Screen.width * factor), (int)(Screen.height * factor));
            
            // Initialize Render Textures.
            gDepth = new RenderTexture(gBufferSize.x, gBufferSize.y, 24, RenderTextureFormat.Depth);
            gBuffer = new RenderTexture[2];
            // Albedo.
            gBuffer[0] = new RenderTexture(gBufferSize.x, gBufferSize.y, 0, RenderTextureFormat.ARGB32);
            // Normal.
            gBuffer[1] = new RenderTexture(gBufferSize.x, gBufferSize.y, 0, RenderTextureFormat.ARGB32);

            // Setup Render Target Identifier.
            gBufferID = new RenderTargetIdentifier[gBuffer.Length];
            for (var i = 0; i < gBuffer.Length; i++)
            {
                gBufferID[i] = gBuffer[i];
                
            }
        }
    }
}