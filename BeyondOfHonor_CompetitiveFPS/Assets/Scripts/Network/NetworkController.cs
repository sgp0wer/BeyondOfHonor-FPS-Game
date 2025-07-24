using FishNet.Object;
using UnityEngine;
using FishNet.Connection;

public class NetworkController : NetworkBehaviour
{
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private MonoBehaviour[] localOnlyScripts;

    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {
        base.OnOwnershipClient(prevOwner);

        if (!IsOwner) return;

        if (cameraHolder != null)
            cameraHolder.SetActive(true);

        foreach (var s in localOnlyScripts)
            s.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
