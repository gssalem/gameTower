using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] public float allyHealth;

    [SerializeField] private float movementSpeed;

    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenShots;

    [SerializeField] public int custoMana = 2;

    private GameObject Path;

    public GameObject objetivo;

    public Rigidbody2D m_Rigidbody;

    public GameObject currentTarget;

    private float nextTimeToAttack;

    public Transform healthBar;

    private Vector3 healthBarScale;

    private float healthPercent;

    public Animator animator;

    private int indexPath = 0;

    AlliesPath allyPath;

    private bool inEnd = false;

    public GameObject HUD;

    public AudioSource audioAttack;


    private void Awake()
    {
        Allies.allies.Add(gameObject);
    }

    private void Await()
    {
        StartCoroutine("wait_to_die");
    }

    IEnumerator wait_to_die()
    {
      yield return new WaitForSeconds(1);
      die();
    }

    private void Start()
    {
        HUD = GameObject.Find("HUD");
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / allyHealth;
        Path = GameObject.Find("Path");
        allyPath = Path.GetComponent<AlliesPath>();
    }

    void updateHealthBar()
    {
        healthBarScale.x = healthPercent * allyHealth;
        healthBar.localScale = healthBarScale;
    }

    public void takeDamage(float amount)
    {
        allyHealth -= amount;
        updateHealthBar();
        if(allyHealth <= 0)
        {   
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("is_attack", false);
            animator.SetBool("is_die", true);
            Await();
        }   
    }
    

    private void die()
    {   
        Allies.allies.Remove(gameObject);
        Destroy(transform.gameObject);
    }

    private void moveAlly()
    {
        if(indexPath < allyPath.Path.Length)
        {
            objetivo = allyPath.Path[indexPath];
            Vector3 distancia = (transform.position - objetivo.transform.position).normalized; 
            m_Rigidbody.MovePosition(transform.position - distancia * Time.deltaTime * movementSpeed);
        }
    }

    private void updateNearestEnemy()
    {
        if(!inEnd)
        {
            GameObject currentNearestEnemy = null;

            float distance = Mathf.Infinity;

            foreach(GameObject enemy in Enemies.enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                    if(enemyScript.enemyHealth > 0)
                    {
                        float _distance = (transform.position - enemy.transform.position).magnitude;
                        if(_distance < distance)
                        {
                            distance = _distance;
                            currentNearestEnemy = enemy;
                            
                        }
                    }
            }

            if(distance <= range)
            {
                currentTarget = currentNearestEnemy;
                animator.SetBool("is_attack", true);
            }else
            {
                currentTarget = null;
                animator.SetBool("is_attack", false);
            }
        }else{
            currentTarget = HUD;
        }
        
    }

    private void attack()
    {
        if(currentTarget != null && inEnd == false)
        {
            Enemy enemyScript = currentTarget.GetComponent<Enemy>();

            enemyScript.takeDamage(damage);

        }
        if(inEnd)
        {
            HUDManager hudScript = HUD.GetComponent<HUDManager>();

            hudScript.takeDamage(damage);
        }

        audioAttack.Play();
    }

    private void Update()
    {   
        takeDamage(0);
        updateNearestEnemy();

        if(Time.time >= nextTimeToAttack)
        {
            if(currentTarget != null)
            {
                attack();
                nextTimeToAttack = Time.time + timeBetweenShots;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Chegada"){
            indexPath += 1;
        }

        if(col.gameObject.tag == "EnemyBase"){
            inEnd = true;
        }
    }

    private void FixedUpdate()
    {
        if(allyHealth > 0 && currentTarget == null)
        {
            moveAlly();
        }
    }

}
