using UnityEngine;

namespace SustainTheStrain
{
    public class TempController : MonoBehaviour
    {
        private AimSpawner spawner;
        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            spawner = GetComponent<AimSpawner>();
        }

        private void Update()
        {
            //just for example
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                    spawner.SpawnAimRound(hit.point);
            }
        }
    }
}
