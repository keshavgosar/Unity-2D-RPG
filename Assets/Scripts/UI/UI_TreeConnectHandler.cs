using System;
using UnityEngine;

[Serializable]
public class UI_TreeConnectDetails
{
    public UI_TreeConnectHandler childNode;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float length;
}

public class UI_TreeConnectHandler : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private UI_TreeConnectDetails[] connectionDetails;
    [SerializeField] private UI_TreeConnection[] connections;

    private void OnValidate()
    {
        if(rect == null)
            rect = GetComponent<RectTransform>();

        if (connectionDetails.Length != connections.Length)
        {
            Debug.Log("Amount of details shoub be same as amount of connection. - " + gameObject.name);
            return;
        }

        UpdateConnections();
    }

    private void UpdateConnections()
    {
        for (int i =0; i< connectionDetails.Length; i++)
        {
            var detail = connectionDetails[i];
            var connection = connections[i];
            Vector2 targetPosition = connection.GetConnectPoint(rect);

            connection.DirectConnection(detail.direction, detail.length);
            detail.childNode.SetPosition(targetPosition);
        }
    }

    public void SetPosition(Vector2 position) => rect.anchoredPosition = position;
}
