using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 30;

    private void Start()
    {
        Destroy(gameObject,10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Player>(out Player player))
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
