using UnityEngine;

namespace Atlas.Combat
{
	/**
		For testing purposes.
		Initial (obsolete) representation a weapon.
	*/
    public interface IWeapon
    {

        Collider[] DamageColliders { get; }

        int TotalWeaponDamage { get; }
        int BaseWeaponDamage { get; }

        void PerformAttack(Vector3 direction, int characterDamage);

        void OpenDamageColliders();
        void CloseDamageColliders();
    }
}