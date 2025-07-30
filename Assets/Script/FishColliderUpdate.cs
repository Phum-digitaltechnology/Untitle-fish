using UnityEngine;

public class FishColliderUpdate : MonoBehaviour
{
    [SerializeField] FishTugMinigame fishTug;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //fishTug.FoundSomething(other);
    }

}
