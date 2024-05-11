using TMPro;
using UnityEngine;

namespace Arkanoid.UI
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _highScoreTMesh;
        [SerializeField] private TextMeshProUGUI _currentScoreTMesh;

        private IScoreNotifier _notifier;

        public void Construct(IScoreNotifier scoreNotifier)
        {
            _notifier = scoreNotifier;
            _notifier.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(NewScoreData data)
        {
            _highScoreTMesh.text = data.HighScore.ToString();
            _currentScoreTMesh.text = data.CurrentScore.ToString();
        }

        private void OnDestroy()
        {
            _notifier.ScoreChanged -= OnScoreChanged;
            _notifier = null;
        }
    }
}
