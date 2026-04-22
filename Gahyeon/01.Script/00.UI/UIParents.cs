using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIParents : MonoBehaviour
{
    protected UIDocument uiDocument;
    public VisualElement Root;

    protected virtual void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    public virtual void OnEnable()
    {
        Root = uiDocument.rootVisualElement;
    }

    public virtual void Show() { }

    public virtual void Hide() { }
}
