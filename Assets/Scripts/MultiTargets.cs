using UnityEngine;

public class MultiTargets : MonoBehaviour
{
    [SerializeField] private GameObject startModel;

    private int modelsCount;
    private int indexCurrentModel;

    void Start()
    {
        modelsCount = transform.childCount;
        indexCurrentModel = startModel.transform.GetSiblingIndex();
    }

    public void ChangeARModel(int index)
    {
        transform.GetChild(indexCurrentModel).gameObject.SetActive(false);

        int newIndex = indexCurrentModel + index;

        if (newIndex < 0)
        {
            newIndex = modelsCount - 1;
        }
        else if (newIndex > modelsCount - 1)
        {
            newIndex = 0;
        }

        GameObject newModel = transform.GetChild(newIndex).gameObject;
        newModel.SetActive(true);

        indexCurrentModel = newModel.transform.GetSiblingIndex();
    }
}