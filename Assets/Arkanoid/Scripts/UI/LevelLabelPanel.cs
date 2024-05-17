using Arkanoid.Levels;
using TMPro;
using UnityEngine;
using Zenject;

namespace Arkanoid.UI
{
    public class LevelLabelPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;

        private ILevelsEventNotifier _notifier;

        [Inject]
        public void Contstruct(ILevelsEventNotifier notifier)
        {
            _notifier = notifier;

            _notifier.LevelLoaded += OnLevelLoaded;
        }

        private void OnLevelLoaded(string name)
        {
            _textMesh.text = name;
        }

        private void OnDestroy()
        {
            _notifier.LevelLoaded -= OnLevelLoaded;
        }
    }
}
