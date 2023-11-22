
using UnityEngine;

//adiciona o componente CharacterController automaticamente
[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public float speed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    // public Animator animator;
    public bool isPlayerLight = false;

    private AudioSource somDanca;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [SerializeField] Light PlayerLight;

    void Start()
    {
        // somDanca = GetComponents<AudioSource>()[0];

        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    void Update()
    {
        ControlLight();
        if (characterController.isGrounded)
        {
            // Se o jogador estiver no chão, então pode se mover
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX =  speed * Input.GetAxis("Vertical");
            float curSpeedY = speed * Input.GetAxis("Horizontal");
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            int velocidade = (int)curSpeedX;
            // animator.SetInteger("velocidade", velocidade);

            int lado = (int)curSpeedY;
            // animator.SetInteger("lado", lado);

            if (Input.GetButton("Jump"))
            {
                // animator.SetBool("pulando", true);
                moveDirection.y = jumpSpeed;
            }
        }else {
            // animator.SetBool("pulando", false);
        }

        // Aplica gravidade
        moveDirection.y -= gravity * Time.deltaTime;

        // Move o jogador
        characterController.Move(moveDirection * Time.deltaTime);

        // Gira a Câmera
        /*rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);*/

        //Gira a Câmera para os lados
        if (Input.GetButton("Fire1")) {
            rotation.y -= 1 * lookSpeed;
        } else if (Input.GetButton("Fire2")) { 
            rotation.y += 1 * lookSpeed;
        } else {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        }

        // Gira a Câmera para cima
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);

    }

    // void OnTriggerEnter(Collider other) {
    //     if (other.gameObject.CompareTag("Danca")) {
    //         other.gameObject.SetActive(false);
    //         // animator.SetBool("dancando", true);
    //         // somDanca.Play();
    //     }
    // }

    private void ControlLight(){
         if (Input.GetKeyDown(KeyCode.L)){
            isPlayerLight = !isPlayerLight;
            if(isPlayerLight){
                PlayerLight.intensity = 2f;
            } else {
                PlayerLight.intensity = 0f;
            }
        }
    }
}

