using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NightManager : MonoBehaviour {
    /// <summary>
    /// Dune, Criket, Lioness, Reggie, Mikey, Sally
    /// </summary>
    public static int[] AnimatronicLevels = new int[6] { 0, 0, 0, 0, 0, 0 };
    public GameObject[] Animatronics = new GameObject[6];
    private int GameNight;
    [SerializeField]
    private TextMeshProUGUI TimeText;
    private enum AnimatronicIndex {
        Dune = 0,
        Criket,
        Lioness,
        Reggie,
        Mikey,
        Sally
    }

    private float TimePerHour = 90; // Seconds
    private int _currentHour = 0;
    public int CurrentHour 
    {
        get => _currentHour;
        private set {
            _currentHour = value;
            TimeText.text = $"{_currentHour} AM";
        }
    }  

    private void Start() {
        GameNight = NightInformation.GameNight;
        GetAiLevels();
        SetAiLevels();
        StartCoroutine(NightCouroutine());
    }

    private void GetAiLevels() {
        for (int i = 0; i < 6; i++)
            AnimatronicLevels[i] = (GameNight <= 6) ? NightInformation.nightLevels[GameNight - 1, i] : NightInformation.AiLevels[i];
    }

    private void SetAiLevels() {
        for (int i = 0; i < Animatronics.Length; i++)
            if (Animatronics[i] != null)
                Animatronics[i].GetComponent<Animatronic>().SetAiLevel(AnimatronicLevels[i]);

    }
    
    private IEnumerator NightCouroutine() {
        while (CurrentHour < 6) {
            yield return new WaitForSeconds(TimePerHour);
            CurrentHour++;
        }

        SceneManager.LoadScene("Main Menu");
    }
    
}
