using Scripts.Services.WindowService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OpenWindowButton : MonoBehaviour
{
    public Button openButton;
    public WindowId windowId;

    private IWindowService _windowService;

    [Inject]
    public void Construct(IWindowService windowService) => _windowService = windowService;

    private void Awake() => openButton.onClick.AddListener(Open);

    private void Open() => _windowService.Open(windowId);
}
