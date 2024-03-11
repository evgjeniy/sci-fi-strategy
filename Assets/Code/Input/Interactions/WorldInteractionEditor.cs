#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.InputSystem.Editor;

namespace SustainTheStrain.Input.Interactions
{
    public class WorldInteractionEditor : InputParameterEditor<WorldInteraction>
    {
        protected override void OnEnable() {}

        public override void OnGUI()
        {
            target.LayerMask = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(EditorGUILayout.MaskField
            (
                "Layer Mask",
                InternalEditorUtility.LayerMaskToConcatenatedLayersMask(target.LayerMask),
                InternalEditorUtility.layers
            ));
                
            target.RayCastMaxDistance = EditorGUILayout.FloatField
            (
                "Raycast Max Distance",
                target.RayCastMaxDistance
            );
        }
    }
}
#endif