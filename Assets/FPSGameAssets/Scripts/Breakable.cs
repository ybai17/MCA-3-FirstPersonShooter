using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject cratePieces;
    public GameObject ammoPotion;
    public GameObject quidditchChest;

    //0 = nothing
    //1 = spawn ammo on destruction
    //2 = spawn quidditch chest on destruction
    public int CrateType {get; set;}

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(cratePieces, transform.position, transform.rotation);

        switch (CrateType) {
            case 0:
                break;
            case 1:
                Instantiate(ammoPotion, transform.position, transform.rotation);
                break;
            case 2:
                Instantiate(quidditchChest, transform.position, transform.rotation);
                Debug.Log("SPAWN QUIDDITCH CHEST");
                break;
        }

        Destroy(gameObject);
    }

    public void MarkAsQuidditchCrate()
    {
        CrateType = 2;
    }

    public void MarkAsAmmoCrate()
    {
        CrateType = 1;
    }

    public void MarkAsDefault()
    {
        CrateType = 0;
    }
}
