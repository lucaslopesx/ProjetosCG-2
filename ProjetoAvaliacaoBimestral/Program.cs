using System;
using Tao.FreeGlut;
using Tao.OpenGl;
using System.Drawing;

namespace Projeto
{
    class Program
    {
        static float rot = 0.0f;
        static float carPositionX = 0.0f;
        const float PI = 3.14f;

        static void desenhaCirculo(bool isFrontWheel)
        {
            float positionX = carPositionX;
            if (isFrontWheel)
            {
                positionX += 0.4f;
            }

            if (!isFrontWheel)
            {
                positionX += -0.4f;
            }
            Gl.glPushMatrix();
            Gl.glTranslatef(positionX, 0.0f, 0.0f);
            Gl.glRotatef(rot, 0.0f, 0.0f, 1.0f);

            float raio, x, y, pontos;
            raio = 0.3f;
            pontos = (2 * PI) / 8;

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

        static void desenhaCarro()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(carPositionX, 0.0f, 0.0f); // Use the carPositionX variable for translation

            // desenha o corpo do carro
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex2f(-0.5f, 0.0f);
            Gl.glVertex2f(0.5f, 0.0f);
            Gl.glVertex2f(0.5f, 0.5f);
            Gl.glVertex2f(-0.5f, 0.5f);
            Gl.glEnd();

            // desenha as janelas do carro
            Gl.glColor3f(0.0f, 0.0f, 1.0f);
            Gl.glBegin(Gl.GL_QUADS);

            // janela esquerda
            Gl.glVertex2f(-0.4f, 0.3f);
            Gl.glVertex2f(-0.1f, 0.3f);
            Gl.glVertex2f(-0.1f, 0.45f);
            Gl.glVertex2f(-0.4f, 0.45f);

            // janela direita
            Gl.glVertex2f(0.1f, 0.3f);
            Gl.glVertex2f(0.4f, 0.3f);
            Gl.glVertex2f(0.4f, 0.45f);
            Gl.glVertex2f(0.1f, 0.45f);

            Gl.glEnd();
            Gl.glPopMatrix();
        }



        static void display()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            desenhaCirculo(true);
            desenhaCirculo(false);
            desenhaCarro();

            Glut.glutSwapBuffers();
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
                case Glut.GLUT_KEY_LEFT:
                    rot += 10.0f;
                    carPositionX -= 0.05f;
                    break;

                case Glut.GLUT_KEY_RIGHT:
                    rot -= 10.0f;
                    carPositionX += 0.05f;
                    break;

                case Glut.GLUT_KEY_UP: // Add case for the up arrow key
                    break;

                case Glut.GLUT_KEY_DOWN: // Add case for the down arrow key
                    break;
            }

            Glut.glutPostRedisplay();
        }

        static void Main()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGB);
            Glut.glutInitWindowSize(800, 600);
            Glut.glutCreateWindow("Projeto");

            Glut.glutReshapeFunc(OnReshape);

            Glut.glutDisplayFunc(display);
            Glut.glutSpecialFunc(TeclasEspeciais);
            inicializa();
            Glut.glutMainLoop();
        }

        private static void OnReshape(int width, int height)
        {
            Gl.glViewport(0, 0, width, height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            float aspectRatio = (float)width / (float)height;

            if (width <= height)
            {
                Gl.glOrtho(-1.0, 1.0, -1.0 / aspectRatio, 1.0 / aspectRatio, -1.0, 1.0);
            }
            else
            {
                Gl.glOrtho(-1.0 * aspectRatio, 1.0 * aspectRatio, -1.0, 1.0, -1.0, 1.0);
            }

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }
    }
}
