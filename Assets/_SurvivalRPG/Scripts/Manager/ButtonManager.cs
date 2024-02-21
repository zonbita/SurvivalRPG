using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public ButtonType btnType;


    public Image frame;


    public Image icon;


    public Image subIcon1;


    public Image subIcon2;


    public TextMeshProUGUI text;

    public TextMeshProUGUI subText1;


    public TextMeshProUGUI subText2;

    public int amount;

    public System.Action action;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //AudioManager.Instance.PlaySfx(AudioManager.Sfx.Buy);
            GameManager.Instance.ActionButtonClicked(btnType, gameObject, action);
        });

        GameManager.Instance.RegisterButton(this);
    }

    public void SetFrame(Sprite sprite)
    {
        frame.sprite = sprite;
    }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void SetText(string text)
    {
        this.text.SetText(text);
    }

    public void SetAction(System.Action action)
    {
        this.action = action;
    }
}

public enum ButtonType
{
    NONE,
    HOME,
    SHOP,
    SKIN,
    SETTINGS,
    WHEEL,
    ATTACK1,
    ATTACK2,
    BACK,
    PLAY,
    NEXT,
    RETRY,
    UPGRADE_MOVEMENT,
    UPGRADE_SIZE,
    UPGRADE_ATKSPEED,
    TAB_SKIN,
    TAB_PET,
    CONTENT_SHOP,
    CONTENT_IAP,
    BUY_ITEM,
    PICK_UP,
    UNEQUIP,
    EQUIP,
    REVIVE,
    SWITCH_SWORD,
    SWITCH_DAGGER,
    SWITCH_AXE,
    SWITCH_GUN,
    SWITCH_GAUNTLET,
    SWITCH_BALL,
}