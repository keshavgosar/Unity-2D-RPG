using UnityEngine;

public class Object_Checkpoint : MonoBehaviour, ISaveable
{
    private Object_Checkpoint[] allCheckpoints;
    private Animator anim;

    private void Awake()
    {

        anim = GetComponentInChildren<Animator>();
        allCheckpoints = FindObjectsByType<Object_Checkpoint>(FindObjectsSortMode.None);
    }



    public void ActivateCheckpoint(bool activate)
    {
        anim.SetBool("IsActive", activate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var points in allCheckpoints)
        {
            points.ActivateCheckpoint(false);
        }

        SaveManager.instance.GetGameData().savedCheckpoint = (Vector2)transform.position;
        ActivateCheckpoint(true);
    }

    public void LoadData(GameData data)
    {
        bool active = data.savedCheckpoint == transform.position;
        ActivateCheckpoint(active);

        if (active)
            Player.instance.TeleportPlayer(transform.position);
    }

    public void SaveData(ref GameData data)
    {

    }
}
