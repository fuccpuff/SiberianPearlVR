using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuController : MonoBehaviour
{
    public GameObject leftControllerObject; // ������ ������ �����������
    public InputHelpers.Button menuButton = InputHelpers.Button.MenuButton; // ������ ����
    public string menuSceneName = "Menu"; // ��� ����� � ����
    public float activationThreshold = 0.1f; // ����� ��������� ������
    public float initializationDelay = 1.0f; // �������� ����� ��������������

    private InputDevice leftController; // ���������� ����� ��� ������ �����������

    void Start()
    {
        StartCoroutine(InitializeController());
    }

    private IEnumerator InitializeController()
    {
        yield return new WaitForSeconds(initializationDelay);

        var inputDevices = new List<InputDevice>();
        InputDeviceCharacteristics leftHandedController = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftHandedController, inputDevices);

        if (inputDevices.Count > 0)
        {
            leftController = inputDevices[0];
        }
        else
        {
            Debug.LogError("No left hand device found");
        }
    }

    public void Update()
    {
        if (leftController.isValid && IsMenuButtonPressed(leftController))
        {
            LoadMenuScene();
        }
    }

    private bool IsMenuButtonPressed(InputDevice device)
    {
        if (device.TryGetFeatureValue(CommonUsages.menuButton, out bool isPressed))
        {
            return isPressed;
        }
        return false;
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}