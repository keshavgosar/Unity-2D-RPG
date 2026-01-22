using UnityEngine;

public class Object_Checkpoint : MonoBehaviour, ISaveable
{
    [SerializeField] private string checkPointId;
    [SerializeField] private Transform respawnPoint;

    public bool isActive { get; private set; }
    private Animator anim;

    private void Awake()
    {

        anim = GetComponentInChildren<Animator>();

    }

    public string GetCheckpointId() => checkPointId;

    public Vector3 GetPosition() => respawnPoint == null ? transform.position : respawnPoint.position;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(checkPointId))
        {
            checkPointId = System.Guid.NewGuid().ToString();
        }
#endif
    }

    public void ActivateCheckpoint(bool activate)
    {
        isActive = activate;
        anim.SetBool("IsActive", activate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        ActivateCheckpoint(true);
    }

    public void LoadData(GameData data)
    {
        bool active = data.unlockedCheckpoints.TryGetValue(checkPointId, out active);
        ActivateCheckpoint(active);
    }

    public void SaveData(ref GameData data)
    {
        if (isActive == false)
            return;

        if(data.unlockedCheckpoints.ContainsKey(checkPointId) == false)
            data.unlockedCheckpoints.Add(checkPointId, true);
    }
}
