using System;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject 
    {
        [Header("Weapons")]
        [SerializeField] private float weaponRange;
        [SerializeField] private float weaponDamage;
        [SerializeField] private float weaponKnockback;
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private AnimatorOverrideController weaponOverrideController;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile = null;
        [SerializeField] public bool CanRuneAttack = false;
        const string weaponName = "Weapon";
        public void Spawn(Transform rightSocket, Transform leftSocket, Animator animator)
        {
            DestroyOldWeapon(rightSocket, leftSocket);
            if(weaponPrefab == null){return;}
            Transform handTransform = GetTransform(rightSocket, leftSocket);
            GameObject weapon = Instantiate(weaponPrefab, handTransform);
            weapon.name = weaponName;
            animator.runtimeAnimatorController = weaponOverrideController;
        }

        private void DestroyOldWeapon(Transform rightSocket, Transform leftSocket)
        {
            Transform oldWeapon = rightSocket.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftSocket.Find(weaponName);
            }
            if(oldWeapon == null){return;}

            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
        private Transform GetTransform(Transform rightSocket, Transform leftSocket)
        {
            Transform handTransform;
            if(isRightHanded) handTransform = rightSocket;
            else handTransform = leftSocket;
            return handTransform;
        }
        public void LaunchProjectile(Transform rightSocket, Transform leftSocket, Health target, float damage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightSocket, leftSocket).position, Quaternion.identity);
            projectileInstance.SetTarget(target, damage);
        }
        public void LaunchProjectile(Transform rightSocket, Transform leftSocket)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightSocket, leftSocket).position, Quaternion.identity);
        }
        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }
        public float GetWeaponKnokcback()
        {
            return weaponKnockback;
        }
    }
}
