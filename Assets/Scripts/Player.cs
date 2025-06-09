using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour {
    public float m_speed = 1;	   
    public float m_life = 3;
	[SerializeField] private Text m_PlayerLife;
    // prefab
    public Transform m_rocket;
    protected Transform m_transform;
    float m_rocketRate = 0;
    // ����
    public AudioClip m_shootClip;
    // ����Դ
    protected AudioSource m_audio;
    // ��ը��Ч
    public Transform m_explosionFX;

	// Use this for initialization
	void Start () {
        m_transform = this.transform;
        m_audio = this.GetComponent<AudioSource>();
	}	
	// Update is called once per frame
	void Update () {
		m_PlayerLife.text = "PlayerLife:"+ m_life;
	//	m_PlayerLife.text = "PlayerLife:<color=green><b>"+ m_life  +" </b></color>";
		transform.Translate (Time.deltaTime * m_speed * Input.GetAxis ("Horizontal"), 0, Time.deltaTime * m_speed * Input.GetAxis ("Vertical"), Space.World);
        m_rocketRate -= Time.deltaTime;
        if ( m_rocketRate <= 0 )
        {
            m_rocketRate = 0.1f;
            if ( Input.GetKey( KeyCode.Space ) || Input.GetMouseButton(0) )
            {
                Instantiate( m_rocket, m_transform.position, m_transform.rotation );
                // �����������
                m_audio.PlayOneShot(m_shootClip);
            }
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("PlayerRocket") != 0)
        {
            m_life -= 1;
			m_PlayerLife.text = "PlayerLife:"+ m_life ;
            if (m_life <= 0) 
            {
                // ��ը��Ч
                Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
