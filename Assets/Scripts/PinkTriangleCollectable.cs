using UnityEngine;

public class PinkTriangleCollectable : MonoBehaviour
{
    string test;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    public string getTestString()
    {
        test = "Hellows from pink collectable :3";

        return test;
    }
}
