using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testtest : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        BinaryDataMgr.Instance.InitData();
        //TowerInfoContainer towerInfoContainer = BinaryDataMgr.Instance.GetTable<TowerInfoContainer>();
        //print(towerInfoContainer.dataDic[5].name);
    }

    // Update is called once per frame
    void Update() {

    }
}
