using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;

    [Header("General")]
    [SerializeField] private float gravityScale = -20f;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3;
    [SerializeField] private float runSpeed = 7;

    [Header("Rotation")]
    [SerializeField] private float rotationSensibility;

    //[Header("Jump")]
    //[SerializeField] private float jumpHeight = 1.9f;

    [Header("Crouch")]
    public float crouchHeight;
    public bool crouch;
    [SerializeField] private bool isCrouch = false;
    float currentCrouchHeigt = 1f;

    [Header("LookUp")]
    public float lookUphHeight;
    public bool lookUp;
    [SerializeField] private bool isLookUp = false;
    float currentLookUphHeight = 1f;

    [Header("Animator")]
    public Animator animator;

    [Header("NoteBook")]
    [SerializeField] GameObject NoteBook;
    public bool isBookOpen = false;

    [Header("Log")]
    [SerializeField] GameObject logObject;
    public bool isLogOpen = false;

    [SerializeField] private AudioManager audioManager; 

    [SerializeField] private float smooth = 4f;
    private float cameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //audioManager = AudioManager.instance;
    }
    private void Start()
    {
        if (GameSettings.Instance == null || GameSettings.Instance.mouseSensitivity <= 100)
        {
            rotationSensibility = 400;
        }
        else
        {
            UpdateSensitivity();
        }
    }
    private void Update()
    {
        Look();
        Move();
        Crouch();
        LookUp();
        UpdateAnimator();
        OpenNoteBook();
        OpenLogView();
    }
    public void UpdateSensitivity()
    {
        rotationSensibility = GameSettings.Instance.mouseSensitivity;
    }
    private void OnEnable()
    {
        InGameMenu.OnSensitivityChanged += UpdateSensitivity;
    }
    private void Move()
    {
        if (!PlayerDialogue.isHavingDialogue)
        {
            if(characterController.isGrounded)
            {
                moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
                moveInput = Vector3.ClampMagnitude(moveInput, 1f);

                if (Input.GetButton("Sprint"))
                {
                    moveInput = transform.TransformDirection(moveInput) * runSpeed;
                }
                else
                {
                    moveInput = transform.TransformDirection(moveInput) * walkSpeed;
                }
                //if (Input.GetButtonDown("Jump"))
                //{
                //    moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
                //}
            }
            moveInput.y += gravityScale * Time.deltaTime;
            characterController.Move(moveInput * Time.deltaTime);
        }
    }
    private void Look()
    {
        if (!PlayerDialogue.isHavingDialogue)
        {
            rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
            rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

            cameraVerticalAngle += rotationInput.y;
            cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

            transform.Rotate(Vector3.up * rotationInput.x);
            playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
        }
    }
    private void Crouch()
    {
        if (isLookUp) return;

        if (!PlayerDialogue.isHavingDialogue)
        {
            crouch = Input.GetKeyDown(KeyCode.LeftControl);

            if (crouch)
            {
                isCrouch = !isCrouch;
                currentCrouchHeigt = isCrouch ? crouchHeight : 1f;
            }

            float crouchLocalScaleY = currentCrouchHeigt;
            float newCrouchScaleY = Mathf.Lerp(transform.localScale.y, crouchLocalScaleY, Time.deltaTime * smooth);
            transform.localScale = new Vector3(1, newCrouchScaleY, 1);

        }
    }
    private void LookUp()
    {
        if (isCrouch) return;

        if (!PlayerDialogue.isHavingDialogue)
        {
            lookUp = Input.GetKeyDown(KeyCode.V);

            if (lookUp)
            {
                isLookUp = !isLookUp;
                currentLookUphHeight = isLookUp ? lookUphHeight : 1f;
            }

            float targetLocalScaleY = currentLookUphHeight;
            float newScaleY = Mathf.Lerp(transform.localScale.y, targetLocalScaleY, Time.deltaTime * smooth);
            transform.localScale = new Vector3(1, newScaleY, 1);
        }
    }
    private void UpdateAnimator()
    {
        bool isMoving = moveInput.x != 0 || moveInput.y != 0;
        if(animator != null) animator.SetFloat("velocity", characterController.velocity.magnitude);
    }

    private void OpenNoteBook()
    {
        if (isLogOpen) return;

        if(isBookOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isBookOpen = true;
                NoteBook.SetActive(true);
               
                //audioManager.PlaySFX(audioManager.notebookCheck);
                AudioManager.instance.PlaySoundFX(AudioManager.instance.notebookCheck, transform, 1f);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isBookOpen = false;
                NoteBook.SetActive(false);
                
                //audioManager.PlaySFX(audioManager.notebookClose);
                AudioManager.instance.PlaySoundFX(AudioManager.instance.notebookClose, transform, 1f);
            }
        }
    }

    private void OpenLogView()
    {
        if (isBookOpen) return;

        if (Input.GetKeyDown(KeyCode.L)) {
            isLogOpen = !isLogOpen;

            if (isLogOpen) {
                GameManager.Instance.ShowCursor();
                logObject.SetActive(true);
            }

            if (!isLogOpen) {
                GameManager.Instance.HideCursor();
                logObject.SetActive(false);
            }

        }

    }

    public void PlayAudio(AudioClip audioClip)
    {
        //audioManager.PlayAudio(audioClip);
    }
}