using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    private void Start()
    {
        transform.root.GetComponentInChildren<UI_FadeScreen>().DoFadeIn();
    }
    public void PlayBTN()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        //GameManager.instance.ContinuePlay();
    }

    public void QuitBTN()
    {
        Application.Quit();
    }
}
