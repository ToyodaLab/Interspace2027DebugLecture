using UnityEngine;

// Hint: Where does TextMeshProUGUI come from? / TextMeshProUGUI はどこにある?
public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI label;

    void Update()
    {
        label.text = "Score: " + GameManager.Instance.score;
        // Hint: Check the spelling. / つづりを確認
        transfrom.localScale = Vector3.one;
    }
}
