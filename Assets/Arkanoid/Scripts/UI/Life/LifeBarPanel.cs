﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid.UI.LifeBar
{
    public class LifeBarPanel : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _liveGOs;

        private ILivesNotificator _livesNotificator;

        public void Contruct(ILivesNotificator livesNotificator)
        {
            _livesNotificator = livesNotificator;

            _livesNotificator.LifeAdd += OnLifeAdd;
            _livesNotificator.LifeLost += OnLifeLost;
        }

        private void OnLifeLost()
        {
            GameObject go = _liveGOs.First(g => g.activeSelf);

            go.SetActive(false);
        }

        private void OnLifeAdd()
        {
            GameObject go = _liveGOs.First(g => g.activeSelf == false);

            go.SetActive(true);
        }

        private void OnDestroy()
        {
            _livesNotificator.LifeLost -= OnLifeLost;
            _livesNotificator.LifeAdd -= OnLifeAdd;

            _livesNotificator = null;
        }
    }
}
