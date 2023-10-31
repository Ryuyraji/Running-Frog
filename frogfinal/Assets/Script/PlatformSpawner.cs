using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 7; // 생성할 발판의 개수 max=3

    public float timeBetSpawnMin = 1.0f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; //배열을 선언 // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start()
    {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        platforms = new GameObject[count];//여기서 생성한다 게임 오브젝트를 담을 변수 3개만 만들어짐->밑에 instantiate에서 실제로 만들어짐.
        for (int i = 0; i < count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);//애초에 게임 시작할때 미리 3개를 만들어놓는다.(이 전에 오브젝트에  onenable을 생성해서 그걸 부르는거)
        }
        lastSpawnTime = 0f;//마지막 만들고 나서 시간 기록 초기화
        timeBetSpawn = 0f;//이 시간이 지나면 만들어라
    }

    void Update()
    {
        // 순서를 돌아가며 주기적으로 발판을 배치(랜덤하게 만듦)
        if (GameManager.instance.isGameover)
        {
            return;
        }
        if (Time.time >= lastSpawnTime + timeBetSpawn)//마지막으로 만들고 난 시간에 얼마동안 지나고 만드는 시간을 더한 시간보다 크면 
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            float yPos = Random.Range(yMin, yMax);//위치를 어디만들건지 랜덤하게

            platforms[currentIndex].SetActive(false);//유니티에서 체크를 껐다가 키는것->onenable발판 밟으려고??
            platforms[currentIndex].SetActive(true);
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);//위치값만 바꿔주고 있음
            currentIndex++;//0.1.2번 써먹으려고 만약 3이랑 같으면 다시 0.1.2로 돌아감.
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}