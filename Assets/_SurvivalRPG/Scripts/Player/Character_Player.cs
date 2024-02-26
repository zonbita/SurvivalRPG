using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MoveByKey))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(SurvivalManager))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(AlwaysLookAt))]
[RequireComponent (typeof(Looter))]
public class Character_Player : CharacterBase
{
    public static Character_Player Instance;
    private readonly int AttackHash = Animator.StringToHash("Attack");
    internal MoveByKey movement;
    internal PlayerStats playerStats;
    internal SurvivalManager survivalManager;
    internal PlayerInventory playerInventory;
    internal AlwaysLookAt alwaysLookAt;
    internal Looter looter;
    [SerializeField] LayerMask EnemyLayerMask;
    [SerializeField] float Radius = 3;
    [SerializeField] BoxCollider AttackBox;

    [Header("Equipment")]
    public Transform headTransform;
    public Transform chestTransform;
    public Transform PantsTransform;
    public Transform BootsTransform;
    public Transform rightHandTransform;
    public Transform leftHandTransform;

    Dictionary<EEquipType, GameObject> AttachList = new();

    [Header("Gameobject Equipment")]
    public GameObject Head;
    public GameObject Chest;
    public GameObject Pants;
    public GameObject Boots;

    Transform EnemyTransform;

    CinemachineVirtualCamera virtualCamera;
    protected override void Awake()
    {
        base.Awake();
        if (!Instance) Instance = this;
        movement = GetComponent<MoveByKey>();
        playerStats = GetComponent<PlayerStats>();
        survivalManager = GetComponent<SurvivalManager>();
        looter = GetComponent<Looter>();
        playerInventory = GetComponent<PlayerInventory>();
        alwaysLookAt = GetComponent<AlwaysLookAt>();
        playerStats.OnDied += OnDied;
        playerStats.OnHealth.AddListener(OnHealth);
    }
    void Start()
    {
        InitAttach();

        virtualCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = this.transform;
       
        playerInventory.InitInventory(25, null, 0);

        GameManager.Instance.GameRevive += () => 
        {
            _animator.Rebind();
            _animator.Update(0f);
            movement.enabled = true;
            looter.enabled = true;
            survivalManager.enabled = true;
            alwaysLookAt.enabled = false;
            playerStats.enabled = true;
        };
    }

    private void OnDied()
    {
        movement.enabled = false;
        looter.enabled = false;
        survivalManager.enabled = false;
        GameManager.Instance.GameOver();
    }
    
    public override void Attack()
    {
        if (AttachList[EEquipType.Weapon])
        {
            if (Time.time - attackTime < 2.0)
                return;

            attackTime = Time.time;
            _animator.SetInteger("WeaponState", 1);
            _animator.SetTrigger("Attack");


            EnemyTransform = FindEnemy();
            StartAttackBox();
        }
        else
        {
            if (Time.time - attackTime < 2.0)
                return;

            attackTime = Time.time;
            _animator.SetInteger("WeaponState", 0);
            _animator.SetTrigger("Attack");

            EnemyTransform = FindEnemy();
            StartAttackBox();
        }

    }

    IEnumerator ResetEnemyTarget()
    {
        yield return new WaitForSeconds(1);
        EnemyTransform = null;
    }

    private void Update()
    {
        if (EnemyTransform) FaceTarget(EnemyTransform);
    }

    Transform FindEnemy()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, Radius, Vector3.up, 1, EnemyLayerMask);
        if (hits != null && hits.Length > 0)
        {
            Transform closestTransform = hits
            .Select(x => x.transform)
            .OrderBy(transform => Vector3.Distance(transform.position, this.transform.position))
            .FirstOrDefault();

            StartCoroutine( ResetEnemyTarget() );

            return closestTransform;
        }
        return null;
    }

    void StartAttackBox()
    {
        RaycastHit[] hits = Physics.BoxCastAll(AttackBox.transform.position, AttackBox.size, Vector3.up, AttackBox.transform.rotation, EnemyLayerMask);
        if (hits != null && hits.Length > 0)
        {
            foreach(RaycastHit hit in hits)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    int zdamage;
                    zdamage = (int)playerStats.attTotal.Get_A_Attribute(EAttribute.Strength);
                    print(playerStats.attTotal.Get_A_Attribute(EAttribute.Strength));
                    hit.transform.gameObject.GetComponent<IDamageable>().TakeDamage(zdamage);
                }
                    
            }
        }
    }

    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }


    private void OnHealth(float hp)
    {
       
    }

    public void InitAttach()
    {
        AttachList.Add(EEquipType.Helmet, Head);
        AttachList.Add(EEquipType.Chest, Chest);
        AttachList.Add(EEquipType.Pants, Pants);
        AttachList.Add(EEquipType.Boots, Boots);
        AttachList.Add(EEquipType.Weapon, null);
    }

    public void Attach(EEquipType type, GameObject Prefab)
    {
        if (AttachList.ContainsKey(type))
        {


            if (type == EEquipType.Helmet || type == EEquipType.Chest || type == EEquipType.Pants || type == EEquipType.Boots)
            {
                AttachList[type].SetActive(true);
            }
            else
            {
                if (AttachList[type] != null)
                {
                    Destroy(AttachList[type].gameObject);
                }

                Transform t = headTransform;
                switch (type)
                {
                    case EEquipType.Helmet:
                        t = headTransform;
                        break;
                    case EEquipType.Chest:
                        t = chestTransform;
                        break;
                    case EEquipType.Pants:
                        t = PantsTransform;
                        break;
                    case EEquipType.Boots:
                        t = BootsTransform;
                        break;
                    case EEquipType.Weapon:
                        t = leftHandTransform;
                        break;
                }
                GameObject go = Instantiate(Prefab, t);
                go.transform.localPosition = Vector3.zero;
                AttachList[type] = go;
            }

        }
    }
}

