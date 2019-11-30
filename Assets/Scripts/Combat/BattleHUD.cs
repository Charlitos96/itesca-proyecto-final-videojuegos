using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour {
    public Text txtName;
    public Slider hpSlider;

    public void SetHUD (Unit unit) {
        txtName.text = unit.unitName;
        hpSlider.maxValue = unit.hp;
        hpSlider.value = unit.initialHp;
    }

    public void setHP (int hp) {
        hpSlider.value = hp;
    }
}