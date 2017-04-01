using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityShader_MatrixTransform
{
    class Cube
    {
        //构建点时，需要保证w = 1
        Vector4 a = new Vector4(-0.5, 0.5, 0.5, 1);
        Vector4 b = new Vector4(0.5,  0.5, 0.5, 1);
        Vector4 c = new Vector4(0.5,  0.5, -0.5,1);
        Vector4 d = new Vector4(-0.5, 0.5, -0.5,1);

        Vector4 e = new Vector4(-0.5, -0.5, 0.5, 1);
        Vector4 f = new Vector4(0.5, -0.5,  0.5, 1);
        Vector4 g = new Vector4(0.5, -0.5, -0.5, 1);
        Vector4 h = new Vector4(-0.5, -0.5,-0.5, 1);

        //立方体6个面共12个三角面
        Triangle3D[] triangles = new Triangle3D[12];
        public Cube() 
        {
            /**注意：构建三角面时，从顺时针方向构建，左手坐标系**/

            //top可见使用顺时针
            triangles[0] = new Triangle3D(a, b, c);
            triangles[1] = new Triangle3D(a, c, d);

            //bottom,不可见使用逆时针
            triangles[2] = new Triangle3D(e, h, f);
            triangles[3] = new Triangle3D(f, h, g);

            //front可见使用顺时针
            triangles[4] = new Triangle3D(d, c, g);
            triangles[5] = new Triangle3D(d, g, h);

            //back不可见使用逆时针
            triangles[6] = new Triangle3D(a, e, b);
            triangles[7] = new Triangle3D(b, e, f);

            //right可见使用顺时针
            triangles[8] = new Triangle3D(b, f, c);
            triangles[9] = new Triangle3D(c, f, g);

            //left不可见使用逆时针
            triangles[10] = new Triangle3D(a, d, h);
            triangles[11] = new Triangle3D(a, h, e);
        }

        public void Transform(Matrix4x4 m)
        {
            foreach (Triangle3D t in triangles)
            {
                t.Transform(m);
            }
        }

        public void CalculateLighting(Matrix4x4 object2World, Vector4 L)
        {
            foreach (Triangle3D t in triangles)
            {
                t.CalculateLighting(object2World,L);
            }
        }

        public void DrawOnScreen(Graphics g,bool isLine)
        {
            g.TranslateTransform(300, 300);
            foreach (Triangle3D t in triangles)
            {
                t.DrawOnScreen(g,isLine);
            }
        }

    }
}
