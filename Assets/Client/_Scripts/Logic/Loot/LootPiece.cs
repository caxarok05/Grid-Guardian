using Scripts.Data;
using Scripts.Data.Loot;
using Scripts.Logic;
using Scripts.Logic.Hero;
using Scripts.Services.GridService;
using Scripts.Services.ManaService;
using Scripts.Services.SaveLoad;
using TMPro;
using UnityEngine;
using Zenject;

public class LootPiece : MonoBehaviour, ISavedProgress
{
    public bool _picked;

    [SerializeField] private Sprite openLootSprite;
    [SerializeField] private GameObject manaDrop;

    [SerializeField] private int rewardMana;
    [SerializeField] private UniqueId uniqueId;


    private IManaManager _manaManager;
    private IGridManager _gridManager;

    [Inject]
    public void Construct(IManaManager manaManager, IGridManager gridManager)
    {
        _manaManager = manaManager;
        _gridManager = gridManager;

    }
    private void Start()
    {
        if (_picked) OpenChest();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CheckAvailable(collision))
        {
            _picked = true;
            OpenChest();
            RestoreMana(collision.GetComponent<IManaUser>());
        } 
    }
    public void UpdateProgress(PlayerProgress progress)
    {
        progress.gridData.chestData.Add(new ChestData() { id = uniqueId.Id, IsOpened = _picked, position = transform.position.AsVectorData() });
    }

    public void LoadProgress(PlayerProgress progress)
    {
    }

    private void OpenChest()
    {       
        _gridManager.ChangeAvailability(gameObject.transform.position, isFree: false);
        PlayPickAnmation();
        ChangeSprite();
    }

    private void RestoreMana(IManaUser player) => _manaManager.RestoreMana(player, rewardMana);

    private bool CheckAvailable(Collider2D collision) =>
        _gridManager.GetTileAtPosition(transform.position) == collision.GetComponent<PlayerMovement>().GetPlayerPosition() && collision.CompareTag("Player") && !_picked;
    private void ChangeSprite() =>
        gameObject.GetComponent<SpriteRenderer>().sprite = openLootSprite;
    private void PlayPickAnmation()
    {
        GameObject drop = Instantiate(manaDrop, transform.position, Quaternion.identity, gameObject.transform);
        drop.GetComponentInChildren<TextMeshProUGUI>().text = rewardMana.ToString();
        drop.GetComponent<Animation>().Play();
    }

}
