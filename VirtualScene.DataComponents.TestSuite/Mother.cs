using System.Drawing;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Quadrics;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.UnitTesting.Common;

namespace VirtualScene.DataComponents.TestSuite
{
    internal class Mother
    {
        public static SphereEntity CreateSphereEntity()
        {
            var entity = new SphereEntity { Name = Helper.GetUniqueName(), Radius = 1.2f };
            SetTransformation((IHasObjectSpace)entity.Geometry, 2, 3, 4);
            return entity;
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

        public static ISceneEntity CreateCubeEntity()
        {
            return new CubeEntity{Name = Helper.GetUniqueName()};
        }

        public static DiskEntity CreateDiskEntity()
        {
            var entity = new DiskEntity
            {
                Name = Helper.GetUniqueName()
            };
            var disk = ((Disk) entity.Geometry);
            disk.Loops = 2;
            disk.InnerRadius = 1.2;
            disk.OuterRadius = 3.4;
            disk.StartAngle = 10.2;
            disk.SweepAngle = 20.3;
            SetTransformation(((IHasObjectSpace) entity.Geometry), 2, 3, 4);
            return entity;
        }

        public static CustomEntity CreateCustomEntityWithCylinder()
        {
            var entity = new CustomEntity
            {
                Name = Helper.GetUniqueName(),
                Geometry = new Cylinder {TopRadius = 10.2, BaseRadius = 10.2, Height = 11.3}
            };
            SetTransformation(((IHasObjectSpace) entity.Geometry), 2, 3, 4);
            return entity;
        }

        public static Light CreateLight()//TODO rework to LightEntity
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