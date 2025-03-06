using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public string playerName;
    public string opponentName;
    public bool isMainCharacter;
    public Sprite MainCharacterPortrait;
    public Sprite OpponentCharacterPortrait;
    [TextArea] public string dia;
}
