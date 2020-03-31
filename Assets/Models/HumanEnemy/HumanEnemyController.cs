using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEnemyController : MonoBehaviour
{
    public int weapon = 3;

    private Animator anim;

    private string objectName;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("weapon", weapon);

        objectName = this.gameObject.name;

        DeactiveHairAndFace();
        int hair = Random.Range(0, 7);
        SelectHair(hair);
        int face = Random.Range(1, 12);
        SelectFace(face);

        ChooseWeapon(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeactiveHairAndFace()
    {
        DeactivateSkinnedMesh(objectName + "/Hair_FlatTop");
        DeactivateSkinnedMesh(objectName + "/Hair_Messy");
        DeactivateSkinnedMesh(objectName + "/Hair_Modern");
        DeactivateSkinnedMesh(objectName + "/Hair_Short");
        DeactivateSkinnedMesh(objectName + "/Hair_SidePart");
        DeactivateSkinnedMesh(objectName + "/Hair_SlickedBack");
        DeactivateSkinnedMesh(objectName + "/Hair_Thick");

        DeactivateSkinnedMesh(objectName + "/m01");
        DeactivateSkinnedMesh(objectName + "/m02");
        DeactivateSkinnedMesh(objectName + "/m03");
        DeactivateSkinnedMesh(objectName + "/m04");
        DeactivateSkinnedMesh(objectName + "/m05");
        DeactivateSkinnedMesh(objectName + "/m06");
        DeactivateSkinnedMesh(objectName + "/m07");
        DeactivateSkinnedMesh(objectName + "/m08");
        DeactivateSkinnedMesh(objectName + "/m09");
        DeactivateSkinnedMesh(objectName + "/m10");
        DeactivateSkinnedMesh(objectName + "/m11");
    }

    void DeactivateMesh(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    void DeactivateSkinnedMesh(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        SkinnedMeshRenderer renderer = obj.GetComponent<SkinnedMeshRenderer>();
        renderer.enabled = false;
    }

    void ActivateMesh(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.enabled = true;
    }

    void ActivateSkinnedMesh(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        SkinnedMeshRenderer renderer = obj.GetComponent<SkinnedMeshRenderer>();
        renderer.enabled = true;
    }

    void SelectHair(int i)
    {
        switch(i)
        {
            case 0:
                ActivateSkinnedMesh(objectName + "/Hair_FlatTop");
                break;

            case 1:
                ActivateSkinnedMesh(objectName + "/Hair_Messy");
                break;

            case 2:
                ActivateSkinnedMesh(objectName + "/Hair_Modern");
                break;

            case 3:
                ActivateSkinnedMesh(objectName + "/Hair_Short");
                break;

            case 4:
                ActivateSkinnedMesh(objectName + "/Hair_SidePart");
                break;

            case 5:
                ActivateSkinnedMesh(objectName + "/Hair_SlickedBack");
                break;

            case 6:
                ActivateSkinnedMesh(objectName + "/Hair_Thick");
                break;
        }
    }

    void SelectFace(int i)
    {
        switch (i)
        {
            case 1:
                ActivateSkinnedMesh(objectName + "/m01");
                break;

            case 2:
                ActivateSkinnedMesh(objectName + "/m02");
                break;

            case 3:
                ActivateSkinnedMesh(objectName + "/m03");
                break;

            case 4:
                ActivateSkinnedMesh(objectName + "/m04");
                break;

            case 5:
                ActivateSkinnedMesh(objectName + "/m05");
                break;

            case 6:
                ActivateSkinnedMesh(objectName + "/m06");
                break;

            case 7:
                ActivateSkinnedMesh(objectName + "/m07");
                break;

            case 8:
                ActivateSkinnedMesh(objectName + "/m08");
                break;

            case 9:
                ActivateSkinnedMesh(objectName + "/m09");
                break;

            case 10:
                ActivateSkinnedMesh(objectName + "/m10");
                break;

            case 11:
                ActivateSkinnedMesh(objectName + "/m11");
                break;
        }
    }

    void ChooseWeapon(int i)
    {
        switch(i)
        {
            case 0:
                ActivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Rifle");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpacePistol");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpaceRifleOld");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Broadsword");
                break;

            case 1:
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Rifle");
                ActivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpacePistol");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpaceRifleOld");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Broadsword");
                break;

            case 2:
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Rifle");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpacePistol");
                ActivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpaceRifleOld");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Broadsword");
                break;

            case 3:
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Rifle");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpacePistol");
                DeactivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/SpaceRifleOld");
                ActivateMesh(objectName + "/ROOT/Hips/Spine/Spine1/R Clavicle/R UpperArm/R Forearm/R Hand/R Weapon/Broadsword");
                break;

            default:
                break;
        }
    }
}
