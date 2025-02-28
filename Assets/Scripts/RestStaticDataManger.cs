using UnityEngine;

public class ResetStaticDataManger : MonoBehaviour {

    private void Awake() {
        BaseCounter.ResetBaseStaticData();
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}