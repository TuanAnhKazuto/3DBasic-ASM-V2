using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public int curHp;
    public int maxHp;
    public ParticleSystem deathEf;
    public CharacterMovement playerScript;
    public GameObject mC;
    public GameObject mC_skin;
    public GameObject root;

    [HideInInspector] public UIManager uiManager;

    private void Awake()
    {
        GameObject uiM = GameObject.FindWithTag("UIManager");
        uiManager = uiM.GetComponent<UIManager>();

        maxHp = 100;
        curHp = maxHp;
        playerScript.enabled = true;
        mC.SetActive(true);
        mC_skin.SetActive(true);
        root.SetActive(true);
        healthBar.UpdateBar(maxHp, curHp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(50);
        }
        OnDead();
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        healthBar.UpdateBar(maxHp, curHp);
    }

    public void OnDead()
    {
        if (curHp <= 0)
        {
            curHp = 0;
            playerScript.animator.SetBool("Death", true);
            playerScript.speed = 0;
            deathEf.Play();
            Invoke(nameof(HideBodyAndDestroy), 0.2f);
        }
    }

    public void HideBodyAndDestroy()
    {
        playerScript.enabled = false;
        mC.SetActive(false);
        mC_skin.SetActive(false);
        root.SetActive(false);
        deathEf.Stop();
        Destroy(gameObject, 0.4f);
        Invoke(nameof(OnGameOver), 0.3f);
    }

    void OnGameOver()
    {
        uiManager.OnGameOverPanel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZomAtk"))
        {
            TakeDamage(1);
        }
    }
}
