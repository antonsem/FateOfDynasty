using UnityEngine;

namespace GGJ21
{
    public class Clicker : MonoBehaviour
    {
        private enum ClickButton
        {
            Left,
            Right
        }

        [SerializeField] private float rayDistance = 3;
        [SerializeField] private LayerMask mask;
        private Ray _ray;
        private RaycastHit _hit;

        private Transform _cam;

        #region Unity Methods

        private void Awake()
        {
            _cam = Camera.main.transform;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                Click(ClickButton.Left);
            else if (Input.GetMouseButtonDown(1))
                Click(ClickButton.Right);
        }

        #endregion

        private void Click(ClickButton btn)
        {
            _ray = new Ray(_cam.position, _cam.forward);
            if (!Physics.Raycast(_ray, out _hit, rayDistance, mask)) return;

            if (!_hit.transform.TryGetComponent(out IClickable clickable)) return;

            switch (btn)
            {
                case ClickButton.Left:
                    clickable.Clicked();
                    break;
                case ClickButton.Right:
                    Events.Instance.displayMessage?.Invoke(clickable.Description);
                    break;
            }
        }
    }
}