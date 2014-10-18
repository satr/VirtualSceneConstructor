using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Primitives;

namespace VirtualScene.BusinessComponents.Core.Importers
{
    internal class WavefrontFormatImporter
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

        public void LoadDataToScene(string fullFileName, ISceneEngine sceneEngine)
        {
            var split = new[] {' '};

            //  Create a scene and polygon.
            var polygon = new Polygon();

            string mtlName = null;

            //  Create a stream reader.
            using (var reader = new StreamReader(fullFileName))
            {
                //  Read line by line.
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //  Skip any comments (lines that start with '#').
                    if (line.StartsWith("#"))
                        continue;

                    //  Do we have a texture coordinate?
                    if (line.StartsWith("vt"))
                    {
                        //  Get the texture coord strings.
                        string[] values = line.Substring(3).Split(split, StringSplitOptions.RemoveEmptyEntries);

                        //  Parse texture coordinates.
                        float u = float.Parse(values[0]);
                        float v = float.Parse(values[1]);

                        //  Add the texture coordinate.
                        polygon.UVs.Add(new UV(u, v));

                        continue;
                    }

                    //  Do we have a normal coordinate?
                    if (line.StartsWith("vn"))
                    {
                        //  Get the normal coord strings.
                        string[] values = line.Substring(3).Split(split, StringSplitOptions.RemoveEmptyEntries);

                        //  Parse normal coordinates.
                        float x = float.Parse(values[0]);
                        float y = float.Parse(values[1]);
                        float z = float.Parse(values[2]);

                        //  Add the normal.
                        polygon.Normals.Add(new Vertex(x, y, z));

                        continue;
                    }

                    //  Do we have a vertex?
                    if (line.StartsWith("v"))
                    {
                        //  Get the vertex coord strings.
                        string[] values = line.Substring(2).Split(split, StringSplitOptions.RemoveEmptyEntries);

                        //  Parse vertex coordinates.
                        float x = float.Parse(values[0]);
                        float y = float.Parse(values[1]);
                        float z = float.Parse(values[2]);

                        //   Add the vertices.
                        polygon.Vertices.Add(new Vertex(x, y, z));

                        continue;
                    }

                    //  Do we have a face?
                    if (line.StartsWith("f"))
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

                        continue;
                    }

                    var currentDirectory = Environment.CurrentDirectory;
                    try
                    {
                        if (line.StartsWith("mtllib") && fullFileName != null && File.Exists(fullFileName))
                        {
                            // Set current directory in case a relative path to material file is used.
                            // ReSharper disable once AssignNullToNotNullAttribute
                            Environment.CurrentDirectory = Path.GetDirectoryName(fullFileName);

                            // Load materials file.
                            var mtlPath = ReadMaterialValue(line);
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
            }

            sceneEngine.AddSceneElement(polygon);
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

        public bool SaveData(Scene scene, string path)
        {
            throw new NotImplementedException("The SaveData method has not been implemented for .obj files.");
            //return SaveData(scene, scene.SceneContainer, path);
        }
    }
}
