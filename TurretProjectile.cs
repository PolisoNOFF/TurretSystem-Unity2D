using UnityEngine;

namespace pol1son.turret.projectile
{
    public class TurretProjectile : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float speed = 5f;
        public int damage = 10;
        public float lifeTime = 3f;
        public bool isHealing = false;

        void Start()
        {
            Destroy(gameObject, lifeTime); // Уничтожаем снаряд по истечении времени жизни
        }

        public void SetDirection(Vector2 direction)
        {
            rb.linearVelocity = direction * speed;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Получаем компонент здоровья игрока (предполагается, что он есть)
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    if (isHealing)
                    {
                        player.Heal(damage);
                    }
                    else
                    {
                        player.TakeDamage(damage);
                    }
                }

                Destroy(gameObject); // Уничтожаем снаряд при попадании
            }
            else if (!other.CompareTag("Turret") && !other.CompareTag("Projectile"))
            {
                // Уничтожаем снаряд при столкновении с любым другим объектом, кроме турели и других снарядов
                Destroy(gameObject);
            }
        }
    }
}
