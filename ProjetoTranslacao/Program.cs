using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace ProjetoTranslacao
{
    class Program
    {
        static float tx = 0.3f;
        static float ty = 0.3f;
        static void display()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLineWidth(2);
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex2f(0.0f, 0.0f);
            Gl.glVertex2f(0.15f, 0.3f);
            Gl.glVertex2f(0.3f, 0.0f);
            Gl.glEnd();
            Gl.glPushMatrix();
            Gl.glTranslatef(tx, ty, 0.0f);
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex2f(0.0f, 0.0f);
            Gl.glVertex2f(0.15f, 0.3f);
            Gl.glVertex2f(0.3f, 0.0f);
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
                    ty += 0.3f;
                    break;
                case Glut.GLUT_KEY_DOWN:
                    ty -= 0.3f;
                    break;

                case Glut.GLUT_KEY_LEFT:
                    tx -= 0.3f;
                    break;

                case Glut.GLUT_KEY_RIGHT:
                    tx += 0.3f;
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
            Glut.glutDisplayFunc(display);
            Glut.glutSpecialFunc(TeclasEspeciais);
            Glut.glutMainLoop();
        }
    }
}
