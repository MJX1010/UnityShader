using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityShader_MatrixTransform
{
    class Matrix4x4
    {
        private double[,] pts;

        public Matrix4x4()
        {
            pts = new double[4, 4];
        }

        /// <summary>
        /// 定义 索引器
        /// </summary>
        /// <param name="i"> row(从1开始) </param>
        /// <param name="j"> column(从1开始) </param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get { return pts[i -1, j - 1]; }
            set { pts[i - 1, j - 1] = value; }
        }

        /// <summary>
        /// 矩阵乘法（将当前矩阵按给定矩阵进行变换）
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public Matrix4x4 Mul(Matrix4x4 m)
        {
            Matrix4x4 newM = new Matrix4x4();

            for (int r = 1; r <= 4; r++)
                for (int c = 1; c <= 4; c++)
                    for (int n = 1; n <= 4; n++)
                    {
                        newM[r, c] += this[r, n] * m[n, c];//使用了索引器
                    }

            return newM;
        }

        /// <summary>
        /// 4维向量与4维矩阵的乘法（将4维向量按当前矩阵进行变换）
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector4 Mul(Vector4 v)
        {
            Vector4 newV = new Vector4();

            newV.x = v.x * this[1, 1] + v.y * this[2, 1] + v.z * this[3, 1] + v.w * this[4, 1];
            newV.y = v.x * this[1, 2] + v.y * this[2, 2] + v.z * this[3, 2] + v.w * this[4, 2];
            newV.z = v.x * this[1, 3] + v.y * this[2, 3] + v.z * this[3, 3] + v.w * this[4, 3];
            newV.w = v.x * this[1, 4] + v.y * this[2, 4] + v.z * this[3, 4] + v.w * this[4, 4];

            return newV;
        }

        /// <summary>
        /// 矩阵的转置
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 Transpose()
        {
            Matrix4x4 t = new Matrix4x4();
            for(int i=1;i<=4;i++)
                for (int j = 1; j <= 4; j++)
                    t[i, j] = this[j, i];
            return t;
        }
    }
}
