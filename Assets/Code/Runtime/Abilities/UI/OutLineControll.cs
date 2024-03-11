using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class OutLineControll : MonoBehaviour
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
            if (needMark)
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
