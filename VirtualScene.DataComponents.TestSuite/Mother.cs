using System.Drawing;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Quadrics;
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
            SetTransformation(cube, 2, 3 ,4);
            return cube;
        }

        public static Sphere CreateSphere()
        {
            var sphere = new Sphere { Name = Helper.GetUniqueName(), Radius = 1.2 };
            SetTransformation(sphere, 2, 3, 4);
            return sphere;
        }

        private static void SetTransformation(IHasObjectSpace objectSpace, int translateX, int translateY, int translateZ)
        {
            objectSpace.Transformation.TranslateX = translateX;
            objectSpace.Transformation.TranslateY = translateY;
            objectSpace.Transformation.TranslateZ = translateZ;
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
            var disk = new Disk
            {
                Name = Helper.GetUniqueName(),
                Loops = 2,
                InnerRadius = 1.2,
                OuterRadius = 3.4,
                StartAngle = 10.2,
                SweepAngle = 20.3
            };
            SetTransformation(disk, 2, 3, 4);
            return disk;
        }

        public static Cylinder CreateCylinder()
        {
            var cylinder = new Cylinder {Name = Helper.GetUniqueName(), TopRadius = 10.2, BaseRadius = 10.2, Height = 11.3};
            SetTransformation(cylinder, 2, 3, 4);
            return cylinder;
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