using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour {
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisuals;

    private void Awake() {
        // init list
        plateVisuals = new List<GameObject>();
    }

    public void Start() {
        plateCounter.OnPlateSpwaned += PlateCounter_OnPlateSpwaned;

        plateCounter.OnPlateTaken += PlateCounter_OnPlateTaken;
    }

    private void PlateCounter_OnPlateSpwaned(object sender, System.EventArgs e) {
        Transform plateVisualTransfrom = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffset = 0.1f;

        plateVisuals.Add(plateVisualTransfrom.gameObject);

        plateVisualTransfrom.localPosition = new Vector3(0, plateOffset * plateVisuals.Count, 0);
    }

    private void PlateCounter_OnPlateTaken(object sender, System.EventArgs e) {
        GameObject plateVisual = plateVisuals[plateVisuals.Count - 1];
        plateVisuals.Remove(plateVisual);

        Destroy(plateVisual);
    }
}
