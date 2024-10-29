using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;

    private void Start()
    { // 여기 기능은 뭐야?
        CharacterManager.Instance.Player.condition.uICondition = this;
    }
}
