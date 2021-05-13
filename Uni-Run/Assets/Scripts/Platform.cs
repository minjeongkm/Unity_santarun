using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 장애물 오브젝트들
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable() {
        // 발판을 리셋하는 처리
        stepped = false;

        for(int i=0; i<obstacles.Length; i++)//obstacles배열 ,obstacles.Length은 배열의 길이를 말해.
        {
            if (Random.Range(0, 3) == 0)//0~2중에 랜덤으로 불러.0일때만 실행해라. 012중에 0만 나왔을때 즉 세번중에 한번만 실행
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);//나머지 두번은 false
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){ //OntriggerEnter - 물체와 물체가 부딪히면 불리고 지나가 통과해 ,isTrigger체크 되어 있으면 불려. OnCollisionEnter2D - 부딪히면 멈춰.isTrigger체크 ㅇ안되어 있으면 불려
      // 플레이어 캐릭터가 자신(플랫폼)을 밟았을때 점수를 추가하는 처리
      if(collision.collider.tag == "Player" && !stepped)//collider의 tag가 player고 밟은게 아닌 상태일때 . 한번만 점수를 주려고 !stepped stepped가 안됐을때 점수를 주는거야. 중복방(
        {
            stepped = true;//그럼 밟은걸로 해주자. true로.
            GameManager.instance.AddScore(1);//점수를 올려.
        }
    }
}