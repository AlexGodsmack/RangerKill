using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//[System.Serializable]
public class GenerateStores : MonoBehaviour {

    public static GenerateStores Instance;

    public TextAsset WeaponSpec;
    public TextAsset BulletSpec;

    public void Start() {
        Instance = this;
    }
}

[System.Serializable]
public class StoreStack {
    
    public List<StorePoint> storePoint = new List<StorePoint>();

}


[System.Serializable]
public class StorePoint {

    public int StoreID;
    public string TypeOfStore;
    public int CountOfItem;
    public List<SlvLot> Lot1 = new List<SlvLot>();
    public List<WpnLot> Lot2 = new List<WpnLot>();
    public List<BulLot> Lot3 = new List<BulLot>();
    public List<StffLot> Lot4 = new List<StffLot>();

    public void SlaveRandomize() {

        SlvLot newItem = new SlvLot();

        newItem.Skin = Random.Range(1, 6);
        newItem.FullHealth = Random.Range(9, 90) * 5;
        newItem.Health = newItem.FullHealth;
        if (newItem.FullHealth >= 225) {
            newItem.Damage = Random.Range(4, 12) * 5;//between 20 to 60
        } else {
            newItem.Damage = Random.Range(12, 21) * 5;//between 60 to 105
        }
        newItem.Accuracy = Random.Range(3, 10);
        newItem.Level = 1;
        newItem.Price = newItem.Health + newItem.Damage * newItem.Accuracy;
        newItem.St_Health = newItem.FullHealth;
        newItem.St_Damage = newItem.Damage;
        newItem.St_Accuracy = newItem.Accuracy;
        newItem.Heal_Units = 3;
        newItem.Shot_Units = 5;
        newItem.Rush_Units = 3;

        Lot1.Add(newItem);
    }

    public void WeaponRandomize() {

        string[] GetData = GenerateStores.Instance.WeaponSpec.text.Split('\n');
        WpnLot newItem = new WpnLot();

        newItem.Skin = Random.Range(1, 11);
        newItem.Condition = Random.Range(1, 11);
        newItem.Name = GetData[3 * (newItem.Skin - 1)];
        newItem.Name = newItem.Name.Substring(0, newItem.Name.Length - 1);
        newItem.Damage = int.Parse(GetData[3 * newItem.Skin - 2]);
        newItem.Price = newItem.Condition * int.Parse(GetData[3 * newItem.Skin - 1]);
        Lot2.Add(newItem);
    }

    public void BulletRandomize() {

        string[] GetData = GenerateStores.Instance.BulletSpec.text.Split('\n');
        BulLot newItem = new BulLot();

        newItem.Skin = Random.Range(1, 11);
        newItem.Count = Random.Range(1, 20);
        newItem.Name = GetData[2 * (newItem.Skin - 1)];
        newItem.Name = newItem.Name.Substring(0, newItem.Name.Length - 1);
        newItem.Price = int.Parse(GetData[2 * newItem.Skin - 1]);
        //Debug.Log(newItem.Name);
        Lot3.Add(newItem);
    }

    public void StuffRandomize(int Number) {

        StffLot newItem = new StffLot();
        if (Number == 0) {
            newItem.Skin = 2;
            newItem.Price = 100;
            newItem.Liters = 100;
        } else {
            newItem.Skin = Random.Range(1, 4);
            if (newItem.Skin == 1) {
                newItem.Price = 200;
            }
            if (newItem.Skin == 2) {
                newItem.Price = 100;
                newItem.Liters = 100;
            }
            if (newItem.Skin == 3) {
                newItem.Price = 150;
            }
        }
        Lot4.Add(newItem);
    }

}

[System.Serializable]
public class SlvLot {

    public int Skin;
    public int Price;
    public int Health;
    public int FullHealth;
    public int Damage;
    public int Accuracy;
    public int Level;
    [Space]
    public int St_Health;
    public int St_Damage;
    public int St_Accuracy;
    public int Shot_Units;
    public int Heal_Units;
    public int Rush_Units;

}

[System.Serializable]
public class WpnLot {
    public string Name;
    public int Skin;
    public int Price;
    public int Damage;
    public int Condition;
    public int Bullets;
}

[System.Serializable]
public class BulLot {
    public string Name;
    public int Skin;
    public int Price;
    public int Count;
}

[System.Serializable]
public class StffLot {
    public int Skin;
    public int Price;
    public int Liters;
}
