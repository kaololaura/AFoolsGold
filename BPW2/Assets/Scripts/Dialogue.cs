using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Dit script geeft in de scene ruimte om de dialoog in te vullen
public class Dialogue
{
    [TextArea(3, 10)]
    public string Sentence;
}
