using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class LayeredTile
    {
        private Tile backgroundTile;
        private Tile foregroundTile;
        private Tile overlayTile;

        public LayeredTile(Tile backgroundTile, Tile foregroundTile, Tile overlayTile)
        {
            this.backgroundTile = backgroundTile;
            this.foregroundTile = foregroundTile;
            this.overlayTile = overlayTile;
        }

        public Tile getBackgroundTile()
        {
            return backgroundTile;
        }
        
        public Tile getForegroundTile()
        {
            return foregroundTile;
        }
        
        public Tile getOverlayTile()
        {
            return overlayTile;
        }
    }
}