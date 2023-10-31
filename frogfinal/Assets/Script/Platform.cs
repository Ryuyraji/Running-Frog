using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] items; // 아이템 오브젝트들
   

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드//Awake() -> OnEnable() -> Start() 순으로 진행
    private void OnEnable()
    {//setactive==true일때 onEnable 자동실행
        // 발판을 리셋하는 처리
      
        for (int i = 0; i < items.Length; i++)
        {
            if (Random.Range(0, 4) == 0)//0.1.2중에 하나 3/1확률
            {
                items[i].SetActive(true);
            }
            else
            {
                items[i].SetActive(false);
            }
        }
    }


    /*
       private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.instance.AddScore(1); // 아이템을 먹으면 1점씩 획득
                Destroy(gameObject); // 아이템 오브젝트를 삭제
            }
        }*/
    }
