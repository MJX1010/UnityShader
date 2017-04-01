using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnityShader_MatrixTransform
{
    public partial class Form1 : Form
    {
        private Triangle3D t;

        private Cube cube;

        private Matrix4x4 m_Scale;
        
        private Matrix4x4 m_RotateY;
        private Matrix4x4 m_RotateX;
        private Matrix4x4 m_RotateZ;

        private int angleOffset = 0;

        private Matrix4x4 m_viewTranslation;

        private Matrix4x4 m_projection;

        public Form1()
        {
            InitializeComponent();

            //缩放矩阵
            //注意[4,4]的矩阵元素为1，保证该矩阵可以反复进行缩放操作
            //注意该矩阵未赋值的元素默认为 0
            m_Scale = new Matrix4x4();
            m_Scale[1, 1] = 250;
            m_Scale[2, 2] = 250;
            m_Scale[3, 3] = 250;
            m_Scale[4, 4] = 1;

            m_RotateY = new Matrix4x4();
            m_RotateX = new Matrix4x4();
            m_RotateZ = new Matrix4x4();

            //相机平移矩阵(Z轴),等同于物体对象相对于相机的偏移
            m_viewTranslation=new Matrix4x4();
            m_viewTranslation[1, 1] = 1;
            m_viewTranslation[2, 2] = 1;
            m_viewTranslation[3, 3] = 1;
            m_viewTranslation[4, 3] = 250;
            m_viewTranslation[4, 4] = 1;

            m_projection = new Matrix4x4();
            m_projection[1, 1] = 1;
            m_projection[2, 2] = 1;
            m_projection[3, 3] = 1;
            m_projection[3, 4] = 1.0/250;
            m_projection[4, 4] = 1;

            cube = new Cube();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //注意 w 的值为1 才能进行正常的矩阵变换
            //z 为 0
            Vector4 a = new Vector4(0, 0.5, 0, 1);//此时三角形顶点朝下
            Vector4 b = new Vector4(0.5,-0.5,0,1);
            Vector4 c = new Vector4(-0.5,-0.5,0,1);
            t = new Triangle3D(a,b,c);

           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //t.DrawOnScreen(e.Graphics);
            cube.DrawOnScreen(e.Graphics,checkBox4.Checked);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            angleOffset += 2;
            double angle = angleOffset / 360.0 * Math.PI;
            if (angleOffset == 360)
            {
                angleOffset = 0;
                angle = Math.PI;
            }
           

            //注意，以下三个旋转矩阵仅适用于顶点朝上的三角形
            //如：            Vector4 a = new Vector4(0, -0.5, 0, 1);//此时三角形顶点朝上，但是坐标轴为Y轴朝下为正
            //                Vector4 b = new Vector4(0.5, 0.5, 0, 1);
            //                Vector4 c = new Vector4(-0.5, 0.5, 0, 1);
            //                当前三角形不适用与下列三个旋转矩阵
            m_RotateY[1, 1] = Math.Cos(angle);
            m_RotateY[1, 3] = Math.Sin(angle);
            m_RotateY[2, 2] = 1;
            m_RotateY[3, 1] = -Math.Sin(angle);
            m_RotateY[3, 3] = Math.Cos(angle);
            m_RotateY[4, 4] = 1;

            m_RotateX[1, 1] = 1;
            m_RotateX[2, 2] = Math.Cos(angle);
            m_RotateX[2, 3] = Math.Sin(angle);
            m_RotateX[3, 2] = -Math.Sin(angle);
            m_RotateX[3, 3] = Math.Cos(angle);
            m_RotateX[4, 4] = 1;

            m_RotateZ[1, 1] = Math.Cos(angle);
            m_RotateZ[1, 2] = Math.Sin(angle);
            m_RotateZ[2, 1] = -Math.Sin(angle);
            m_RotateZ[2, 2] = Math.Cos(angle);
            m_RotateZ[3, 3] = 1;
            m_RotateZ[4, 4] = 1;

            //因为旋转矩阵为正交的，所以可以通过原矩阵和转置矩阵相乘得到单位矩阵实现取消旋转的操作
            if (this.checkBox1.Checked)
            {
                Matrix4x4 tx = m_RotateX.Transpose();
                m_RotateX = m_RotateX.Mul(tx);
            }
            if (this.checkBox2.Checked)
            {
                Matrix4x4 ty = m_RotateY.Transpose();
                m_RotateY = m_RotateY.Mul(ty);
            }
            if (this.checkBox3.Checked)
            {
                Matrix4x4 tz = m_RotateZ.Transpose();
                m_RotateZ = m_RotateZ.Mul(tz);
            }
            Matrix4x4 m_Rot = m_RotateX.Mul(m_RotateY.Mul(m_RotateZ));

            //模型到世界
            Matrix4x4 m = m_Scale.Mul(m_Rot);

            //添加光照（光源位置）
            //t.CalculateLighting(m,new Vector4(-1,1,-1,0));
            cube.CalculateLighting(m, new Vector4(-1, 1, -1, 0));

           // m = m.Mul(m_RotateX);
            //世界到相机
            Matrix4x4 mv = m.Mul(m_viewTranslation);
            //相机到屏幕（投影）
            Matrix4x4 mvp = mv.Mul(m_projection);

            //t.Transform(mvp);
            cube.Transform(mvp);

            ////按给定缩放矩阵进行缩放
            //t.Transform(m_Scale);
             //cube.Transform(m_Scale);
            this.Invalidate();//强制无效窗口，使其重绘
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            m_viewTranslation[4, 3] = (sender as TrackBar).Value;
        }


      
    }
}
