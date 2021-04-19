using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterController2D : MonoBehaviour
{
    //[SerializeField] private GameObject plusResourceText;
    [SerializeField] private GameObject playerGO;
    private Vector3 startingPosition;
    private Rigidbody2D rigidbody;
    private Animator animator;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask resourceLayer;
    [SerializeField] private Image healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject bloodPS;
    [SerializeField] private GameObject upgradeMenuOBJ; 

    [Header("Player Characteristics")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] public int damage;
    [SerializeField] private float hitBounce = 5f;
    [SerializeField] private float attackRange = 1.7f;
 

    private void Start()
    {
        startingPosition = playerGO.transform.position;
        healthSlider.fillAmount = (float)health / (float)maxHealth;
        healthText.text = health + "/" + maxHealth;
        rigidbody = playerGO.GetComponent<Rigidbody2D>();
        animator = playerGO.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0) && !upgradeMenuOBJ.activeSelf)
        {
            animator.SetBool("Attack", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Attack", false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        Debug.DrawRay(playerGO.transform.position, Vector3.right * attackRange, Color.red);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        animator.SetFloat("magnitude", Mathf.Abs(h));

        if(h < -0.02f)
        {
            playerGO.transform.localScale = new Vector3(1,1,1);
        }
        else if(h > 0.02f)
        {
            playerGO.transform.localScale = new Vector3(-1, 1, 1);
        }
        
        rigidbody.velocity = new Vector2(h * movementSpeed, rigidbody.velocity.y);

        RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.15f, groundLayer);

        if (hit && rigidbody.velocity.y < -0.1f)
        {
            animator.SetBool("Jump", false);
        }
    }

    private void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.15f, groundLayer);
        if(hit)
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("Jump",true);
        } 
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerGO.transform.position, attackRange, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit)
            {
                if (hit.gameObject.GetComponent<MonsterController>() != null)
                {
                    // looking left
                    if (playerGO.transform.localScale.x == 1 && playerGO.transform.position.x > hit.transform.position.x)
                    {
                        hit.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * hitBounce, hit.GetComponent<Rigidbody2D>().velocity.y);
                        if (QuickSlotPanel.currentWeapon == QuickSlotPanel.CurrentWeapon.Sword)
                        {
                            hit.gameObject.GetComponent<MonsterController>().TakeDamage(damage);
                        }
                        else if(QuickSlotPanel.currentWeapon == QuickSlotPanel.CurrentWeapon.Axe || QuickSlotPanel.currentWeapon == QuickSlotPanel.CurrentWeapon.Pickaxe)
                        {
                            hit.gameObject.GetComponent<MonsterController>().TakeDamage(Mathf.CeilToInt((float)damage/20));
                        }
                        Instantiate(bloodPS, hit.transform.GetChild(0).position, Quaternion.identity);
                    }
                    else if (playerGO.transform.localScale.x == -1 && playerGO.transform.position.x < hit.transform.position.x)
                    {
                        if (QuickSlotPanel.currentWeapon == QuickSlotPanel.CurrentWeapon.Sword)
                        {
                            hit.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * hitBounce, hit.GetComponent<Rigidbody2D>().velocity.y);
                            hit.gameObject.GetComponent<MonsterController>().TakeDamage(damage);
                        }
                        else
                        {
                            hit.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * hitBounce, hit.GetComponent<Rigidbody2D>().velocity.y);
                            hit.gameObject.GetComponent<MonsterController>().TakeDamage(Mathf.CeilToInt((float)damage / 20));
                        }

                        Instantiate(bloodPS, hit.transform.GetChild(0).position, Quaternion.identity);
                    }
                }
            }
        }

        Collider2D[] resourcesHit = Physics2D.OverlapCircleAll(playerGO.transform.position, attackRange, resourceLayer);
        foreach(Collider2D resourceHit in resourcesHit)
        if (resourceHit)
        {
            if (playerGO.transform.localScale.x == 1 && playerGO.transform.position.x > resourceHit.transform.position.x || playerGO.transform.localScale.x == -1 && playerGO.transform.position.x < resourceHit.transform.position.x)
            {
                
                
                switch (resourceHit.GetComponent<ResourceItem>().resourceType)
                {
                    case ResourceItem.ResourceTypes.Wood:
                        if (QuickSlotPanel.currentWeapon == QuickSlotPanel.CurrentWeapon.Axe) { 
                            ResourceStorage.AddWood(damage);
                                //Instantiate(plusResourceText, resourceHit.gameObject.transform.position, Quaternion.identity);
                                resourceHit.GetComponent<ResourceItem>().health--;
                            }
                        break;
                    case ResourceItem.ResourceTypes.Stone:
                        if (QuickSlotPanel.currentWeapon == QuickSlotPanel.CurrentWeapon.Pickaxe)
                        {
                            ResourceStorage.AddStone(damage);
                               // Instantiate(plusResourceText, resourceHit.gameObject.transform.position, Quaternion.identity);
                                resourceHit.GetComponent<ResourceItem>().health--;
                            }
                        break;
                    case ResourceItem.ResourceTypes.Gold:
                        if (QuickSlotPanel.currentWeapon == QuickSlotPanel.CurrentWeapon.Pickaxe)
                        {
                            ResourceStorage.AddGold(Mathf.RoundToInt((float)damage/3));
                                //Instantiate(plusResourceText, resourceHit.gameObject.transform.position, Quaternion.identity);
                                resourceHit.GetComponent<ResourceItem>().health--;
                            }
                        break;
                    case ResourceItem.ResourceTypes.Diamond:
                        if (QuickSlotPanel.currentWeapon == QuickSlotPanel.CurrentWeapon.Pickaxe)
                        {
                            ResourceStorage.AddDiamond(Mathf.RoundToInt((float)damage / 5));
                                //Instantiate(plusResourceText, resourceHit.gameObject.transform.position, Quaternion.identity);
                                resourceHit.GetComponent<ResourceItem>().health--;
                            }
                        break;
                }
                if (resourceHit.GetComponent<ResourceItem>().health <= 0)
                {
                    Destroy(resourceHit.gameObject);
                }
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
            health = 100;
            healthSlider.fillAmount = (float)health / (float)maxHealth;
            healthText.text = health + "/" + maxHealth;
            ResourceStorage.wood = 0;
            ResourceStorage.stone = 0;
            ResourceStorage.gold = 0;
            ResourceStorage.diamond = 0;
            ResourceStorage.woodText.text = ResourceStorage.wood.ToString();
            ResourceStorage.stoneText.text = ResourceStorage.stone.ToString();
            ResourceStorage.goldText.text = ResourceStorage.gold.ToString();
            ResourceStorage.diamondText.text = ResourceStorage.diamond.ToString();
            transform.position = startingPosition;
        }
    }
}
