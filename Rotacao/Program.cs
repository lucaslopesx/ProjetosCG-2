using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Rotacao
{
    class Program
    {
        static float rot = 0.0f;
        static void desenhaTriangulo()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLineWidth(2);
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glPushMatrix();
            Gl.glRotatef(rot, 1.0f, 1.0f, 1.0f);
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex2f(0.3f, 0.4f);
            Gl.glVertex2f(0.45f, 0.7f);
            Gl.glVertex2f(0.6f, 0.4f);
            Gl.glEnd();
            Gl.glPopMatrix();
            Glut.glutSwapBuffers(); //Atualiza o BUFFER
        }
        static void inicializa()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0f, 1.0f, 0.0f, 1.0f);
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
            Glut.glutDisplayFunc(desenhaTriangulo);
            Glut.glutSpecialFunc(TeclasEspeciais);
            Glut.glutMainLoop();
        }
    }
}
