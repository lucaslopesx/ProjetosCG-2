using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace RotacaoTriangulo
{
    internal class Program
    {
        static float rot = 0.0f;
        static void desenhaTriangulo()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glPushMatrix();
            Gl.glTranslatef(0.5f, 0.4f, 0.0f);
            Gl.glRotatef(rot, 0.0f, 0.0f, 1.0f);
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
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
        static void Timer(int value)
        {
            rot += 5.0f;
            Glut.glutPostRedisplay();
            Glut.glutTimerFunc(50, Timer, 1);
        }
        static void Main()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_SINGLE | Glut.GLUT_RGB);
            Glut.glutCreateWindow("Projeto");
            inicializa();
            Glut.glutDisplayFunc(desenhaTriangulo);
            Glut.glutTimerFunc(50, Timer, 1);
            Glut.glutMainLoop();
        }
    }
}
