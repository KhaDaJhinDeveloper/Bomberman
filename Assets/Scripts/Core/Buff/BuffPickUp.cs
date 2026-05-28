using UnityEngine;

public class BuffPickUp : MonoBehaviour
{
    public BuffData buffData;
    private bool canInterac;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.TAG_PLAYER))
        {
            if(this.canInterac)
            {
                BuffManager.S_BuffInstance.ApplyBuff(buffData);
                ObjectPooling.Instance.ReturnToPool(this.buffData.buffKey, this.gameObject);
            }
        }
        if (collision.gameObject.CompareTag(Tag.TAG_WALLBREAK))
        {
            this.canInterac = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.TAG_WALLBREAK))
        {
            this.canInterac = true;
        }
    }
}
