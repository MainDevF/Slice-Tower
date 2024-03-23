using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _score = 0;

    private void Start()
    {
        GameManager.OnAddedScore += Score;
    }

    private void Score()
    {
        _score++;
        _text.text = _score.ToString();
    }

    private void OnDestroy()
    {
        GameManager.OnAddedScore -= Score;
    }
}
