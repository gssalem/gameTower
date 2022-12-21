using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float enemyHealth;

    [SerializeField] private float movementSpeed;

    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenShots;

    private GameObject Path;

    public GameObject objetivo;

    public GameObject currentTarget;

    public Rigidbody2D m_Rigidbody;

    private float nextTimeToAttack;

    public Transform healthBar;

    private Vector3 healthBarScale;

    private float healthPercent;

    public Animator animator;

    private GameObject targetTile;
    
    private int indexPath;

    AlliesPath allyPath;

    private bool inEnd;

    public GameObject HUD;

    

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
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

    void updateHealthBar()
    {
        healthBarScale.x = healthPercent * enemyHealth;
        healthBar.localScale = healthBarScale;
    }

    private void Start()
    {
        HUD = GameObject.Find("HUD");
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / enemyHealth;
        Path = GameObject.Find("EnemyPath");
        allyPath = Path.GetComponent<AlliesPath>();
        indexPath = allyPath.Path.Length - 1 ;

        
    }

    public void takeDamage(float amount)
    {
        enemyHealth -= amount;
        updateHealthBar();

        if(enemyHealth <= 0)
        {   
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("is_attack", false);
            animator.SetBool("is_die", true);

            Await();
        }   
    }    

    private void die()
    {   
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }

    private void moveEnemy()
    {
        if(indexPath >= 0)
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

            foreach(GameObject ally in Allies.allies)
            {
                Ally allyScript = ally.GetComponent<Ally>();
                    if(allyScript.allyHealth > 0)
                    {
                        float _distance = (transform.position - ally.transform.position).magnitude;

                        if(_distance < distance)
                        {
                            distance = _distance;
                            currentNearestEnemy = ally;
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
            Ally allyScript = currentTarget.GetComponent<Ally>();

            allyScript.takeDamage(damage);

        }
        if(inEnd)
        {
            HUDManager hudScript = HUD.GetComponent<HUDManager>();

            hudScript.playerTakeDamage(damage);
        }
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
            indexPath -= 1;
        }
        if(col.gameObject.tag == "AllyBase"){
            Debug.Log("AllyBase");
            inEnd = true;
        }
    }

    private void FixedUpdate()
    {
        if(enemyHealth > 0 && currentTarget == null)
        {
            moveEnemy();
        }
    }
}
