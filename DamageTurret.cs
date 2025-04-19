using UnityEngine;

namespace pol1son.turret.damage
{
    public class DamageTurret : Turret
    {
        protected override void Update()
        {
            base.Update();
        }

        void OnDrawGizmosSelected()
        {
            // отображение радиуса атаки в редакторе
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
