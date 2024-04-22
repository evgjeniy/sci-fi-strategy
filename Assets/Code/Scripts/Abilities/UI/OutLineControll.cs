using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class OutLineControll : MonoBehaviour // TODO : REMOVE THIS USELESS SCRIPT
    {
        private Outline outline;
        private bool needMark = false;

        private void Start()
        {
            outline = GetComponent<Outline>();
            outline.enabled = false;
        }

        public void setMark() => needMark = true;

        private void LateUpdate()
        {
            if (needMark /*|| AbilitiesController.freezeSelected*/)
            {
                if (!outline.enabled)
                    outline.enabled = true;
                needMark = false;
            }
            else
            {
                if (outline.enabled)
                    outline.enabled = false;
            }
        }
    }
}
