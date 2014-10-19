using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using VirtualScene.BusinessComponents.Core.Properties;

namespace VirtualScene.BusinessComponents.Core.Importers
{
    /// <summary>
    /// The importer of geometry from 3D files with Wavefront format
    /// </summary>
    public class WavefrontFormatImporter : IGeometryImporter
    {
        private const char SpaceChar = ' ';

        private static Color ReadMaterialColor(string line, float alpha)
        {
            var lineParts = line.Split(new[] {SpaceChar}, StringSplitOptions.RemoveEmptyEntries);
            if (lineParts.Length < 4) 
                return Color.White;
            // Convert float a,r,g,b values to byte values.  Make sure they fall in 0-255 range.
            var a = ConvertToColor(alpha);
            var r = ConvertToColor(lineParts[1]);
            var g = ConvertToColor(lineParts[2]);
            var b = ConvertToColor(lineParts[3]);
            return Color.FromArgb(a, r, g, b);
        }

        private static int ConvertToColor(string value)
        {
            return ConvertToColor(Convert.ToSingle(value));
        }

        private static int ConvertToColor(float value)
        {
            var color = Convert.ToInt32(255 * value);
            if (color < 0)
                return 0;
            return color > 255 ? 255 : color;
        }

        private static void SetAlphaForMaterial(Material material, float alpha)
        {
            int a = Convert.ToInt32(255*alpha);
            material.Ambient = Color.FromArgb(a, material.Ambient);
            material.Diffuse = Color.FromArgb(a, material.Diffuse);
            material.Specular = Color.FromArgb(a, material.Specular);
            material.Emission = Color.FromArgb(a, material.Emission);
        }

        private static string ReadMaterialValue(string line)
        {
            //  The material is everything after the first space.
            int spacePos = line.IndexOf(SpaceChar);
            if (spacePos == -1 || (spacePos + 1) >= line.Length)
                return null;

            //  Return the material path.
            return line.Substring(spacePos + 1);
        }

        private static void LoadMaterials(string path, ISceneEngine sceneEngine)
        {
            //  Create a stream reader.
            using (var reader = new StreamReader(path))
            {
                Material mtl = null;
                float alpha = 1;

                //  Read line by line.
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    //  Skip any comments (lines that start with '#').
                    if (line.StartsWith("#"))
                        continue;

                    // newmatl indicates start of material definition.
                    if (line.StartsWith("newmtl"))
                    {
                        // Add new material to scene's assets.
                        mtl = new Material();
                        sceneEngine.AddAsset(mtl);

                        // Name of material is on same line, immediately follows newmatl.
                        mtl.Name = ReadMaterialValue(line);

                        // Reset assumed alpha.
                        alpha = 1;
                    }

                    // Read properties of material.
                    if (mtl != null)
                    {
                        if (line.StartsWith("Ka"))
                            mtl.Ambient = ReadMaterialColor(line, alpha);
                        else if (line.StartsWith("Kd"))
                            mtl.Diffuse = ReadMaterialColor(line, alpha);
                        else if (line.StartsWith("Ks"))
                            mtl.Specular = ReadMaterialColor(line, alpha);
                        else if (line.StartsWith("Ns"))
                            mtl.Shininess = Convert.ToSingle(ReadMaterialValue(line));
                        else if (line.StartsWith("map_Ka") ||
                                 line.StartsWith("map_Kd") ||
                                 line.StartsWith("map_Ks"))
                        {
                            // Get texture map.                    		
                            string textureFile = ReadMaterialValue(line);
                            // Set texture for material.
                            mtl.Texture = sceneEngine.LoadOrCreateTexture(path, textureFile);
                        }
                        else if (line.StartsWith("d") || line.StartsWith("Tr"))
                        {
                            alpha = Convert.ToSingle(ReadMaterialValue(line));
                            SetAlphaForMaterial(mtl, alpha);
                        }
                        // TODO: Handle illumination mode (illum)                    	                    
                    }
                }
            }
        }

        /// <summary>
        /// Load of 3D geometry from the file
        /// </summary>
        /// <param name="fullFileName">The path to the file with 3D geometry</param>
        /// <param name="sceneEngine">The engine of the scene</param>
        /// <returns></returns>
        public ActionResult<SceneElement> LoadGeometry(string fullFileName, ISceneEngine sceneEngine)
        {
            var polygon = new Polygon();
            
            var actionResult = new ActionResult<SceneElement>("Import Wavefront format geometry") {Value = polygon};

            if (!File.Exists(fullFileName))
            {
                actionResult.AddError(Resources.Message_LoadGeometry_File_N_not_found, fullFileName);
                return actionResult;
            }
            try
            {
                LoadGeometryFromFile(fullFileName, sceneEngine, polygon, actionResult);
            }
            catch (Exception e)
            {
                actionResult.AddError(e);
            }
            return actionResult;
        }

        private static void LoadGeometryFromFile(string fullFileName, ISceneEngine sceneEngine, Polygon polygon, ActionResult<SceneElement> actionResult)
        {
            var split = new[] {' '};
            string mtlName = null;
            using (var reader = new StreamReader(fullFileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //  Skip any comments (lines that start with '#').
                    if (line.StartsWith("#"))
                        continue;

                    if (line.StartsWith("vt")) //texture coordinate
                    {
                        AddTextureCoordinate(polygon, line, split);
                        continue;
                    }

                    if (line.StartsWith("vn")) //normal coordinate
                    {
                        AddNormal(polygon, line, split);
                        continue;
                    }

                    if (line.StartsWith("v")) //vertex
                    {
                        AddVertices(polygon, line, split);
                        continue;
                    }

                    if (line.StartsWith("f")) //Face
                    {
                        AddFace(sceneEngine, polygon, mtlName, line, split);
                        continue;
                    }

                    AddMaterials(fullFileName, sceneEngine, line, ref mtlName, actionResult);
                }
            }
        }

        private static void AddMaterials(string fullFileName, ISceneEngine sceneEngine, string line, ref string mtlName, ActionResult<SceneElement> actionResult)
        {
            var currentDirectory = Environment.CurrentDirectory;
            try
            {
                var fileInfo = new FileInfo(fullFileName);
                if (line.StartsWith("mtllib") && fullFileName != null && fileInfo.Exists && fileInfo.Directory != null)
                {
                    // Set current directory in case a relative path to material file is used.
                    // ReSharper disable once AssignNullToNotNullAttribute
                    Environment.CurrentDirectory = fileInfo.Directory.FullName;

                    // Load materials file.
                    var mtlPath = ReadMaterialValue(line);
                    if (!File.Exists(mtlPath))
                    {
                        actionResult.AddWarning(Resources.Message_AddMaterials_File_with_material_N_not_found, mtlPath);
                        return;
                    }
                    LoadMaterials(mtlPath, sceneEngine);
                }

                if (line.StartsWith("usemtl"))
                    mtlName = ReadMaterialValue(line);
            }
            finally
            {
                Environment.CurrentDirectory = currentDirectory;
            }
        }

        private static void AddFace(ISceneEngine sceneEngine, Polygon polygon, string mtlName, string line, char[] split)
        {
            var face = new Face();

            if (!string.IsNullOrWhiteSpace(mtlName))
                face.Material = sceneEngine.GetAssets<Material>().FirstOrDefault(t => t.Name == mtlName);

            //  Get the face indices
            string[] indices = line.Substring(2).Split(split, StringSplitOptions.RemoveEmptyEntries);

            //  Add each index.
            AddIndexesToFace(indices, face);

            //  Add the face.
            polygon.Faces.Add(face);
        }

        private static void AddVertices(Polygon polygon, string line, char[] split)
        {
//  Get the vertex coord strings.
            string[] values = line.Substring(2).Split(split, StringSplitOptions.RemoveEmptyEntries);

            //  Parse vertex coordinates.
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);
            float z = float.Parse(values[2]);

            //   Add the vertices.
            polygon.Vertices.Add(new Vertex(x, y, z));
        }

        private static void AddNormal(Polygon polygon, string line, char[] split)
        {
//  Get the normal coord strings.
            string[] values = line.Substring(3).Split(split, StringSplitOptions.RemoveEmptyEntries);

            //  Parse normal coordinates.
            float x = float.Parse(values[0]);
            float y = float.Parse(values[1]);
            float z = float.Parse(values[2]);

            //  Add the normal.
            polygon.Normals.Add(new Vertex(x, y, z));
        }

        private static void AddTextureCoordinate(Polygon polygon, string line, char[] split)
        {
//  Get the texture coord strings.
            string[] values = line.Substring(3).Split(split, StringSplitOptions.RemoveEmptyEntries);

            //  Parse texture coordinates.
            float u = float.Parse(values[0]);
            float v = float.Parse(values[1]);

            //  Add the texture coordinate.
            polygon.UVs.Add(new UV(u, v));
        }

        private static void AddIndexesToFace(IEnumerable<string> indices, Face face)
        {
            foreach (var parts in indices.Select(index => index.Split(new[] {'/'}, StringSplitOptions.None)))
            {
                //  Add each part.
                face.Indices.Add(new Index(
                    (parts.Length > 0 && parts[0].Length > 0) ? int.Parse(parts[0]) - 1 : -1,
                    (parts.Length > 1 && parts[1].Length > 0) ? int.Parse(parts[1]) - 1 : -1,
                    (parts.Length > 2 && parts[2].Length > 0) ? int.Parse(parts[2]) - 1 : -1));
            }
        }
    }
}
