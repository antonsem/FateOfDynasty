using UnityEngine;

namespace GGJ21
{
    public class MultiStateItem : Item
    {
        [SerializeField] private ItemData[] states;
        [SerializeField] private GameObject[] stateVisuals;
        
        private int _state = 0;
        
        #region Unity Methods

        private void Awake()
        {
            _state = 0;
            data = states[0];
            for (int i = 0; i < stateVisuals.Length; i++)
                stateVisuals[i].SetActive(i == _state);
        }

        #endregion

        protected override void Use()
        {
            base.Use();
            if (states.Length > _state + 1)
                _state++;

            data = states[_state];
            for (int i = 0; i < stateVisuals.Length; i++)
                stateVisuals[i].SetActive(i == _state);
        }
    }
}