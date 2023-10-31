using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   
   
   
    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.AddScore(1); // 아이템을 먹으면 1점씩 획득
                Destroy(gameObject); // 아이템 오브젝트를 삭제
            
        }
        }
}