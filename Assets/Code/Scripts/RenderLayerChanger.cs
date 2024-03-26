using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain
{
    public class RenderLayerChanger : MonoBehaviour
    {
        [SerializeField] private LightLayerEnum _renderLayer;

        [Button("Change Render Layer")]
        private void ChangeRenderLayer()
        {
            if(TryGetComponent<Renderer>(out var localRenderer))
            {
                localRenderer.renderingLayerMask = (uint)_renderLayer;
            }

            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.renderingLayerMask = (uint)_renderLayer;
            }
        }
    }
}
