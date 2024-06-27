using UnityEngine;

/// <summary>
/// ������������� ������������ ������ ������ �����������.
/// </summary>
public class TerrainClipPrevention : MonoBehaviour
{
    /// <summary>
    /// ������������� ������������ ������ ������ �����������.
    /// </summary>
    /// <param name="transform">��������� ������� ������.</param>
    public void PreventTerrainClip(Transform transform)
    {
        float checkDistance = 1.0f; // ���������� ��� �������� ������
        Vector3 forwardDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // ����������� �������� ������
        Vector3 forwardCheckPoint = transform.position + forwardDirection * checkDistance; // ����� �������� �������

        float currentTerrainHeight = Terrain.activeTerrain.SampleHeight(transform.position) + Terrain.activeTerrain.transform.position.y; // ������� ������ ����������� ��� �������
        float forwardTerrainHeight = Terrain.activeTerrain.SampleHeight(forwardCheckPoint) + Terrain.activeTerrain.transform.position.y; // ������ ����������� �������
        float currentPlayerHeight = transform.position.y; // ������� ������ ������

        if (currentPlayerHeight > forwardTerrainHeight) // ���� ����� ���� ����������� �������
        {
            transform.position = new Vector3(transform.position.x, forwardTerrainHeight, transform.position.z); // ����������� ������� ������ �� ������
        }
        else if (currentPlayerHeight < currentTerrainHeight) // ���� ����� ���� ������� �����������
        {
            transform.position = new Vector3(transform.position.x, currentTerrainHeight, transform.position.z); // ����������� ������� ������ �� ������
        }
    }
}