using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour
{
    public ResourceSpawner rs;
    private Rigidbody2D rigidbody;
    private Animator animator;
    [SerializeField] private float movementSpeed = 2;
    private bool isRight;
    public Vector2 changeDirectionTime = new Vector2(2,4);
    [Header("Healthbar")]
    public Image healthSlider;
    public TMP_Text healthText;
    public Transform canvasTransform;
    [SerializeField] private LayerMask playerLayerMask;


    [Header("Enemy Characteristics")]
    public int maxHealth;
    public int health;
    [SerializeField] private int damage;
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRecharge;
    [SerializeField] private float hitBounce = 1.5f;


    private void OnDestroy()
    {
        if(gameObject.tag == "Boss")
        {
            SceneManager.LoadScene(1);
        }
        rs.StartCoroutine("EnemyRespawnDelay");
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.fillAmount = (float)health / (float)maxHealth;
        healthText.text = health + "/" + maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("ChangeDirection", Random.Range(changeDirectionTime.x, changeDirectionTime.y), Random.Range(changeDirectionTime.x, changeDirectionTime.y));
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetFloat("AttackRecharge") > 0)
        {
            float newAttackRecharge = animator.GetFloat("AttackRecharge") - Time.deltaTime;
            animator.SetFloat("AttackRecharge", newAttackRecharge);
        }

        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayerMask);
        if (hit)
        {
            Collider2D attackHit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayerMask);
            if (isRight && transform.position.x < hit.transform.position.x)
            {
                CancelInvoke();

                if (attackHit)
                {
                    Debug.Log(attackHit.gameObject.name);
                    animator.SetBool("Attack", true);
                    if (animator.GetFloat("AttackRecharge") <= 0f)
                    {
                        animator.SetFloat("AttackRecharge", attackRecharge);
                    }
                }
                else
                {
                    Debug.Log("no object detected");
                    animator.SetBool("Attack", false);
                }
            }
            else if (!isRight && transform.position.x > hit.transform.position.x)
            {
                CancelInvoke();

                if (attackHit)
                {
                    Debug.Log(attackHit.gameObject.name);
                    animator.SetBool("Attack", true);
                    if (animator.GetFloat("AttackRecharge") <= 0f)
                    {
                        animator.SetFloat("AttackRecharge", attackRecharge);
                    }
                }
                else
                {
                    Debug.Log("no object detected");
                    animator.SetBool("Attack", false);
                }
            }
        }
        else
        {
            if (!IsInvoking("ChangeDirection"))
            {
                InvokeRepeating("ChangeDirection", Random.Range(changeDirectionTime.x, changeDirectionTime.y), Random.Range(changeDirectionTime.x, changeDirectionTime.y));
            }
        }


        if (!animator.GetBool("Attack"))
        {
            if (isRight)
            {
                //rigidbody.velocity = new Vector2(movementSpeed, rigidbody.velocity.y);
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                canvasTransform.localScale = new Vector3(-1, 1, 1);
                animator.SetBool("Walk", true);
            }
            else
            {
                //rigidbody.velocity = new Vector2(-movementSpeed,rigidbody.velocity.y);
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
                canvasTransform.localScale = new Vector3(1, 1, 1);
                animator.SetBool("Walk", true);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    public void ChangeDirection()
    {
        isRight = !isRight;
        
    }
    public void Attack()
    {

        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayerMask); ;

        if (hit)
        {
            if (isRight && transform.position.x < hit.transform.position.x)
            {
                hit.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * hitBounce, hit.GetComponent<Rigidbody2D>().velocity.y);
                hit.gameObject.GetComponent<CharacterController2D>().TakeDamage(damage);
            }
            else if (!isRight && transform.position.x > hit.transform.position.x)
            {
                hit.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * hitBounce, hit.GetComponent<Rigidbody2D>().velocity.y);
                hit.gameObject.GetComponent <CharacterController2D>().TakeDamage(damage);
            }
        }
    }
    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        healthSlider.fillAmount = (float)health / (float)maxHealth;
        healthText.text = health + "/" + maxHealth;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
