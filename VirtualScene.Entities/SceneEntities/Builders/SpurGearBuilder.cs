using System;
using System.Collections.Generic;
using System.Drawing;
using SharpGL.SceneGraph.Assets;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.Entities.SceneEntities.Builders
{
    /// <summary>
    /// The factory building the spur gear geometry.
    /// </summary>
    public class SpurGearBuilder : SceneElementBuilderBase
    {
        private readonly Material _materialForAuxiliaryGeometry = new Material { Diffuse = Color.Red };
        private readonly object _syncRoot = new object();
        private readonly float _segmentAngleInRad;
        private const double RadInCyrcle = 2 * Math.PI;
        const float Rad2DegRatio = (float) (Math.PI / 180);

        /// <summary>
        /// Initializes a new instance of the <see cref="SpurGearBuilder" />
        /// </summary>
        /// <param name="segmentAngle">The angle of the segment (in degrees)</param>
        public SpurGearBuilder(float segmentAngle)
        {
            _segmentAngleInRad = segmentAngle * Rad2DegRatio;
        }

        /// <summary>
        /// The spur gear to be built.
        /// </summary>
        public SpurGear SpurGear { private get; set; }

        /// <summary>
        /// Build a spur gear.
        /// </summary>
        /// <param name="centerX">The X of the center.</param>
        /// <param name="centerY">The Y of the center.</param>
        /// <param name="showAxiliaryGeometry">Show the auxiliary geometry.</param>
        public void Build(float centerX, float centerY, bool showAxiliaryGeometry)
        {
            lock (_syncRoot)
            {
                SpurGear.Faces.Clear();
                SpurGear.Vertices.Clear();
                if (SpurGear.PitchDiameter < 0
                    || SpurGear.OutsideDiameter < 0
                    || SpurGear.WorkingDepth < 0
                    || SpurGear.WholeDepth < 0
                    || SpurGear.ShaftDiameter < 0)
                {
                    return;
                }

                if (SpurGear.UVs.Count == 0)
                    AddTextureCoordinates(SpurGear);

                DrawSpurGear(showAxiliaryGeometry, centerX, centerY);
            }
        }

        private void DrawSpurGear(bool showAxiliaryGeometry, float centerX, float centerY)
        {
            DrawGearShaft(centerX, centerY);
            DrawGearFace(centerX, centerY);
            if (showAxiliaryGeometry)
                DrawAxiliaryGeometry(centerX, centerY);
        }

        private void DrawAxiliaryGeometry(float centerX, float centerY)
        {
            const float addendum = 0.1f;
            DrawCylinder(SpurGear.OutsideDiameter, addendum, _materialForAuxiliaryGeometry, centerX, centerY);
            DrawCylinder(SpurGear.PitchDiameter, addendum, _materialForAuxiliaryGeometry, centerX, centerY);
            DrawCylinder(SpurGear.OutsideDiameter - (SpurGear.WorkingDepth * 2), addendum, _materialForAuxiliaryGeometry, centerX, centerY);
            DrawCylinder(SpurGear.OutsideDiameter - (SpurGear.WholeDepth * 2), addendum, _materialForAuxiliaryGeometry, centerX, centerY);
        }

        private void DrawGearShaft(float centerX, float centerY)
        {
            if (SpurGear.ShaftDiameter <= 0) 
                return;
            DrawCylinder(SpurGear.ShaftDiameter/2, 0, null, centerX, centerY);
        }

        private void DrawGearFace(float centerX, float centerY)
        {
            var initVerticesCount = SpurGear.Vertices.Count;
            foreach (var pos in GetPositionsForPitches(centerX, centerY))
                AddFace(pos.X, pos.Y, SpurGear.FaceWidth, SpurGear, 0, initVerticesCount);
        }
        
        //     4__5<-------- OuterDiameter
        //     /  \
        //   3/    \6<------ Pitch Diameter
        //  ->|----|<------- Tooth Pitch
        // ___|    |___<---- Whole Depth; Root Radius
        // 1  2    7   8
        private IEnumerable<Pos2D> GetPositionsForPitches(float centerX, float centerY)
        {
            var outsideRadius = SpurGear.OutsideDiameter / 2 + centerX;
            var rootRadius = outsideRadius - SpurGear.WholeDepth;
            var pitchRadius = SpurGear.PitchDiameter / 2;
            var circularPitchAngle = (float)(RadInCyrcle / SpurGear.NumberOfTeeth);
            var toothThicknessAngle = ((float)(RadInCyrcle * SpurGear.ToothThickness) / (Math.PI * SpurGear.PitchDiameter));
            var halfBottomLandPitch = ((circularPitchAngle - toothThicknessAngle) / 2f);
            /*1*/
            var startPos = new Pos2D(centerX, rootRadius + centerY);
            yield return startPos;
            float startAngle = 0;
        
            for (var i = 0; i < SpurGear.NumberOfTeeth; i++)
            {
                /*1-2 draw first half base*/
                for (var angle = _segmentAngleInRad; angle < halfBottomLandPitch; angle += _segmentAngleInRad)
                    yield return Pos(rootRadius, startAngle + angle, centerX, centerY);
                /*2*/
                yield return Pos(rootRadius, startAngle + halfBottomLandPitch, centerX, centerY);
                /*3 - draw tooth*/
                yield return Pos(pitchRadius, startAngle + halfBottomLandPitch, centerX, centerY);
                /*4*/
                var q = toothThicknessAngle / 4;
                yield return Pos(outsideRadius, startAngle + halfBottomLandPitch + q, centerX, centerY);
                /*5*/
                yield return Pos(outsideRadius, startAngle + halfBottomLandPitch + toothThicknessAngle - q, centerX, centerY);
                /*6*/
                yield return Pos(pitchRadius, startAngle + halfBottomLandPitch + toothThicknessAngle, centerX, centerY);
                /*7*/
                yield return Pos(rootRadius, startAngle + halfBottomLandPitch + toothThicknessAngle, centerX, centerY);
                /*8* draw second half base*/
                for (var angle = startAngle + halfBottomLandPitch + toothThicknessAngle; angle < circularPitchAngle; angle += _segmentAngleInRad)
                    yield return Pos(rootRadius, angle, centerX, centerY);

                startAngle += circularPitchAngle;
            }
            /*1 draw last half base*/
            for (var angle = _segmentAngleInRad; angle < halfBottomLandPitch; angle += _segmentAngleInRad)
                yield return Pos(rootRadius, startAngle + angle, centerX, centerY);
        }

        private static Pos2D Pos(float rootRadius, double angle, float centerX, float centerY)
        {
            return new Pos2D((float)(rootRadius * Math.Sin(angle)) + centerX, (float)(rootRadius * Math.Cos(angle)) + centerY);
        }

        private void DrawCylinder(float diameter, float addendum, Material material, float centerX, float centerY)
        {
            var initVerticesCount = SpurGear.Vertices.Count;
            foreach (var pos in GetPositionsForGear(diameter / 2, 0, 0, 5))
                AddFace(pos.X + centerX, pos.Y + centerY, SpurGear.FaceWidth, SpurGear, addendum, initVerticesCount, material);
        }

        private static IEnumerable<Pos2D> GetPositionsForGear(float radius, float centerX, float centerY, int segmentAngle)
        {
            var startPosition = new Pos2D(0 + centerX, radius + centerY);
            yield return startPosition;
            for (var angle = segmentAngle; angle < 360 + segmentAngle; angle += segmentAngle)
            {
                var rad = angle*Rad2DegRatio;
                yield return new Pos2D((float) (radius*Math.Sin(rad)) + centerX, (float) (radius*Math.Cos(rad)) + centerY);
            }
            yield return startPosition;
        }
    }
}