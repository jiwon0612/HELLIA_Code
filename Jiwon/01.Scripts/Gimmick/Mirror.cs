using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Mirror : MonoBehaviour ,IInitializable
{
    [SerializeField] protected Entity target;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected PlayerInputSO playerInput;
    [SerializeField] protected GimmickSoundDataSO switchSound;

    public UnityEvent onStartMirrorEvent;
    public UnityEvent onStopMirrorEvent;
    public UnityEvent onSwitchEvent;
    
    protected MirrorImage _image;
    
    protected bool _isDrawing;

    protected void Awake()
    {
        _image = transform.Find("Image").GetComponent<MirrorImage>();
        
    }

    private void Start()
    {
        _image.Initialized(target.GetComponent<CapsuleCollider2D>(), whatIsTarget, target);
        _image.gameObject.SetActive(false);
    }

    public virtual void StartDrawing()
    {
        if (_isDrawing) return;

        playerInput.InteractEvent += SwitchPlayerAndImage;        
        onStartMirrorEvent?.Invoke();
        _image.gameObject.SetActive(true);
        _isDrawing = true;
    }

    protected virtual void Drawing()
    {
        _image.Mirror();
    }

    protected virtual void Update()
    {
        if (_isDrawing)
            Drawing();
    }
    
    public virtual void StopDrawing()
    {
        playerInput.InteractEvent -= SwitchPlayerAndImage;
        onStopMirrorEvent?.Invoke();
        _isDrawing = false;
        _image.gameObject.SetActive(false);
    }

    public virtual void SwitchPlayerAndImage()
    {
        //switchSound.PlaySound(SoundType.MirrorInteract);
        //_image.ImageParticle.SetStartPoint(target.transform.position);
        onSwitchEvent?.Invoke();
    }

    private void OnDisable()
    {
        if (_isDrawing)
            StopDrawing();
    }

    public void Initialize()
    {
        if (_isDrawing)
            StopDrawing();
    }
}
