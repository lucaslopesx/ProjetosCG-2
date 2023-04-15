using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Rotacao2._0
{
    class Program
    {
        static float rot = 0.0f;
        const float PI = 3.14f;
        static void desenhaCirculo()
        {
            Gl.glPushMatrix();
            Gl.glRotatef(rot, 0.0f, 0.0f, 1.0f);
            float raio, x, y, pontos;
            raio = 0.3f;
            pontos = (2 * PI) / 8;
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex2f(0.0f, 0.0f);
            for (float angulo = 0.0f; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo));
                y = (float)(raio * Math.Sin(angulo));
                Gl.glVertex2f(x, y);
            }
            x = (float)(raio * Math.Cos(2 * PI));
            y = (float)(raio * Math.Sin(2 * PI));
            Gl.glVertex2f(x, y);
            Gl.glEnd();
            Gl.glFlush();
            Gl.glPopMatrix();
        }
        static void inicializa()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
        }
        static void TeclasEspeciais(int key, int x, int y)
        {
            switch (key)
            {
                case Glut.GLUT_KEY_UP:
                    rot += 5.0f;
                    break;

                case Glut.GLUT_KEY_DOWN:
                    rot -= 5.0f;
                    break;

            }
            Glut.glutPostRedisplay();
        }
        static void Main()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_SINGLE | Glut.GLUT_RGB);
            Glut.glutCreateWindow("Projeto");
            inicializa();
            Glut.glutDisplayFunc(desenhaCirculo);
            Glut.glutSpecialFunc(TeclasEspeciais);
            Glut.glutMainLoop();
        }
    }
}
