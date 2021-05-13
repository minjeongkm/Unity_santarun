using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수 3개까지 만들수있어.platformPrefab;얘를 3개만드는거야

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값 ,하나 만들고나서 1.25-2.25 사이로 만들거야
    private float timeBetSpawn; // 다음 배치까지의 시간 간격, 하나 만들고 얼마나 걸렸냐

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치,poolPosition 처음에 저장하는 위치 화면에 안보여
    private float lastSpawnTime; // 마지막 배치 시점


    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        platforms = new GameObject[count];//GameObject만 지금 3개 가지고있어. 껍데기만.

        for(int i=0; i<count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);//poolPosition-위치,Quaternion.identity- 회전값이 없다. 발판이 하나 만들어져서 platforms 1에넣고 2에넣고....
        }

        lastSpawnTime = 0f;

        timeBetSpawn = 0f;
    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치

        if(GameManager.instance.isGameover)//게임오버면 더이상 안해주겠다.
        {
            return;
        }

        if(Time.time >= lastSpawnTime + timeBetSpawn)//29번째 줄 Instantiate로 미리 만들어 놨는데 Time.time 시간에 따라 올려줘.
        {
            lastSpawnTime = Time.time;//Time.time은 현재시점 이걸 라스트스폰타임에 넣어줘
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);//다음값 랜덤하게 정해
            float yPos = Random.Range(yMin, yMax);

            platforms[currentIndex].SetActive(false); //OnEnable 초기화 시켜줄려고
            platforms[currentIndex].SetActive(true);//OnEnable 초기화 시켜줄려고 마지막이 false가 되면 안보여. true로 해줘야돼

            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);//new Vector2(xPos, yPos)새로운 블럭의 위치 

            currentIndex++;//currentIndex 처음엔0이야 17번째줄

            if (currentIndex >= count)//3보다 같거나 커졌/
            {
                currentIndex = 0;
            }
        }
    }
}