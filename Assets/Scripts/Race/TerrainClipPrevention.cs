using UnityEngine;

/// <summary>
/// prevents the player from clipping through the terrain.
/// </summary>
public class TerrainClipPrevention : MonoBehaviour
{
    /// <summary>
    /// prevents the player from clipping through the terrain.
    /// </summary>
    /// <param name="transform">transform of the player object.</param>
    public void PreventTerrainClip(Transform transform)
    {
        float checkDistance = 1.0f; // distance for forward check
        Vector3 forwardDirection = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized; // direction for forward check
        Vector3 forwardCheckPoint = transform.position + forwardDirection * checkDistance; // point for forward check

        float currentTerrainHeight = Terrain.activeTerrain.SampleHeight(transform.position) + Terrain.activeTerrain.transform.position.y; // current surface height under the player
        float forwardTerrainHeight = Terrain.activeTerrain.SampleHeight(forwardCheckPoint) + Terrain.activeTerrain.transform.position.y; // surface height ahead of the player
        float currentPlayerHeight = transform.position.y; // current height of the player

        if (currentPlayerHeight > forwardTerrainHeight) // if the player is above the surface ahead
        {
            transform.position = new Vector3(transform.position.x, forwardTerrainHeight, transform.position.z); // adjust the player's position to match the surface height ahead
        }
        else if (currentPlayerHeight < currentTerrainHeight) // if the player is below the current surface
        {
            transform.position = new Vector3(transform.position.x, currentTerrainHeight, transform.position.z); // adjust the player's position to match the current surface height
        }
    }
}