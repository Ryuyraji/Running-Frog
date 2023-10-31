using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadItem : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        
            Destroy(gameObject); // 아이템 오브젝트를 삭제
        }
    }
}
