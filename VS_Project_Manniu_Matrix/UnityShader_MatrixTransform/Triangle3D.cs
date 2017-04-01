using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityShader_MatrixTransform
{
    class Triangle3D
    {
        //三角形原始顶点数据
        public Vector4 A, B, C;

        //三角形变换后顶点数据
        private Vector4 a, b, c;

        //三角形表面的法线，中间值
       // private Vector4 normal;

        //三角形表面顶点的光照强度
        private float lightDot;

        //是否剔除背面
        private bool IsCutoff = false;
        public Triangle3D()
        {
        }

        public Triangle3D(Vector4 a, Vector4 b, Vector4 c)
        {
            //此时为赋值方式为引用传值，当A ,B ,C的值变换时，a ,b ,c 的值也会相应变换
            //this.A = a;
            //this.B = b;
            //this.C = c;
            //此时为赋值方式为值传递
            this.A=this.a = new Vector4(a);
            this.B=this.b = new Vector4(b);
            this.C=this.c = new Vector4(c);
        }

        /// <summary>
        ///  将三角形三个顶点进行矩阵变换
        /// 
        /// 注意：顶点的矩阵变换是基于原始坐标顶点进行的变换
        /// 所以，多次进行相同状态的矩阵变换（如，model变换），最终变化结果正确
        /// </summary>
        /// <param name="m"></param>
        public void Transform(Matrix4x4 m)
        {
            this.a = m.Mul(this.A);
            this.b = m.Mul(this.B);
            this.c = m.Mul(this.C);
        }


        /// <summary>
        ///  /// 光照计算
        /// 
        /// 其中包括计算法向量
        /// 默认法向量指向屏幕外（左手坐标系）
        /// 需要的顶点是模型顶点在世界空间的位置信息
        ///  **************（因为需要和光源进行向量计算）******************
        ///  **************（注意：计算光照使用了左手坐标系）******************
        /// </summary>
        /// <param name="object2World">模型空间转到世界空间</param>
        /// <param name="L">光源世界空间位置</param>
        public void  CalculateLighting(Matrix4x4 object2World , Vector4 L)
        {
            //计算时现将模型顶点转到世界空间中
            this.Transform(object2World);

            Vector4 U = this.b - this.a;
            Vector4 V = this.c - this.a;
            //normal= U.Cross(V);//法向量为中间值，可以不缓存
            Vector4 normal = U.Cross(V);

            lightDot = normal.Normalized.Dot(L.Normalized);
            lightDot = Math.Max(0, lightDot);

            //视向量
            Vector4 eyeV = new Vector4(0,0,-1,0);
            IsCutoff = normal.Normalized.Dot(eyeV) < 0 ? true : false;
        }

        /// <summary>
        /// 绘制三角形到2D窗口上
        /// </summary>
        /// <param name="g"></param>
        public void DrawOnScreen(Graphics g,bool isLine)
        {
            /** g.TranslateTransform(300,300);**/  //通过平移改变坐标系原点
           // g.TranslateTransform(300,300);

            PointF[] points2d = this.Get2DPointF_Array();

            if (isLine)
            {
                //绘制边框
                g.DrawLines(new Pen(Color.Red, 2), points2d);
            }
            else
            {
                //是否进行背面剔除 
                if (!IsCutoff)
                {
                    //绘制表面
                    GraphicsPath path = new GraphicsPath();
                    path.AddLines(points2d);

                    int c = (int)(55 + 200 * lightDot);
                    Color bColor = Color.FromArgb(c, c, c);
                    Brush brush = new SolidBrush(bColor);

                    g.FillPath(brush, path);
                }
            }
           
        }

        /// <summary>
        /// 获取投影点的集合（注意：构成一个封闭的三角形需要4个顶点来画线）
        /// </summary>
        /// <returns></returns>
        private PointF[] Get2DPointF_Array()
        {
            PointF[] pArray = new PointF[4];
            pArray[0] = Get2DPointF(this.a);
            pArray[1] = Get2DPointF(this.b);
            pArray[2] = Get2DPointF(this.c);
            pArray[3] = pArray[0];
            return pArray;
        }

        /// <summary>
        /// 获取世界坐标在屏幕上的投影位置（输入值为经过矩阵变换后的顶点）
        /// </summary>
        /// <returns></returns>
        private PointF Get2DPointF(Vector4 v)
        {
            PointF p = new PointF();
            p.X = (float) (v.x / v.w);
            //...保证显示三角形顶点朝上
            p.Y =  - (float) (v.y / v.w);
            return p;
        }
    }
}
