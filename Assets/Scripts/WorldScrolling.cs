using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling: MonoBehaviour {
        [SerializeField] Transform playerTransform;
        Vector2Int currentTilePosition = new Vector2Int(0, 0);
        [SerializeField] Vector2Int playerTilePosition;
        Vector2Int onTileGridPlayerPosition;
        [SerializeField] float tileSize = 4f;
        GameObject[, ] terrainTiles;

        [SerializeField] int terrainTileHorizontalCount, terrainTileVerticalCount;

        [SerializeField] int fovHorizontal = 3, fovVertical = 3;

        private void Awake() {
                terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
        }

        public void Add(GameObject tileGameObject, Vector2Int tilePosition) {
                terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
        }

        // Update is called once per frame
        void Update() {
                playerTilePosition.x = Mathf.RoundToInt(playerTransform.position.x / tileSize);
                playerTilePosition.y = Mathf.RoundToInt(playerTransform.position.y / tileSize);

                playerTilePosition.x -= playerTransform.position.x < 0 ? 1 : 0;
                playerTilePosition.y -= playerTransform.position.y < 0 ? 1 : 0;

                if (currentTilePosition != playerTilePosition) {
                        currentTilePosition = playerTilePosition;

                        onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
                        onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
                        UpdateTilesOnScreen();
                }
        }

        private void UpdateTilesOnScreen() {
                for (int pov_x = -(fovHorizontal / 2); pov_x <= fovHorizontal / 2; pov_x++) {
                        for (int pov_y = -(fovVertical / 2); pov_y <= fovVertical / 2; pov_y++) {
                                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, false);

                                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x,
                                        playerTilePosition.y + pov_y);
                        }
                }
        }

        private Vector3 CalculateTilePosition(int x, int y) {
                return new Vector3(x * tileSize, y * tileSize, 0f);
        }
        private int CalculatePositionOnAxis(float currentValue, bool horizontal) {
                if (horizontal) {
                        if (currentValue >= 0) {
                                currentValue = currentValue % terrainTileHorizontalCount;
                        } else {
                                currentValue += 1;
                                currentValue = terrainTileHorizontalCount - 1 + currentValue % terrainTileHorizontalCount;
                        }
                } else {
                        if (currentValue >= 0) {
                                currentValue = currentValue % terrainTileVerticalCount;
                        } else {
                                currentValue += 1;
                                currentValue = terrainTileVerticalCount - 1 + currentValue % terrainTileVerticalCount;
                        }
                }

                return Mathf.RoundToInt(currentValue);
        }
}