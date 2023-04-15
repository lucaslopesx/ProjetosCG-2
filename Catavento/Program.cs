using Tao.OpenGl;
using Tao.FreeGlut;
using System;

namespace ProjetoLaboratório2D
{
    class Program
    {
        const float PI = 3.14159265358f;
        static float rot = 0.0f;
        static void desenha()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            fundo();
            haste();
            HelicesCatavento();
            centro();
            Glut.glutSwapBuffers();
        }
        static void centro()
        {
            float raio, x, y, pontos;
            raio = 0.033f;
            pontos = (2 * PI) / 1000;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glVertex2f(0.5f, 0.6f);
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.6f);
                Gl.glVertex2f(x, y);
            }

            Gl.glEnd();
        }
        static void haste()
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0.803922f, 0.521569f, 0.247059f);
            Gl.glVertex2f(0.51f, 0.6f);
            Gl.glVertex2f(0.48f, 0.6f);
            Gl.glVertex2f(0.48f, 0.0f);
            Gl.glVertex2f(0.51f, 0.0f);
            Gl.glEnd();
        }
        static void Helice()
        {
            Gl.glColor3f(0.745098f, 0.745098f, 0.745098f);
            Gl.glBegin(Gl.GL_TRIANGLES);
            Gl.glVertex2f(0.0f, 0.0f);
            Gl.glVertex2f(0.3f, 0.0f);
            Gl.glVertex2f(0.3f, 0.2f);
            Gl.glEnd();
        }
        static void HelicesCatavento()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(0.5f, 0.6f, 0.0f);
            Gl.glRotatef(rot, 0.0f, 0.0f, 1.0f);
            Helice();
            Gl.glPushMatrix();
            for (int i = 0; i < 3; i++)
            {
                Gl.glRotatef(90.0f, 0.0f, 0.0f, 1.0f);
                Helice();

            }
            Gl.glPopMatrix();
            Gl.glPopMatrix();
        }
        static void fundo()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0.282353f, 0.239216f, 0.545098f);
            Gl.glVertex2f(1.0f, 1.0f);
            Gl.glVertex2f(0.0f, 1.0f);
            Gl.glColor3f(0.0f, 0.74902f, 1.0f);
            Gl.glVertex2f(0.0f, 0.2f);
            Gl.glVertex2f(1.0f, 0.2f);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0.678431f, 1.0f, 0.184314f);
            Gl.glVertex2f(1.0f, 0.2f);
            Gl.glVertex2f(0.0f, 0.2f);
            Gl.glVertex2f(0.0f, 0.0f);
            Gl.glVertex2f(1.0f, 0.0f);
            Gl.glEnd();
        }
        static void Timer(int value)
        {
            rot -= 5;
            Glut.glutPostRedisplay();
            Glut.glutTimerFunc(40, Timer, 1);
        }
        static void inicializa()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, 1.0, 0.0, 1.0);
            Gl.glClearColor(1, 1, 1, 0);
        }
        static void Main()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_SINGLE | Glut.GLUT_RGB);
            Glut.glutCreateWindow("Catavento");
            Glut.glutDisplayFunc(desenha);
            Glut.glutTimerFunc(40, Timer, 1);
            inicializa();
            Glut.glutMainLoop();
        }
    }
}
