using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
public class PlantingBombsNetwork : NetworkBehaviour
{
    private PlayerStats playerStats;
    private void Start()
    {
        LoadComponents();
    }
    public void Planting(InputAction.CallbackContext context)
    {
        if (!IsOwner && !context.performed) return;
        Vector2Int pos = Vector2Int.RoundToInt(transform.position);
    }    
    [ServerRpc]
    private void RequestPlantingBombServerRpc(Vector2Int pos, ServerRpcParams rpcParams = default)
    {
        ulong clientID = rpcParams.Receive.SenderClientId;
    }    
    private void LoadComponents()
    {
        this.playerStats = GetComponent<PlayerStats>();
    }    
}
