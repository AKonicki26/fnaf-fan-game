using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CustomNightMenu : MonoBehaviour
{
    // The Other AI Sections
    /*
    [SerializeField]
    private ReggieAI ReggieAI;
    [SerializeField]
    private ReggieAI ReggieAI;
    [SerializeField]
    private ReggieAI ReggieAI;
    [SerializeField]
    private ReggieAI ReggieAI;
    */

    [SerializeField]
    private TextMeshProUGUI[] AILevelIndicators = new TextMeshProUGUI[6];

    public void IncreaseAiLevel(int index) {
        NightInformation.AiLevels[index] = Mathf.Clamp(NightInformation.AiLevels[index] + 1, 0, 20);
        AILevelIndicators[index].text = NightInformation.AiLevels[index].ToString();
    }

    public void DecreaseAiLevel(int index) {
        NightInformation.AiLevels[index] = Mathf.Clamp(NightInformation.AiLevels[index] - 1, 0, 20);
        AILevelIndicators[index].text = NightInformation.AiLevels[index].ToString();
    }

    public static void StartNight(int night) {
        NightInformation.GameNight = night;
        SceneManager.UnloadSceneAsync("Main Menu");
        SceneManager.LoadScene("Main Game Scene");
    }

}
