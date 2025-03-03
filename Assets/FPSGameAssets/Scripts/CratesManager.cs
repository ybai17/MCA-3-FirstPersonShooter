using UnityEngine;

public class CratesManager : MonoBehaviour
{
    public int numAmmoCrates = 2;
    public int numQuidditchCrates = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get number of crates in level
        int numCrates = transform.childCount;

        //assign one crate to contain the quidditch chest
        int quidditchCrateID = Random.Range(0, numCrates - 1);

        transform.GetChild(quidditchCrateID).GetComponent<Breakable>().MarkAsQuidditchCrate();

        Debug.Log("Marked crate " + quidditchCrateID + " as quidditch chest");

        //assign some crates to spawn ammo when broken

        for (int i = 0; i < 2; i++) {
            int currentCrateID = Random.Range(0, numCrates - 1);
            Transform currCrate = transform.GetChild(currentCrateID);

            while (currCrate.GetComponent<Breakable>().CrateType == 1 ||
                    currCrate.GetComponent<Breakable>().CrateType == 2) {
                
                currentCrateID = Random.Range(0, numCrates - 1);
                currCrate = transform.GetChild(currentCrateID);
            }

            //found a default crate
            currCrate.GetComponent<Breakable>().MarkAsAmmoCrate();

            Debug.Log("Marked crate " + currentCrateID + " as AMMO chest");
        }
    }
}
