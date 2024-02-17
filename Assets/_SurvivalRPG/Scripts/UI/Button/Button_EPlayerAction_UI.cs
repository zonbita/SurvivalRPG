using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImagePlayerAction
{
    public EPlayerAction playerAction;
    public Image image;
}

public class Button_EPlayerAction_UI : Singleton<Button_EPlayerAction_UI>
{
    public List<ImagePlayerAction> ListPlayerActions;

    public ImagePlayerAction Setup(EPlayerAction playerAction)
    {
        return ListPlayerActions.Find(x => x.playerAction == playerAction);
    }
}
