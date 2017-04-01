using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityShader_MatrixTransform
{
    internal class Vector4
    {
        public double x, y, z, w;

        public Vector4()
        {
        }

        public Vector4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4(Vector4 v)
        {
            //值拷贝，
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = v.w;
        }

        /*  下列向量可以重新设为Vector3  */

        //c# 运算符重载之 减号重载  
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x - b.x, a.y - b.y, a.z - b.y, a.w - b.w);
        }

        //叉乘(u * v)
        public Vector4 Cross(Vector4 v)
        {
            return new Vector4(this.y*v.z - this.z*v.y,
                this.z*v.x - this.x*v.z,
                this.x*v.y - this.y*v.x
                , 0);
        }

        //点积(light · reflectPoint)
        public float Dot(Vector4 v)
        {
            return (float) (this.x*v.x + this.y*v.y + this.z*v.z);
        }

        //计算 向量的模，不覆盖原先的值
        public Vector4 Normalized
        {
            get
            {
                double Mod = Math.Sqrt(x*x + y*y + z*z + w*w);
                return new Vector4(x/Mod,y/Mod,z/Mod,w/Mod);
            }
        }
    }
}
