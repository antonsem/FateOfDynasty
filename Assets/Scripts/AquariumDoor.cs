﻿using UnityEngine;

namespace GGJ21
{
    public class AquariumDoor : Door
    {
        [SerializeField] private ItemData lockedData;
        [SerializeField] private ItemData unlockedData;
        
        private bool _isLocked = true;

        private void OnEnable()
        {
            _isLocked = true;
            data = lockedData;
            Events.Instance.switchPressed += Unlock;
        }

        private void OnDisable()
        {
            if(GameState.IsQuitting) return;
            Events.Instance.switchPressed -= Unlock;
        }

        private void Unlock()
        {
            _isLocked = false;
            data = unlockedData;
        }

        protected override void Use()
        {
            if (_isLocked)
            {
                CantUse();
                return;
            }
            base.Use();
        }
    }
}