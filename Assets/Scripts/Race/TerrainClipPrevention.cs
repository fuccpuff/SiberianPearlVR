using UnityEngine;

/// <summary>
/// ѕредотвращает проваливание игрока сквозь поверхность.
/// </summary>
public class TerrainClipPrevention : MonoBehaviour
{
    /// <summary>
    /// ѕредотвращает проваливание игрока сквозь поверхность.
    /// </summary>
    /// <param name="transform">“рансформ объекта игрока.</param>
    public void PreventTerrainClip(Transform transform)
    {
        float checkDistance = 1.0f; // рассто€ние дл€ проверки вперед
        Vector3 forwardDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // направление проверки вперед
        Vector3 forwardCheckPoint = transform.position + forwardDirection * checkDistance; // точка проверки впереди

        float currentTerrainHeight = Terrain.activeTerrain.SampleHeight(transform.position) + Terrain.activeTerrain.transform.position.y; // текуща€ высота поверхности под игроком
        float forwardTerrainHeight = Terrain.activeTerrain.SampleHeight(forwardCheckPoint) + Terrain.activeTerrain.transform.position.y; // высота поверхности впереди
        float currentPlayerHeight = transform.position.y; // текуща€ высота игрока

        if (currentPlayerHeight > forwardTerrainHeight) // если игрок выше поверхности впереди
        {
            transform.position = new Vector3(transform.position.x, forwardTerrainHeight, transform.position.z); // корректирую позицию игрока по высоте
        }
        else if (currentPlayerHeight < currentTerrainHeight) // если игрок ниже текущей поверхности
        {
            transform.position = new Vector3(transform.position.x, currentTerrainHeight, transform.position.z); // корректирую позицию игрока по высоте
        }
    }
}