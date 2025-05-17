using System;
using IziHardGames.EventSourcing.Contracts;
using IziHardGames.Geometry.Domain.Tilemap;
using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.UserControl.Domain.Events;

namespace IziHardGames.UserControl.GridSelector
{
    /// <summary>
    /// Horizontal plane by default.
    /// Y axis represents depth
    /// </summary>
    public class GridSelector : IConsumer<OnRaycastChangedEvent>
    {
        /// <summary>
        /// Normilized Up direction
        /// </summary>
        public Vector3RO PlaneXZUp01 { get; private set; } = new Vector3RO(0, 0, 1);
        public Vector3RO PlaneXZRight01 { get; private set; } = new Vector3RO(1, 0, 0);
        /// <summary>
        /// Forward
        /// </summary>
        public Vector3RO PlaneXZNormal { get => Vector3RO.Cross(PlaneXZUp01, PlaneXZRight01); }
        public Vector3RO Origin { get; private set; } = Vector3RO.Zero;
        public Vector3RO CellSizeXZ { get; private set; } = new Vector3RO(1f, 0, 1f);
        public TilePosition TilePosition { get; private set; }
        public Vector3RO HitOnPlane { get => hitOnPlane; }
        public Vector3RO HitCellCenter { get; private set; } = Vector3RO.NaN;
        public Vector3RO HitOnPlaneProjected { get => hitOnPlaneProjected; }
        public Vector3RO ProjectionToCam { get => projectionToCam; }
        public bool IsCellChanged { get; private set; }
        public float AngleDegree => angleDegree;
        public float AngleRad => angleRad;
        public float Cosine => cosine;

        private Vector3RO hitOnPlane = Vector3RO.NaN;
        private Vector3RO projectionToCam = Vector3RO.NaN;
        private Vector3RO hitOnPlaneProjected = Vector3RO.NaN;
        private float angleDegree;
        private float angleRad;
        private float cosine;

        public void Consume(OnRaycastChangedEvent e)
        {
            var toCam = e.PositionOfCamera - Origin;
            var projection = Vector3RO.Project(toCam, PlaneXZNormal);
            this.projectionToCam = projection;
            var projectionReversed = (projection * (-1));
            this.hitOnPlaneProjected = projectionReversed + e.PositionOfCamera;
            this.angleDegree = Vector3RO.Angle(projectionReversed, e.Ray);
            this.angleRad = angleDegree * Mathf.Deg2Rad;
            this.cosine = MathF.Cos(angleRad);
            this.hitOnPlane = e.RayNormilized * (projectionReversed.Distance / cosine) + e.PositionOfCamera;
            var toHit = hitOnPlane - Origin;
            var xInt = (int)Math.Floor((toHit.x / CellSizeXZ.x));
            var zInt = (int)Math.Floor((toHit.z / CellSizeXZ.z));
            var tp = new TilePosition(xInt, 0, zInt);
            IsCellChanged = tp != TilePosition;
            this.TilePosition = tp;
            this.HitCellCenter = PlaneXZUp01 * TilePosition.z + PlaneXZRight01 * TilePosition.x + CellSizeXZ * 0.5f;
        }
    }
}
