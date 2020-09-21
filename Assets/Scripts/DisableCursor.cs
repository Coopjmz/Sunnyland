using UnityEngine;

namespace Sunnyland
{
    sealed class DisableCursor : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

