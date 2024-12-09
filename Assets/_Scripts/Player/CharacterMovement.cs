using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    #region Variable
    public CharacterController controller;
    public Animator animator;

    public Transform cam;
    public float speed = 4f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    [HideInInspector] public bool isRunning = false;
    public Vector3 direction;
    bool canMove = true;

    // State 
    public enum CharState
    { Normal, Run, Attack, Die }
    public CharState curState;
    //bool isRun;

    [Header("Attack")]
    public int noOfClicks = 0;
    float lastClickTime = 0;
    public float maxCombooDelay = 0.9f;
    public bool isAttacking = false;
    private bool hasSubStamina = false;
    public AtkCollider atkCollider;
    public bool isUserSkill;

    [Header("Status")]
    public Slider staminaSlider;
    [HideInInspector] public float maxStm = 100f;
    public float curStamina;

    [Header("Stamina")]
    [HideInInspector] public float countReturn;

    [Header("Sound")]
    public PlayerSound sound;

    [Header("Skill")]
    public ParticleSystem slashExplosion;
    public SphereCollider slashCollider;
    public SphereCollider explosionCollider;

    [Header("Inventory")]
    public Inventory inventory;
    #endregion

    #region Base Function
    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main.transform; // Tự động gán Camera chính nếu chưa được gán
            if (cam == null)
            {
                Debug.LogError("Camera không được gán và không tìm thấy Camera chính (Main Camera).");
            }
        }
        maxStm = 100f;
        curStamina = maxStm;
        staminaSlider.value = curStamina;
        isUserSkill = false;
        Time.timeScale = 1.0f;

        slashCollider.enabled = false;
        explosionCollider.enabled = false;
    }

    private void FixedUpdate()
    {

        switch (curState)
        {
            case CharState.Normal:
                Movement();
                break;
            case CharState.Run:
                Movement();
                break;
        }
    }

    private void Update()
    {
        Attack();
        SkillControler();
        RecoveryStamina();
        SubStaminaWhenRun();
        SoundControler();
    }
    #endregion
    private void Movement()
    {
        if (!canMove || isAttacking || animator.GetCurrentAnimatorStateInfo(0).IsName("SkillExplosion")) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            if (Input.GetMouseButton(1) && curState != CharState.Run && curStamina > 3f)
            {
                ChageState(CharState.Run);
                isRunning = true;
            }
            else if (curState == CharState.Run && (curStamina <= 3f || !Input.GetMouseButton(1)))
            {
                // Chuyển về Normal khi hết Stamina hoặc không giữ chuột phải
                ChageState(CharState.Normal);
                isRunning = false;
            }
        }
        else if (curState != CharState.Normal)
        {
            ChageState(CharState.Normal);
        }

        animator.SetFloat("Speed", direction.magnitude);
    }

    public void ChageState(CharState newState)
    {
        switch (curState)
        {
            case CharState.Normal:
                break;
            case CharState.Run:
                break;
            case CharState.Attack:
                break;
            case CharState.Die:
                break;
        }

        switch (newState)
        {
            case CharState.Normal:
                animator.SetBool("Run", false);
                //isAttacking = false;
                speed = 4f;
                break;
            case CharState.Run:
                animator.SetBool("Run", true);
                speed = 13f;
                break;
            case CharState.Attack:
                //animator.SetBool("Attack", true);
                animator.SetFloat("Speed", 0f);
                break;
            case CharState.Die:
                break;
        }

        curState = newState;
    }

    #region Attack
    void Attack()
    {
        if (!canMove || isUserSkill || inventory.isInventoryOpen) return;
        if (noOfClicks > 0)
        {
            isAttacking = true;
        }
        else if (noOfClicks == 0)
        {
            isAttacking = false;
        }

        if (Time.time - lastClickTime > maxCombooDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetMouseButtonDown(0) && curStamina >= 7f) // Chỉ cho phép Attack nếu curStamina đủ
        {
            lastClickTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                SubStaminaWhenAttack();
                animator.SetBool("Attack01", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 5);
        }
    }

    public void EndAttack01()
    {
        if (noOfClicks >= 2)
        {
            SubStaminaWhenAttack();
            hasSubStamina = true;

            animator.SetBool("Attack02", true);
            animator.SetBool("Attack01", false);
        }
        else
        {
            hasSubStamina = false;
            animator.SetBool("Attack01", false);
        }
    }

    public void EndAttack02()
    {
        if (noOfClicks >= 3)
        {
            SubStaminaWhenAttack();
            hasSubStamina = true;

            animator.SetBool("Attack03", true);
            animator.SetBool("Attack02", false);
        }
        else
        {
            hasSubStamina = false;
            animator.SetBool("Attack02", false);
        }
    }

    public void EndAttack03()
    {
        if (noOfClicks >= 4)
        {
            SubStaminaWhenAttack();
            hasSubStamina = true;

            animator.SetBool("Attack04", true);
            animator.SetBool("Attack03", false);
        }
        else
        {
            hasSubStamina = false;
            animator.SetBool("Attack03", false);
        }
    }

    public void EndAttack04()
    {
        if (noOfClicks >= 5)
        {
            SubStaminaWhenAttack();
            hasSubStamina = true;

            animator.SetBool("Attack05", true);
            animator.SetBool("Attack04", false);
        }
        else
        {
            hasSubStamina = false;
            animator.SetBool("Attack04", false);
        }
    }

    public void EndAttack05()
    {
        isAttacking = false;
        animator.SetBool("Attack01", false);
        animator.SetBool("Attack02", false);
        animator.SetBool("Attack03", false);
        animator.SetBool("Attack04", false);
        animator.SetBool("Attack05", false);
        noOfClicks = 0;
    }
    #endregion

    #region Skill
    public void SkillControler()
    {
        if (isAttacking) return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isUserSkill = true;

            if (Input.GetMouseButtonDown(0) && !slashExplosion.isPlaying &&
                !animator.GetCurrentAnimatorStateInfo(0).IsName("SkillExplosion") &&
                !sound.soundAttack01.isPlaying)
            {
                canMove = false;
                animator.SetTrigger("SkillExplosion");
                sound.soundAttack01.Play();
                speed = 0;
                Invoke(nameof(EffectDelay), 0.5f);
                Invoke(nameof(ResetSkillState), 1.65f);
                SubStaminaWhenUserSkillSlashExplositon();
            }
        }
        else
        {
            isUserSkill = false;
        }
    }

    void ResetSkillState()
    {
        canMove = true;
        isUserSkill = false;
        speed = 3f;
        Invoke(nameof(EndSkillCollider), 0.3f);
    }

    void EffectDelay()
    {
        slashExplosion.Play();
        slashCollider.enabled = true;
        Invoke(nameof(ExplosionColliderOn), 0.7f);
    }

    void ExplosionColliderOn()
    {
        explosionCollider.enabled = true;
    }

    void EndSkillCollider()
    {
        slashCollider.enabled = false;
        explosionCollider.enabled = false;
    }

    #endregion

    #region Stamina
    public void SubStaminaWhenRun()
    {
        if (curState == CharState.Run)
        {
            curStamina -= 3f * Time.deltaTime;
            staminaSlider.value = curStamina;

            if (curStamina <= 3) // Khi stamina về 0, chuyển sang trạng thái Normal
            {
                curStamina = 0;
                ChageState(CharState.Normal);
                isRunning = false;
            }
        }
    }

    public void RecoveryMp(int value)
    {
        curStamina += value;
        staminaSlider.value = curStamina;
    }

    public void SubStaminaWhenAttack()
    {
        curStamina -= 7f; // Giảm stamina khi Attack
        if (curStamina < 0)
        {
            curStamina = 0;
        }
        staminaSlider.value = curStamina;
    }

    public void SubStaminaWhenUserSkillSlashExplositon()
    {
        curStamina -= 40;
        staminaSlider.value = curStamina;

    }

    public void RecoveryStamina()
    {
        if (curState == CharState.Run || isAttacking || isUserSkill) return;

        if (curStamina < maxStm)
        {
            curStamina += countReturn * Time.deltaTime;
            staminaSlider.value = curStamina;
        }

        if (direction.magnitude <= 0f)
        {
            countReturn = 7f;
        }
        else if (curState == CharState.Normal)
        {
            countReturn = 4f;
        }
    }
    #endregion

    #region Sound
    public void SoundControler()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Walk"))
        {
            if (!sound.soundWalk.isPlaying)
            {
                sound.soundWalk.Play();
            }
        }
        else
        {
            sound.soundWalk.Stop();
        }

        if (stateInfo.IsName("Run"))
        {
            if (!sound.soundRunning.isPlaying)
            {
                sound.soundRunning.Play();
            }
        }
        else
        {
            sound.soundRunning.Stop();
        }

        if (stateInfo.IsName("WGS_attackA3"))
        {
            if (!sound.soundAttack02.isPlaying)
            {
                sound.soundAttack02.Play();
            }
        }
        else
        {
            sound.soundAttack02.Stop();
        }

        if (stateInfo.IsName("WGS_attackA1") || stateInfo.IsName("WGS_attackA2") || stateInfo.IsName("WGS_attackA5"))
        {
            if (!sound.soundAttack01.isPlaying)
            {
            }
            speed = 0f;
        }
        else
        {
            sound.soundAttack01.Stop();
            if (curState == CharState.Normal)
            {
                speed = 3f;
            }
        }

        if (stateInfo.IsName("WGS_attackA5toStand"))
        {
            if (!sound.soundAttack01.isPlaying)
            {
                sound.soundAttack01.Play();
            }
            speed = 0f;
        }
        else
        {
            if (curState == CharState.Normal)
            {
                speed = 3f;
            }
        }
    }
    #endregion
}