using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class Weapons : MonoBehaviour
{
    public int weaponLeft,weaponRight;
    public Animator anim;
    public Character_Controller chrcrtl;
    public Transform RayCam;
    public TwoBoneIKConstraint LeftArm, RightArm;
    public MultiAimConstraint lefthand, righthand;
    public int  Pisol_Damage;
    public GameObject muzzleflash_L, muzzleflash_R;
    public AudioSource ShootSound, SlashSound;

    public GameObject LeftKatana, RightKatana, LeftGun, RightGun;

    
    // Start is called before the first frame update
    void Start()
    {
        anim.SetInteger("weaponLeft", weaponLeft);
        anim.SetInteger("weaponRight", weaponRight);

        UpdateWeapons();


    }
    public void UpdateWeapons()
    {
        if (weaponLeft == 0)
        {
            LeftKatana.SetActive(true);
            LeftGun.SetActive(false);
        }
        if (weaponLeft == 1)
        {
            LeftKatana.SetActive(false);
            LeftGun.SetActive(true);
        }
        if (weaponRight == 0)
        {
            RightKatana.SetActive(true);
            RightGun.SetActive(false);

        }
        if (weaponRight== 1)
        {
            RightKatana.SetActive(false);
            RightGun.SetActive(true);
        }
     
    }
    public void Katanaleft()
    {
        weaponLeft = 0;
        UpdateWeapons();
    }
    public void Katanaright()
    {
        weaponRight = 0;
        UpdateWeapons();
    }
    public void Gunleft()
    {
        weaponLeft = 1;
        UpdateWeapons();
    }
    public void Gunright()
    {
        weaponRight = 1;
        UpdateWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AimWeapon(int armNo)
    {
        if(armNo==1)
        {
            LeftArm.weight = 1f;
            lefthand.weight = 1f;
        }
        if(armNo==2)
        {
           RightArm.weight = 1f;
            righthand.weight = 1f;
        }
    }
    public void AimDown(int ArmNo)
    {
        if(ArmNo==1)
        {
            LeftArm.weight = 0f;
            lefthand.weight = 0f;
        }
        if(ArmNo==2)
        {
            RightArm.weight = 0f;
            righthand.weight = 0f;
        }
    }

    public void InitAttack(int arm)
    {
       
        anim.SetInteger("weaponLeft", weaponLeft);
        anim.SetInteger("weaponRight", weaponRight);

        if (arm == 1)
        {
            anim.SetBool("attackingLeft", true);
        }
        if (arm == 2)
        {
            anim.SetBool("attackingRight", true);
        }
        SlashSound.Play();
    }

    public void InitShoot(int Side)
    {
        Debug.Log("shoot");
        anim.SetInteger("weaponLeft", weaponLeft);
        anim.SetInteger("weaponRight", weaponRight);

        RaycastHit bullet;
        Physics.Raycast(RayCam.position, RayCam.forward, out bullet);
        if(bullet.collider!=null)
        {
            if (bullet.collider.GetComponent<Health>())
            {
                bullet.collider.GetComponent<Health>().TakeDamage(Pisol_Damage);

            }
        }

        ShootSound.Play();
        if(Side==1)
        {
            muzzleflash_L.SetActive(true);
            StartCoroutine(FinishLeftShot());
        }
        if(Side==2)
        {
            muzzleflash_R.SetActive(true);
            StartCoroutine(FinishRightSHot());
        }


    }
    public IEnumerator FinishLeftShot()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleflash_L.SetActive(false);
    }
    public IEnumerator FinishRightSHot()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleflash_R.SetActive(false);
    }
   

    public void FinishAttack(int ArmNo)
    {
        if (ArmNo == 1)
        {
            anim.SetBool("attackingLeft",false);
            chrcrtl.LeftAtt = false;
        }
        if (ArmNo == 2)
        {
            anim.SetBool("attackingRight", false);
            chrcrtl.RightAtt = false;
        }
    }
}
