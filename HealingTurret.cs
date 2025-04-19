using UnityEngine;

namespace pol1son.turret.healing
{
    public class HealingTurret : Turret
    {
        protected override void Update()
        {
            base.Update();
        }

        void OnDrawGizmosSelected()
        {
            // отображение радиуса атаки в редакторе
            Gizmos.color = Color.green; // cделал цвет зеленым, чтобы отличать от атакующей турели
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
