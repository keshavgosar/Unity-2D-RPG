using UnityEngine;

public class ChanceTest : MonoBehaviour
{
    [SerializeField] private float chance;
    [SerializeField] private float rollResult;


    [ContextMenu("Try")]
    public void Try()
    {
        rollResult = Random.Range(0, 100);
    }
}
