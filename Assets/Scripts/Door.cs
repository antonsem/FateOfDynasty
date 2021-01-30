using System.Collections;
using UnityEngine;

namespace GGJ21
{
    public class Door : Item
    {
        [SerializeField] private float openRotation;
        [SerializeField] private float closeRotation;

        private IEnumerator _rotate;
        private bool isOpen = false;
        
        protected override void Use()
        {
            if(_rotate != null)
                StopCoroutine(_rotate);

            isOpen = !isOpen;
            _rotate = Rotate(isOpen);
            StartCoroutine(_rotate);
        }

        private IEnumerator Rotate(bool open)
        {
            float finalAngle = open ? openRotation : closeRotation;
            Quaternion finalRotation = Quaternion.Euler(0, finalAngle, 0);
            Quaternion initialRotation = transform.localRotation;

            float t = 0;
            while (t < 1)
            {
                transform.localRotation = Quaternion.Lerp(initialRotation, finalRotation, t);
                t += Time.deltaTime * 2;
                yield return null;
            }

            //transform.localRotation = finalRotation;
            _rotate = null;
        }
    }
}