using TMPro;
using UnityEngine;

namespace Arkanoid.UI
{
    public class ScorePanel : MonoBehaviour
    {
        private const string SCORE_FORMAT = "{0:00#\\.###\\.###\\.###}";

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
            _highScoreTMesh.text = "HS " + string.Format(SCORE_FORMAT, data.HighScore);
            _currentScoreTMesh.text = "CS " + string.Format(SCORE_FORMAT, data.CurrentScore);
        }

        private void OnDestroy()
        {
            _notifier.ScoreChanged -= OnScoreChanged;
            _notifier = null;
        }
    }
}
