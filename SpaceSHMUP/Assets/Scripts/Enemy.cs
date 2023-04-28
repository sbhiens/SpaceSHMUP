/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[SelectionBase] //forces selection of parent object
public class Enemy : MonoBehaviour
{
    /*** VARIABLES ***/

    [Header("Inscribed")]
    public float speed = 10f;
    public float rollMult = -45f;
    public float pitchMult = 5f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    private BoundsCheck bndCheck; //reference to bounds check component
    
    //method that acts as a field (property)
    public Vector3 pos
    {
        get 
        { 
            return this.transform.position; 
        }
        set 
        { 
            this.transform.position = value; 
        }
    }

    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }//end Awake()

    public Quaternion rot
    {
        get
        {
            return this.transform.rotation;
        }
        set
        {
            this.transform.rotation = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (!bndCheck.isOnScreen)
        {
            if (pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                Destroy(gameObject); //destory the object

            }
        }
    }//end Update()

    void OnCollisionEnter(Collision other)
    {
        GameObject otherGo = other.gameObject;
        if(otherGo.GetComponent<ProjectileHero>() != null)
        {
            Destroy(otherGo);
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

}
