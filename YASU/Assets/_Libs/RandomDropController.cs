using UnityEngine;

public class RandomDropController : MonoBehaviour
{
    public GameObject[] PossibleDrops;

    public void Drop()
    {
        if (Random.Range(0, 1f) < 0.33f)
        {
            GameObject drop = Instantiate(PossibleDrops[Random.Range(0, PossibleDrops.Length)]);
            drop.transform.position = transform.position;
            Debug.Log($"Dropped: {drop}", drop);
        }
    }
}