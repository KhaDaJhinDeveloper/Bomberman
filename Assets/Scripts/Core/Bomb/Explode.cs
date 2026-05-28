using System.Collections;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Hide());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.TAG_WALLBREAK))
        {
            ObjectPooling.Instance.ReturnToPool(KeyPool.KEY_WALL_BREAKED, collision.gameObject);
        }
    }
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.25f);
        ObjectPooling.Instance.ReturnToPool(KeyPool.KEY_EXPLODE, this.gameObject);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
