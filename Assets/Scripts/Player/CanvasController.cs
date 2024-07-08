using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] GameObject interactionGO;
    [SerializeField] GameObject instructionsGO;

    private Text interactionText;
    private Text descriptionText;

    public int actualScore = 0;

    private void Start()
    {
        interactionText = interactionGO.GetComponent<Text>();
        descriptionText = interactionGO.transform.GetChild(0).GetComponent<Text>();
        StartCoroutine(InstructionsDelay());
    }

    public void AddScore(int score)
    {
        actualScore = int.Parse(scoreText.text);
        actualScore += score;
        if (actualScore < 0) actualScore = 0;
        scoreText.text = actualScore.ToString();
    }

    public void SetInteractionText(string text)
    {
        interactionText.text = text + " (E)";
    }

    public void SetDescriptionText(string text)
    {
        descriptionText.text = text;
    }

    public void ToggleInteraction(bool isActive)
    {
        interactionGO.SetActive(isActive);
    }

    private IEnumerator InstructionsDelay()
    {
        yield return new WaitForSeconds(8f);
        instructionsGO.SetActive(false);
    }
}
