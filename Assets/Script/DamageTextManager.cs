using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(TextMeshPro))]
public class DamageTextManager : MonoBehaviour
{
    public TextMeshPro text;
    public Rigidbody2D rigid;

    public void DamageIndicate(string damage, Transform pos)
    {
        StartCoroutine(TextFlow(damage, pos));
    }

    private IEnumerator TextFlow(string damage, Transform pos)
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 6.3f);
        transform.position = pos.position;
        text.text = damage;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
