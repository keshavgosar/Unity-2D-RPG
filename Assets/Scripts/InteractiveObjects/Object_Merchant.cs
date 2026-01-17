using UnityEngine;

public class Object_Merchant : Object_NPC, IInteractable
{
    public void Interact()
    {
        Debug.Log("Open Mevhant's shop");
    }
}
