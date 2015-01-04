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
        private static readonly Material MaterialForAuxiliaryGeometry = new Material { Diffuse = Color.Red };
        private static readonly object SyncRoot = new object();
        private const double RadInCyrcle = 2 * Math.PI;
        const double Rad2DegRatio = Math.PI / 180;

        /// <summary>
        /// Build a spur gear.
        /// </summary>
        /// <param name="spurGear">The spur gear to be built.</param>
        /// <param name="showAxiliaryGeometry">Show the auxiliary geometry.</param>
        public static void Build(SpurGear spurGear, bool showAxiliaryGeometry)
        {
            lock (SyncRoot)
            {
                spurGear.Faces.Clear();
                spurGear.Vertices.Clear();
                if (spurGear.PitchDiameter < 0
                    || spurGear.OutsideDiameter < 0
                    || spurGear.WorkingDepth < 0
                    || spurGear.WholeDepth < 0
                    || spurGear.ShaftDiameter < 0)
                {
                    return;
                }

                if (spurGear.UVs.Count == 0)
                    AddTextureCoordinates(spurGear);

                DrawSpurGear(spurGear, showAxiliaryGeometry);
            }
        }

        private static void DrawSpurGear(SpurGear spurGear, bool showAxiliaryGeometry)
        {
            DrawGearShaft(spurGear);
            DrawGearFace(spurGear);
            if (showAxiliaryGeometry)
                DrawAxiliaryGeometry(spurGear);
        }

        private static void DrawAxiliaryGeometry(SpurGear spurGear)
        {
            const float addendum = 0.1f;
            DrawCylinder(spurGear, spurGear.OutsideDiameter, addendum, MaterialForAuxiliaryGeometry);
            DrawCylinder(spurGear, spurGear.PitchDiameter, addendum, MaterialForAuxiliaryGeometry);
            DrawCylinder(spurGear, spurGear.OutsideDiameter - (spurGear.WorkingDepth*2), addendum, MaterialForAuxiliaryGeometry);
            DrawCylinder(spurGear, spurGear.OutsideDiameter - (spurGear.WholeDepth*2), addendum, MaterialForAuxiliaryGeometry);
        }

        private static void DrawGearShaft(SpurGear spurGear)
        {
            if (spurGear.ShaftDiameter <= 0) 
                return;
            DrawCylinder(spurGear, spurGear.ShaftDiameter/2, 0);
        }

        private static void DrawGearFace(SpurGear spurGear)
        {
            var initVerticesCount = spurGear.Vertices.Count;
            foreach (var pos in GetPositionsForPitches(spurGear, 10))
                AddFace(pos.X, pos.Y, spurGear.FaceWidth, spurGear, 0, initVerticesCount);
        }
        
        //     4__5<-------- OuterDiameter
        //     /  \
        //   3/    \6<------ Pitch Diameter
        //  ->|----|<------- Tooth Pitch
        // ___|    |___<---- Whole Depth; Root Radius
        // 1  2    7   8
        private static IEnumerable<Pos2D> GetPositionsForPitches(SpurGear spurGear, double segmentAngle)
        {
            const float centerX = 0;
            const float centerY = 0;
            var segmentAngleInRad = (float)(segmentAngle * Rad2DegRatio);
            var outsideRadius = spurGear.OutsideDiameter / 2 + centerX;
            var rootRadius = outsideRadius - spurGear.WholeDepth;
            var pitchRadius = spurGear.PitchDiameter / 2;
            var circularPitchAngle = (float)(RadInCyrcle / spurGear.NumberOfTeeth);
            var toothThicknessAngle = ((float)(RadInCyrcle * spurGear.ToothThickness) / (Math.PI * spurGear.PitchDiameter));
            var halfBottomLandPitch = ((circularPitchAngle - toothThicknessAngle) / 2f);
            /*1*/
            var startPos = new Pos2D(centerX, rootRadius + centerY);
            yield return startPos;
            float startAngle = 0;
        
            for (int i = 0; i < spurGear.NumberOfTeeth; i++)
            {
                /*1-2*/
                for (var angle = segmentAngleInRad; angle < halfBottomLandPitch; angle += segmentAngleInRad)
                    yield return Pos(rootRadius, startAngle + angle, centerX, centerY);
                /*2*/
                yield return Pos(rootRadius, startAngle + halfBottomLandPitch, centerX, centerY);
                /*3*/
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
                /*8*/
                for (var angle = startAngle + halfBottomLandPitch + toothThicknessAngle; angle < circularPitchAngle; angle += segmentAngleInRad)
                    yield return Pos(rootRadius, angle, centerX, centerY);

                startAngle += circularPitchAngle;
            }
            /*1*/
            yield return startPos;
        }

        private static Pos2D Pos(float rootRadius, double angle, float centerX, float centerY)
        {
            return new Pos2D((float)(rootRadius * Math.Sin(angle)) + centerX, (float)(rootRadius * Math.Cos(angle)) + centerY);
        }

        private static void DrawCylinder(SpurGear spurGear, float diameter, float addendum, Material material = null)
        {
            var initVerticesCount = spurGear.Vertices.Count;
            foreach (var pos in GetPositionsForGear(diameter / 2, 0, 0, 5))
                AddFace(pos.X, pos.Y, spurGear.FaceWidth, spurGear, addendum, initVerticesCount, material);
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