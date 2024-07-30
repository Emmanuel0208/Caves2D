using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_InputField sizeXInput;
    public TMP_InputField sizeYInput;
    public TMP_InputField iterationsInput;
    public Toggle steppedToggle;
    public Button generateButton;
    public Button stepButton;
    public CaveGenerator caveGenerator;

    void Start()
    {
        generateButton.onClick.AddListener(OnGenerateGridButtonClicked);
        stepButton.onClick.AddListener(OnStepButtonClicked);
        stepButton.interactable = false;
    }

    void OnGenerateGridButtonClicked()
    {
        caveGenerator.sizeX = int.Parse(sizeXInput.text);
        caveGenerator.sizeY = int.Parse(sizeYInput.text);
        caveGenerator.iterations = int.Parse(iterationsInput.text);
        caveGenerator.stepped = steppedToggle.isOn;

        caveGenerator.GenerateGrid();
        stepButton.interactable = caveGenerator.stepped;

        caveGenerator.StartSimulation();
    }

    void OnStepButtonClicked()
    {
        caveGenerator.StepSimulation();
    }
}
