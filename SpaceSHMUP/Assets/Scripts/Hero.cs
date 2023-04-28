/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //forces selection of parent object
public class Hero : MonoBehaviour
{
    /*** VARIABLES ***/

   // PlayerShip Singleton
    static public Hero S; //refence GameManager

    [Header("Inscribed")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40f;

    [Header("Dynamic")]
    [Range(0, 4)]
    public float shieldLevel = 1;
    [Tooltip("This field holds a reference to the last triggering GameObject")]
    private GameObject lastTriggerGo = null;

    GameManager gm; //reference to game manager


    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        //Check if instnace is null
        if (S == null)
        {
            S = this; //set SHIP to this game object
        }
        else //else if SHIP is not null send an error
        {
            Debug.LogError("Hero.Awake()");
        }
    }//end Awake()


    //Start is called once before the update
    private void Start()
    {
        gm = GameManager.GM; //find the game manager
    }//end Start()

    
   
    // Update is called once per frame (page 551)
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }//end Update()

    void Fire()
    {
        GameObject projGo = Instantiate<GameObject>(projectilePrefab);
        projGo.transform.position = transform.position;
        Rigidbody rb = projGo.GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * projectileSpeed;
    }

    //Taking Damage
    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        
        if(go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        Enemy enemy = go.GetComponent<Enemy>();

        if (enemy != null)
        {
            shieldLevel--;
            Destroy(go);
        }
        if (shieldLevel < 0)
        {
            Main.HERO_DIED();
            Destroy(this.gameObject);
        }
    }//end OnTriggerEnter()

    public float shieldLevelM
    {
        get
        {
            return (shieldLevel);
        }
        private set
        {
            shieldLevel = Mathf.Min(value, 4);
            if(value<0)
            {
                Destroy(this.gameObject);
                Main.HERO_DIED();
            }
        }
    }
}
