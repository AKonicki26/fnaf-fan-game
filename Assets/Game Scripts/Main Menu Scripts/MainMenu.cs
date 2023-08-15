using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Image FadePanel;

    public void QuitGame() {
        Application.Quit();
    }

    public void MenuTransition(GameObject TargetMenu) {
        FadePanel.color = new Color(0, 0, 0, 0);
        FadePanel.enabled = true;
        StartCoroutine(SwitchMenu(TargetMenu));
    }

    private IEnumerator SwitchMenu(GameObject TargetMenu) {
        int step = 7;
        while (true) {
            for (int i = 0; i <= 255; i += step) {
                FadePanel.color = new Color(0, 0, 0, i / 100f);
                yield return null;
            }
            TargetMenu.SetActive(true);
            

            for (int i = 255; i >= 0; i -= step) {
                FadePanel.color = new Color(0, 0, 0, i / 100f);
                yield return null;
            }
            FadePanel.enabled = false;
            gameObject.SetActive(false);
            break;
        }
    }
}
