using System;
using System.Collections.Generic;
using System.Drawing;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Transformations;

namespace VirtualScene.Entities
{
    public static class GeometryEqualityHelper
    {
        internal static bool Equal(Polygon x, Polygon y)
        {
            if (x == null || y == null)
                return false;
            return TransformationEqual(x.Transformation, y.Transformation) 
                && VerticesEqual(x.Vertices, y.Vertices);
        }

        internal static bool VerticesEqual(IReadOnlyList<Vertex> x, IReadOnlyList<Vertex> y)
        {
            if (x == null || y == null || x.Count != y.Count)
                return false;
// ReSharper disable LoopCanBeConvertedToQuery
            for (var i = 0; i < x.Count; i++)
// ReSharper restore LoopCanBeConvertedToQuery
            {
                if (!FloatEqual(x[i].X, y[i].X) || !FloatEqual(x[i].Y, y[i].Y) || !FloatEqual(x[i].Z, y[i].Z))
                    return false;
            }
            return true;
        }

        internal static bool TransformationEqual(LinearTransformation x, LinearTransformation y)
        {
            return x.RotateX.Equals(y.RotateX) && x.RotateY.Equals(y.RotateY) && x.RotateZ.Equals(y.RotateZ)
                   && x.ScaleX.Equals(y.ScaleX) && x.ScaleY.Equals(y.ScaleY) && x.ScaleZ.Equals(y.ScaleZ)
                   && x.TranslateX.Equals(y.TranslateX) && x.TranslateY.Equals(y.TranslateY) && x.TranslateZ.Equals(y.TranslateZ)
                   && x.TransformationOrder.Equals(y.TransformationOrder);
        }

        internal static bool Equal(Cylinder x, Cylinder y)
        {
            if (x == null || y == null)
                return false;
            return TransformationEqual(x.Transformation, y.Transformation) 
                   && DoubleEqual(x.BaseRadius, y.BaseRadius) && DoubleEqual(x.Height, y.Height) && DoubleEqual(x.TopRadius, y.TopRadius);
        }

        internal static bool Equal(Disk x, Disk y)
        {
            if (x == null || y == null)
                return false;
            return TransformationEqual(x.Transformation, y.Transformation) 
                   && DoubleEqual(x.InnerRadius, y.InnerRadius) && DoubleEqual(x.OuterRadius, y.OuterRadius) && DoubleEqual(x.StartAngle, y.StartAngle)
                   && DoubleEqual(x.SweepAngle, y.SweepAngle) && x.Loops == y.Loops;
        }

        private static bool CamerasEqual(Camera x, Camera y)
        {
            if (x == null || y == null)
                return false;
            return x.Position.Equals(y.Position) && DoubleEqual(x.AspectRatio, y.AspectRatio);
        }

        internal static bool Equal(LookAtCamera x, LookAtCamera y)
        {
            return CamerasEqual(x, y) && x.Target.Equals(y.Target) && x.UpVector.Equals(y.UpVector);
        }

        internal static bool Equal(ArcBallCamera x, ArcBallCamera y)
        {
            return CamerasEqual(x, y);
        }

        internal static bool Equal(OrthographicCamera x, OrthographicCamera y)
        {
            return CamerasEqual(x, y) && x.Bottom.Equals(y.Bottom) && x.Top.Equals(y.Top)
                 && x.Left.Equals(y.Left) && x.Right.Equals(y.Right)
                 && x.Far.Equals(y.Far) && x.Near.Equals(y.Near);
        }

        internal static bool Equal(FrustumCamera x, FrustumCamera y)
        {
            return CamerasEqual(x, y) && x.Bottom.Equals(y.Bottom) && x.Top.Equals(y.Top)
                 && x.Left.Equals(y.Left) && x.Right.Equals(y.Right)
                 && x.Far.Equals(y.Far) && x.Near.Equals(y.Near);
        }

        internal static bool Equal(Light x, Light y)
        {
            if (x == null || y == null)
                return false;
            return x.Position.Equals(y.Position) && x.CastShadow.Equals(y.CastShadow) && x.On.Equals(y.On)
                 && ColorsEqual(x.Ambient, y.Ambient) && ColorsEqual(x.Diffuse, y.Diffuse) 
                 && ColorsEqual(x.Specular, y.Specular) && GLColorsEqual(x.ShadowColor, y.ShadowColor)
                 && x.GLCode.Equals(y.GLCode);
        }

        private static bool ColorsEqual(Color x, Color y)
        {
            return x.A.Equals(y.A) && x.B.Equals(y.B) && x.G.Equals(y.G) && x.R.Equals(y.R)
                   && x.Name.Equals(y.Name)
                   && FloatEqual(x.GetBrightness(), y.GetBrightness())
                   && FloatEqual(x.GetHue(), y.GetHue())
                   && FloatEqual(x.GetSaturation(), y.GetSaturation());
        }

        private static bool GLColorsEqual(GLColor x, GLColor y)
        {
            return x.A.Equals(y.A) && x.B.Equals(y.B) && x.G.Equals(y.G) && x.R.Equals(y.R)
                   && ColorsEqual(x.ColorNET, y.ColorNET);
        }

        internal static bool Equal(Sphere x, Sphere y)
        {
            if (x == null || y == null)
                return false;
            return TransformationEqual(x.Transformation, y.Transformation);
        }

        private static bool DoubleEqual(double x, double y)
        {
            return Math.Abs(x - y) < double.Epsilon;
        }

        private static bool FloatEqual(double x, double y)
        {
            return Math.Abs(x - y) < float.Epsilon;
        }

        public static bool SceneElementEqual(SceneElement x, SceneElement y)
        {

            if ((x == null && y != null) || (x != null && y == null))
                return false;
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (x == null && y == null)
                return true;
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
            return x.GetType() == y.GetType() 
                && x.Name == y.Name
                && (Equal(x as Polygon, y as Polygon)
                   || Equal(x as Cylinder, y as Cylinder)
                   || Equal(x as Disk, y as Disk)
                   || Equal(x as Sphere, y as Sphere)
                   || Equal(x as LookAtCamera, y as LookAtCamera)
                   || Equal(x as ArcBallCamera, y as ArcBallCamera)
                   || Equal(x as OrthographicCamera, y as OrthographicCamera)
                   || Equal(x as FrustumCamera, y as FrustumCamera)
                   || Equal(x as Light, y as Light));
        }
    }
}