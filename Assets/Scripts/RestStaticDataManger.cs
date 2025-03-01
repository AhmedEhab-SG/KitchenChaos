using UnityEngine;

public class ResetStaticDataManger : MonoBehaviour {

    private void Awake() {
        BaseCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}