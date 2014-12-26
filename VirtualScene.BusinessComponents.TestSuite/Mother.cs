using System;
using System.IO;
using Moq;
using VirtualScene.Entities;

namespace VirtualScene.BusinessComponents.TestSuite
{
    public class Mother
    {
        public static string CreateWavefrontFormatFile()
        {
            var fileName = Guid.NewGuid().ToString();
            var wavefrontFormatFileName = fileName + ".obj";
            var materialsFileName = fileName + ".mtl";
            using (var stream = new StreamWriter(wavefrontFormatFileName))
            {
                stream.WriteLine(@"# test geometry - cube");
                stream.WriteLine(@"mtllib " + materialsFileName);
                stream.Write(@"
o Cube
v 1.000000 -1.000000 -1.000000
v 1.000000 -1.000000 1.000000
v -1.000000 -1.000000 1.000000
v -1.000000 -1.000000 -1.000000
v 1.000000 1.000000 -0.999999
v 0.999999 1.000000 1.000001
v -1.000000 1.000000 1.000000
v -1.000000 1.000000 -1.000000
usemtl Material
s off
f 1 2 3
f 5 8 6
f 1 5 2
f 2 6 3
f 3 7 8
f 5 1 8
f 4 1 3
f 5 6 2
f 1 4 8
f 8 7 6
f 4 3 8
f 6 7 3
");
            }
            return wavefrontFormatFileName;
        }

        public static void CreateMaterialsFile(string geometryFileName)
        {
            var materialsFileName = Path.GetFileNameWithoutExtension(geometryFileName) + ".mtl";
            using (var stream = new StreamWriter(materialsFileName))
            {
                stream.WriteLine(@"# test materials");
                stream.Write(@"# Material Count: 1
newmtl Material
Ns 96.078431
Ka 0.000000 0.000000 0.000000
Kd 0.640000 0.640000 0.640000
Ks 0.500000 0.500000 0.500000
Ni 1.000000
d 1.000000
illum 2
");
            }            
        }
    }
}