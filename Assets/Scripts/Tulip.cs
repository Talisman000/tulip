using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tulip : MonoBehaviour
{
    public CharacterInfo characterInfo;
    private void Start()
    {
        transform.DOPunchScale(Vector2.one * 0.3f, 0.3f, 1);
    }

    public IEnumerator DestroyCoroutine(){
        transform.DOScale(Vector2.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        yield break;
    }
}
