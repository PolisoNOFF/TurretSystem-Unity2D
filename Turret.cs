using System;
using UnityEngine;
using pol1son.turret.projectile;

namespace pol1son.turret
{
    public abstract class Turret : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public Transform tower;
        public Transform firePoint;
        public float fireRate = 1f;
        public int maxAmmo = 20;
        public float range = 5f;
        public LayerMask playerLayer;

        protected int currentAmmo;

        private float fireCountdown = 0f;
        private Transform target;

        protected virtual void Start()
        {
            currentAmmo = maxAmmo;
        }

        protected virtual void Update()
        {
            FindTarget();

            // если есть цель, то поворачиваем башню к ней
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                direction.y = 0;

                direction.Normalize();

                Quaternion lookRotation = Quaternion.LookRotation(direction);

                // юзаем вращение к башне
                tower.rotation = lookRotation;
            }

            if (currentAmmo <= 0)
            {
                return;
            }

            fireCountdown -= Time.deltaTime;

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        void FindTarget()
        {
            // создаем невидимый круг вокруг турели
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, playerLayer);

            if (hits.Length > 0)
            {
                // если нашли игрока, устанавливаем его как цель
                target = hits[0].transform;
            }
            else
            {
                target = null;
            }
        }

        protected virtual void Shoot()
        {
            if (target != null)
            {
                GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                TurretProjectile projectile = projectileGO.GetComponent<TurretProjectile>();
                Vector2 direction = (target.position - transform.position).normalized;

                projectile.SetDirection(direction);
                currentAmmo--;
            }
        }

        public void Reload()
        {
            currentAmmo = maxAmmo;
            Debug.Log("Reloaded!");
        }
    }
}
