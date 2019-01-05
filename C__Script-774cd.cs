

using Rhino;
using Rhino.Geometry;
using Rhino.DocObjects;
using Rhino.Collections;

using GH_IO;
using GH_IO.Serialization;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// Unique namespace, so visual studio won't throw any errors about duplicate definitions.
namespace ns774cd
{
    /// <summary>
    /// This class will be instantiated on demand by the Script component.
    /// </summary>
    public class Script_Instance : GH_ScriptInstance
    {
	    /// This method is added to prevent compiler errors when opening this file in visual studio (code) or rider.
	    public override void InvokeRunScript(IGH_Component owner, object rhinoDocument, int iteration, List<object> inputs, IGH_DataAccess DA)
        {
            throw new NotImplementedException();
        }

        #region Utility functions
        /// <summary>Print a String to the [Out] Parameter of the Script component.</summary>
        /// <param name="text">String to print.</param>
        private void Print(string text) { /* Implementation hidden. */ }
        /// <summary>Print a formatted String to the [Out] Parameter of the Script component.</summary>
        /// <param name="format">String format.</param>
        /// <param name="args">Formatting parameters.</param>
        private void Print(string format, params object[] args) { /* Implementation hidden. */ }
        /// <summary>Print useful information about an object instance to the [Out] Parameter of the Script component. </summary>
        /// <param name="obj">Object instance to parse.</param>
        private void Reflect(object obj) { /* Implementation hidden. */ }
        /// <summary>Print the signatures of all the overloads of a specific method to the [Out] Parameter of the Script component. </summary>
        /// <param name="obj">Object instance to parse.</param>
        private void Reflect(object obj, string method_name) { /* Implementation hidden. */ }
        #endregion
        #region Members
        /// <summary>Gets the current Rhino document.</summary>
        private readonly RhinoDoc RhinoDocument;
        /// <summary>Gets the Grasshopper document that owns this script.</summary>
        private readonly GH_Document GrasshopperDocument;
        /// <summary>Gets the Grasshopper script component that owns this script.</summary>
        private readonly IGH_Component Component;
        /// <summary>
        /// Gets the current iteration count. The first call to RunScript() is associated with Iteration==0.
        /// Any subsequent call within the same solution will increment the Iteration count.
        /// </summary>
        private readonly int Iteration;
        #endregion
        /// <summary>
        /// This procedure contains the user code. Input parameters are provided as regular arguments,
        /// Output parameters as ref arguments. You don't have to assign output parameters,
        /// they will have a default value.
        /// </summary>
        #region Runscript
        private void RunScript(Mesh meshInput, object x, object y, ref object A)
        {

            string floatVertexArrayPath = "M:/vertexFloat.bin";
            string intFaceArrayPath = "M:/vertexInt.bin";

            float[] floatVertexArray = new float[meshInput.Vertices.Count * 3];
            int[] intFaceArray = new int[meshInput.Faces.Count * 3];

            for (int i = 0; i < meshInput.Vertices.Count; i++)
            {
                floatVertexArray[i * 3 + 0] = (float) meshInput.Vertices[i].X;
                floatVertexArray[i * 3 + 1] = (float) meshInput.Vertices[i].Y;
                floatVertexArray[i * 3 + 2] = (float) meshInput.Vertices[i].Z;
            }

            // Debug Print
            /*
            for(int i=0; i < floatVertexArray.Length; i++){
            Print(floatVertexArray[i].ToString());
            }
            */
            for (int i = 0; i < meshInput.Faces.Count; i++)
            {
                intFaceArray[i * 3 + 0] = (int) meshInput.Faces[i][0];
                intFaceArray[i * 3 + 1] = (int) meshInput.Faces[i][1];
                intFaceArray[i * 3 + 2] = (int) meshInput.Faces[i][2];
            }
            //Debug Print
            /*
            for(int i=0; i < intFaceArray.Length; i++){
            Print(intFaceArray[i].ToString());
            }
            */

            WriteFloatArraytoBin(floatVertexArray, floatVertexArrayPath);
            WriteIntArraytoBin(intFaceArray, intFaceArrayPath);

  }

  public void WriteFloatArraytoBin(float[] floatArray, string binPath)
  {
            using(System.IO.FileStream floatArrayFile = System.IO.File.Create(binPath,1024))
            {
                using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(floatArrayFile))
                {
                    foreach ( float value in floatArray)
                    {
                        writer.Write((float) value);
                    }
                    writer.Close();
                }
            }
  }

  public void WriteIntArraytoBin(int[] intArray, string binPath)
  {
            using(System.IO.FileStream intArrayFile = System.IO.File.Create(binPath,1024))
            {
                using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(intArrayFile))
                {
                    foreach ( int value in intArray)
                    {
                        writer.Write((int) value);
                    }
                    writer.Close();
                }
            }
        }
        #endregion

        #region Additional

        #endregion
    }
}
