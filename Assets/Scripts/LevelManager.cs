using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string musicGroupName;

    void Start()
    {
        AudioManager.instance.StartBGM(musicGroupName);
    }
}
