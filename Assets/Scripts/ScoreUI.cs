using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI label;

    void Update()
    {
        label.text = "Score: " + GameManager.Instance.score;
        transfrom.localScale = Vector3.one;
    }
}
