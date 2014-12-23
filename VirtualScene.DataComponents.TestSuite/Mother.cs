using System.Drawing;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Transformations;
using VirtualScene.BusinessComponents.Core.Entities;
using VirtualScene.BusinessComponents.Core.Factories;
using VirtualScene.Entities;
using VirtualScene.UnitTesting.Common;

namespace VirtualScene.DataComponents.TestSuite
{
    internal class Mother
    {
        public static Polygon CreateCube()
        {
            //The class Polygon is used instead of the class Cube - read the comment in the CreateCube factory method.
            var cube = GeometryPrimitiveFactory.CreateCube();
            cube.Name = Helper.GetUniqueName();
            cube.Transformation = new LinearTransformation { TranslateX = 2, TranslateY = 3, TranslateZ = 4 };
            return cube;
        }

        public static Sphere CreateSphere()
        {
            return new Sphere { Name = Helper.GetUniqueName(), Radius = 1.2 };
        }

        public static TestEntity CreateTestEntity()
        {
            return new TestEntity {Id = Helper.GetUniqueInt(), Name = Helper.GetUniqueName()};
        }

        public static ISceneEntity CreateSceneEntity(SceneElement sceneElement)
        {
            return new SceneEntity {Name = Helper.GetUniqueName(), Geometry = sceneElement};
        }

        public static Disk CreateDisk()
        {
            return new Disk
                {
                    Name = Helper.GetUniqueName(),
                    Loops = 2,
                    InnerRadius = 1.2,
                    OuterRadius = 3.4,
                    StartAngle = 10.2,
                    SweepAngle = 20.3
                };
        }

        public static Cylinder CreateCylinder()
        {
            return new Cylinder {Name = Helper.GetUniqueName(), TopRadius = 10.2, BaseRadius = 10.2, Height = 11.3};
        }

        public static Light CreateLight()
        {
            return new Light
                {
                    Name = Helper.GetUniqueName(),
                    Ambient = Color.FromName("Green"),
                    CastShadow = true,
                    Diffuse = Color.Yellow,
                    GLCode = 1,
                    On = true,
                    Position = new Vertex(1, 2, 3),
                    ShadowColor = new GLColor(1.1f, 2.2f, 3.3f, 4.4f)
                };
        }
    }
}