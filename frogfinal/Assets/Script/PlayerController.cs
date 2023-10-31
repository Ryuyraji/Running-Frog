  using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; // 사망시 재생할 오디오 클립
    public float jumpForce = 700f; // 점프 힘

    private int jumpCount = 0; // 누적 점프 횟수
    private bool isGrounded = false; // 바닥에 닿았는지 나타냄
    private bool isDead = false; // 사망 상태

    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator animator; // 사용할 애니메이터 컴포넌트
    private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

    private void Start()
    {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 사용자 입력을 감지하고 점프하는 처리
        if (isDead) return;
        if (Input.GetMouseButtonDown(0) && jumpCount < 9)//마우스 딱 눌렀을때 주욱 올라감
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));//addforce(일정하게 올라가~)//(위로 올라가~)속도를 0으로만들어놓고해야함 왜냐 속도가 있으면 덜 나감
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)//올라가는 중=+y,내려가는중=-y중간에 점프하다가 안하고 싶을때 마우스에서 손을 떼면 속도가 줄어들어서 바닥으로 떨어짐
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.4f;//위로 올라갈때만 속도가 줄어들어올
        }

        animator.SetBool("Grounded", isGrounded);//setbool grounded가 불타입 매 프레임마다 둘을 동기


    }

    private void Die()
    {
        // 사망 처리
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerRigidbody.velocity = Vector2.zero;//떨어지지 않게 제자리 
        isDead = true;//한번 죽으면

        GameManager.instance.OnPlayerDead();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {//유니티에 데드에 이즈 트리거 체크해야함
     // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if (other.tag == "Dead" && !isDead)//Deadzone이랑 부딪칠땨ㅐ
        {
            Die();
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {//startplatform 
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)//0.7f->똑바른 발판일때 점프하자라는 뜻//normal ==충돌한 면의 수직인 벡터
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }//on trigger 부딪린거자체가 중요!on collisionEnter는 부딪친 여러정보가 넘어옴

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;

    }
}