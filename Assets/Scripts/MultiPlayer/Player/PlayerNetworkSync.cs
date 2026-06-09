using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNetworkSync : NetworkBehaviour
{
    // Hàm này tự động chạy khi đối tượng được sinh ra (Spawn) trên môi trường mạng
    public override void OnNetworkSpawn()
    {
        // Nếu chúng ta KHÔNG PHẢI là chủ sở hữu (Owner) của nhân vật này
        if (!IsOwner)
        {
            // 1. Tắt script di chuyển vật lý để máy khách khác không tự tính toán di chuyển clone này
            if (TryGetComponent<PlayerMovement>(out var movement))
            {
                movement.enabled = false;
            }
            // 2. Tắt PlayerInput để tránh việc bấm nút trên máy khách điều khiển nhầm nhân vật này
            if (TryGetComponent<PlayerInput>(out var playerInput))
            {
                playerInput.enabled = false;
            }
            // 3. Tắt script đặt bom (PlantingBombs) của clone này
            if (TryGetComponent<PlantingBombs>(out var planting))
            {
                planting.enabled = false;
            }
            // 4. Chuyển Rigidbody2D thành Kinematic ở máy khách khác để tránh va chạm vật lý cục bộ làm lệch vị trí
            if (TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
}
