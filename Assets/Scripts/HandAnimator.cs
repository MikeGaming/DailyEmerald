using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class HandAnimator : MonoBehaviour
{

    [SerializeField]
    XRInputValueReader<float> m_GripInput = new XRInputValueReader<float>("Grip");
    [SerializeField]
    XRInputValueReader<float> m_TriggerInput = new XRInputValueReader<float>("Trigger");
    [SerializeField]
    XRInputValueReader<float> m_ThumbstickInput = new XRInputValueReader<float>("thumbstickTouched");

    public Animator handAnimator;

    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!handAnimator) return;
        handAnimator.SetFloat("Grip", m_GripInput.ReadValue());
        handAnimator.SetFloat("Trigger", m_TriggerInput.ReadValue());
        handAnimator.SetFloat("Thumbstick", m_ThumbstickInput.ReadValue());
    }
}
